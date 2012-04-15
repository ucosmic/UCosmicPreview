using System;
using System.Linq.Expressions;
using UCosmic.Domain.People;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.Establishments
{
    public class SignOnSamlUserHandler : IHandleCommands<SignOnSamlUserCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly IProcessQueries _queryProcessor;
        private readonly ISignUsers _userSigner;

        public SignOnSamlUserHandler(ICommandEntities entities, IProcessQueries queryProcessor, ISignUsers userSigner)
        {
            _entities = entities;
            _queryProcessor = queryProcessor;
            _userSigner = userSigner;
        }

        public void Handle(SignOnSamlUserCommand command)
        {
            // get saml response from http context
            var samlResponse = GetSamlResponse(command);

            // extract data from the response
            var issuerNameIdentifier = samlResponse.IssuerNameIdentifier;
            var subjectNameIdentifier = samlResponse.SubjectNameIdentifier;
            var eduPrincipalPersonName = samlResponse.GetAttributeValueByFriendlyName
                (SamlAttributeFriendlyName.EduPersonPrincipalName);

            // get the establishment for this saml 2 response
            var establishment = GetIssuingEstablishment
                (issuerNameIdentifier);

            // make sure the issuer is trusted
            ThrowExceptionIfIssuingEstablishmentIsNotTrusted
                (establishment, issuerNameIdentifier);

            // verify the response's signature
            ThrowExceptionIfSamlResponseSignatureFailsVerification
                (samlResponse, issuerNameIdentifier);

            // find person with email address
            var person = GetOrCreatePersonWithEmail(eduPrincipalPersonName);

            // find user with this email address
            var user = _queryProcessor.Execute(new GetUserByNameQuery { Name = eduPrincipalPersonName });

            // if there is a user, make sure its person matches
            ThrowExceptionIfUserNameDoesNotMatchPersonEmail(user, person);

            // make sure the person's email address is confirmed
            person.Emails.ByValue(eduPrincipalPersonName).IsConfirmed = true;

            // make sure the person is affiliated with the issuing establishment
            if (!person.IsAffiliatedWith(establishment)) person.AffiliateWith(establishment);

            // make sure the user exists
            if (person.User == null) person.User = new User();

            // make sure the user is registered and has matching name
            person.User.Name = eduPrincipalPersonName;
            person.User.IsRegistered = true;

            // sign on the user
            _userSigner.SignOn(eduPrincipalPersonName);

            command.ReturnUrl = samlResponse.RelayResourceUrl;
        }

        private Saml2Response GetSamlResponse(SignOnSamlUserCommand command)
        {
            var samlResponse = _queryProcessor.Execute(
                new ReceiveSaml2ResponseQuery
                {
                    HttpContext = command.HttpContext,
                    SsoBinding = command.SsoBinding,
                }
            );
            return samlResponse;
        }

        private Establishment GetIssuingEstablishment(string issuerNameIdentifier)
        {
            var establishment = _queryProcessor.Execute(
                new GetEstablishmentBySamlEntityIdQuery
                {
                    SamlEntityId = issuerNameIdentifier,
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.SamlSignOn,
                    },
                }
            );
            return establishment;
        }

        private Person GetOrCreatePersonWithEmail(string eduPrincipalPersonName)
        {
            var person = _queryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = eduPrincipalPersonName,
                }
            );

            // create person if not found
            if (person == null)
            {
                person = new Person
                {
                    DisplayName = eduPrincipalPersonName,
                };

                person.AddEmail(eduPrincipalPersonName);
                _entities.Create(person);
            }
            return person;
        }

        private static void ThrowExceptionIfIssuingEstablishmentIsNotTrusted(Establishment establishment, string issuerNameIdentifier)
        {
            var isIssuerTrusted = establishment != null && establishment.HasSamlSignOn();
            if (!isIssuerTrusted)
                throw new InvalidOperationException(string.Format(
                    "SAML 2 response issuer '{0}' does not appear to be trusted.",
                        issuerNameIdentifier));
        }

        private static void ThrowExceptionIfSamlResponseSignatureFailsVerification(Saml2Response response, string issuerNameIdentifier)
        {
            if (!response.VerifySignature())
                throw new InvalidOperationException(string.Format(
                    "SAML 2 response response signature for '{0}' failed to verify.",
                        issuerNameIdentifier));
        }

        private static void ThrowExceptionIfUserNameDoesNotMatchPersonEmail(User user, Person person)
        {
            // when there is a user associated with a different person than was expected, data needs looked at
            if (user != null && user.Person != person)
                throw new InvalidOperationException(string.Format(
                    "The user account '{0}' does not match expected person '{1}'.",
                        user.Name, person.DisplayName));
        }
    }
}
