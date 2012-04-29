using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Establishments
{
    public class SignOnSamlUserCommand
    {
        public Saml2SsoBinding SsoBinding { get; set; }
        public HttpContextBase HttpContext { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class SignOnSamlUserHandler : IHandleCommands<SignOnSamlUserCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly IProcessQueries _queryProcessor;
        private readonly ISignUsers _userSigner;

        public SignOnSamlUserHandler(IProcessQueries queryProcessor, ICommandEntities entities, ISignUsers userSigner)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _userSigner = userSigner;
        }

        public void Handle(SignOnSamlUserCommand command)
        {
            // get saml response from http context
            var samlResponse = GetSamlResponse(command);

            // get the establishment for this saml 2 response
            var establishment = GetIssuingEstablishment(samlResponse.IssuerNameIdentifier);

            // verify the response's signature
            ThrowExceptionIfSamlResponseSignatureFailsVerification
                (samlResponse, samlResponse.IssuerNameIdentifier);

            // first try to find user by SAML person targeted id
            var user = _queryProcessor.Execute(
                new GetUserByEduPersonTargetedIdQuery
                {
                    EduPersonTargetedId = samlResponse.EduPersonTargetedId,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.SubjectNameIdentifiers,
                        u => u.EduPersonScopedAffiliations,
                        u => u.Person,
                    },
                });
            if (user != null)
            {
                // update the user's sign on info
                user.LogSubjectNameIdentifier(samlResponse.SubjectNameIdentifier);
                user.SetEduPersonScopedAffiliations(samlResponse.EduPersonScopedAffiliations);

                // update the person's mail
                EnforceSamlEmailInvariants(user.Person, samlResponse);

                // sign on the user
                _userSigner.SignOn(samlResponse.EduPersonPrincipalName);

                return;
            }

            // find person with email address
            var person = GetOrCreatePersonWithEmail(samlResponse);

            // find user with this email address
            user = _queryProcessor.Execute(new GetUserByNameQuery { Name = samlResponse.EduPersonPrincipalName });

            // if there is a user, make sure its person matches
            ThrowExceptionIfUserNameDoesNotMatchPersonEmail(user, person);

            // make sure the person's email address is confirmed
            person.Emails.ByValue(samlResponse.EduPersonPrincipalName).IsConfirmed = true;

            // make sure the person is affiliated with the issuing establishment
            if (!person.IsAffiliatedWith(establishment)) person.AffiliateWith(establishment);

            // make sure the user exists
            if (person.User == null) person.User = new User();

            // make sure the user is registered and has matching name
            person.User.Name = samlResponse.EduPersonPrincipalName;
            person.User.EduPersonTargetedId = samlResponse.EduPersonTargetedId;
            person.User.IsRegistered = true;

            // sign on the user
            _userSigner.SignOn(samlResponse.EduPersonPrincipalName);

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

        internal const string UntrustedIssuerExceptionMessageFormat =
            "SAML 2 response issuer '{0}' does not appear to be trusted.";

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

            // when getting by the saml entity id, establishment is immediately trusted if found
            var isIssuerTrusted = establishment != null; // && establishment.HasSamlSignOn();
            if (!isIssuerTrusted)
                throw new InvalidOperationException(string.Format(
                    UntrustedIssuerExceptionMessageFormat,
                        issuerNameIdentifier));

            return establishment;
        }

        private Person GetPersonByEmail(string email)
        {
            var person = _queryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = email,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails,
                    },
                }
            );
            return person;
        }

        private Person GetOrCreatePersonWithEmail(Saml2Response samlResponse)
        {
            // first lookup person whose email equals the eduPersonPrincipalName
            var person = GetPersonByEmail(samlResponse.EduPersonPrincipalName);

            // next, lookup a person whose email equals a mail attribute
            var mails = samlResponse.Mails;
            if (person == null && mails != null)
            {
                foreach (var mail in mails.Where(mail => !string.IsNullOrWhiteSpace(mail)))
                {
                    person = GetPersonByEmail(mail);
                    if (person != null) break;
                }
            }

            // create person if not found
            if (person == null)
            {
                person = new Person
                {
                    DisplayName = samlResponse.CommonName ?? samlResponse.DisplayName ?? samlResponse.EduPersonPrincipalName,
                    FirstName = samlResponse.GivenName,
                    LastName = samlResponse.SurName,
                };
                _entities.Create(person);
            }

            EnforceSamlEmailInvariants(person, samlResponse);

            return person;
        }

        private static void EnforceSamlEmailInvariants(Person person, Saml2Response samlResponse)
        {
            // get the saml mail attribute statement
            var mails = samlResponse.Mails;

            // clear all previous email addresses provided by SAML
            person.ResetSamlEmails();

            // only add eduPersonPrincipalName as email if no emails were provided
            if (mails == null || mails.Length < 1)
            {
                person.AddEmail(samlResponse.EduPersonPrincipalName, true);
            }
            else
            {
                foreach (var mail in mails)
                    person.AddEmail(mail, true);
            }
        }

        internal const string ResponseSignatureFailedVerificationMessageFormat =
            "SAML 2 response response signature for '{0}' failed to verify.";

        private static void ThrowExceptionIfSamlResponseSignatureFailsVerification(Saml2Response response, string issuerNameIdentifier)
        {
            if (!response.VerifySignature())
                throw new InvalidOperationException(string.Format(
                    ResponseSignatureFailedVerificationMessageFormat,
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
