using System;
using System.Linq.Expressions;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Establishments
{
    public class SignOnSamlUserHandler : IHandleCommands<SignOnSamlUserCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ISignUsers _userSigner;

        public SignOnSamlUserHandler(IProcessQueries queryProcessor, ISignUsers userSigner)
        {
            _queryProcessor = queryProcessor;
            _userSigner = userSigner;
        }

        public void Handle(SignOnSamlUserCommand command)
        {
            // get the establishment for this saml 2 response
            var establishment = _queryProcessor.Execute(
                new GetEstablishmentBySamlEntityIdQuery
                {
                    SamlEntityId = command.Saml2Response.IssuerNameIdentifier,
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.SamlSignOn,
                    },
                }
            );

            // make sure the issuer is trusted
            var isIssuerTrusted = establishment != null && establishment.HasSamlSignOn();
            if (!isIssuerTrusted)
                throw new InvalidOperationException(string.Format(
                    "SAML 2 response issuer '{0}' does not appear to be trusted.",
                        command.Saml2Response.IssuerNameIdentifier));

            // verify the response's signature
            if (!command.Saml2Response.VerifySignature())
                throw new InvalidOperationException(string.Format(
                    "SAML 2 response response signature for '{0}' failed to verify.",
                        command.Saml2Response.IssuerNameIdentifier));

            // get the subject name identifier and edu person principal name
            var subjectNameIdentifier = command.Saml2Response.SubjectNameIdentifier;
            var eduPrincipalPersonName = command.Saml2Response.GetAttributeValueByFriendlyName
                (SamlAttributeFriendlyName.EduPersonPrincipalName);

            // find person
            var person = _queryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = eduPrincipalPersonName
                }
            );

            // sign on the user
            _userSigner.SignOn(eduPrincipalPersonName);
        }
    }
}
