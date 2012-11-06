using System;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class ReceiveSamlAuthnResponseCommand
    {
        public Saml2Response SamlResponse { get; set; }
    }

    public class ReceiveSamlAuthnResponseHandler : IHandleCommands<ReceiveSamlAuthnResponseCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly ISignUsers _userSigner;
        private readonly IStorePasswords _passwords;
        private readonly IUnitOfWork _unitOfWork;

        public ReceiveSamlAuthnResponseHandler(ICommandEntities entities
            , ISignUsers userSigner
            , IStorePasswords passwords
            , IUnitOfWork unitOfWork
        )
        {
            _entities = entities;
            _userSigner = userSigner;
            _passwords = passwords;
            _unitOfWork = unitOfWork;
        }

        public void Handle(ReceiveSamlAuthnResponseCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (command.SamlResponse == null)
                throw new InvalidOperationException("The SAML Response cannot be null.");
            var samlResponse = command.SamlResponse;

            // get the trusted establishment for this saml 2 response
            var establishment = GetTrustedIssuingEstablishment(samlResponse);

            // verify the response's signature
            VerifySignature(samlResponse);

            // first try to find user by SAML person targeted id
            var user = GetUserByEduPersonTargetedId(samlResponse);

            // when saml user does not exist, search for person
            if (user == null)
            {
                var person = GetPerson(samlResponse);
                user = person.User ?? new User { Person = person };
            }

            // delete local account if it exists
            if (!string.IsNullOrWhiteSpace(user.Name) && _passwords.Exists(user.Name))
                _passwords.Destroy(user.Name);

            // enforce invariants on user
            user.Name = samlResponse.EduPersonPrincipalName;
            user.EduPersonTargetedId = samlResponse.EduPersonTargetedId;
            user.IsRegistered = true;

            // remove previous scoped affiliations and add new ones
            var oldScopedAffiliations = user.EduPersonScopedAffiliations.ToArray();
            var newScopedAffiliations = samlResponse.EduPersonScopedAffiliations ?? new string[] {};
            newScopedAffiliations = newScopedAffiliations.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            foreach (var oldScopedAffiliation in oldScopedAffiliations)
                if (!newScopedAffiliations.Contains(oldScopedAffiliation.Value))
                    user.EduPersonScopedAffiliations.Remove(oldScopedAffiliation);
            foreach (var newScopedAffiliation in newScopedAffiliations)
                if (oldScopedAffiliations.ByValue(newScopedAffiliation) == null)
                    user.EduPersonScopedAffiliations.Add(
                        new EduPersonScopedAffiliation
                        {
                            Value = newScopedAffiliation,
                            Number = user.EduPersonScopedAffiliations.NextNumber(),
                        });

            // log the subject name id
            var subjectNameId = samlResponse.SubjectNameIdentifier;
            if (!string.IsNullOrWhiteSpace(subjectNameId))
            {
                var subjectNameIdentifier = user.SubjectNameIdentifiers.ByValue(subjectNameId);
                if (subjectNameIdentifier == null)
                    user.SubjectNameIdentifiers.Add(
                        new SubjectNameIdentifier
                        {
                            Value = subjectNameId,
                            Number = user.SubjectNameIdentifiers.NextNumber(),
                        });
                else subjectNameIdentifier.UpdatedOnUtc = DateTime.UtcNow;
            }


            // enforce invariants on person
            if (!user.Person.IsAffiliatedWith(establishment))
                user.Person.AffiliateWith(establishment);

            // remove previous saml mails, add new ones, and update existing ones
            var oldSamlMails = user.Person.Emails.FromSaml().ToArray();
            var newSamlMails = samlResponse.Mails ?? new string[] {};
            newSamlMails = newSamlMails.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            foreach (var oldSamlMail in oldSamlMails)
                if (!newSamlMails.Contains(oldSamlMail.Value))
                    user.Person.Emails.Remove(oldSamlMail);
            foreach (var newSamlMail in newSamlMails)
                if (user.Person.GetEmail(newSamlMail) == null)
                    user.Person.AddEmail(newSamlMail);
            foreach (var emailAddress in user.Person.Emails)
                if (newSamlMails.Contains(emailAddress.Value))
                {
                    emailAddress.IsFromSaml = true;
                    emailAddress.IsConfirmed = true;
                }

            // make sure person has at least 1 confirmed email address
            var defaultEmail = user.Person.DefaultEmail;
            if (defaultEmail == null || !defaultEmail.IsConfirmed)
            {
                if (defaultEmail != null) defaultEmail.IsDefault = false;
                defaultEmail = user.Person.AddEmail(samlResponse.EduPersonPrincipalName);
                defaultEmail.IsDefault = true;
                defaultEmail.IsConfirmed = true;
            }

            // update db
            if (user.RevisionId == 0) _entities.Create(user);
            else _entities.Update(user);
            _unitOfWork.SaveChanges();

            // sign on user
            _userSigner.SignOn(user.Name);
        }

        private Establishment GetTrustedIssuingEstablishment(Saml2Response samlResponse)
        {
            var issuerNameIdentifier = samlResponse.IssuerNameIdentifier;

            var establishment = _entities.Get<Establishment>()
                .EagerLoad(_entities, new Expression<Func<Establishment, object>>[]
                {
                    e => e.SamlSignOn,
                })
                .BySamlEntityId(issuerNameIdentifier);

            // when getting by the saml entity id, establishment is immediately trusted if found
            var isIssuerTrusted = establishment != null;
            if (!isIssuerTrusted)
                throw new InvalidOperationException(string.Format(
                    "SAML 2 response issuer '{0}' does not appear to be trusted.",
                        issuerNameIdentifier));

            return establishment;
        }

        private static void VerifySignature(Saml2Response response)
        {
            if (response.IsSigned && !response.VerifySignature())
                throw new InvalidOperationException(string.Format(
                    "SAML 2 response signature for '{0}' failed to verify.",
                        response.IssuerNameIdentifier));
        }

        private User GetUserByEduPersonTargetedId(Saml2Response samlResponse)
        {
            var user = _entities.Get<User>()
                .EagerLoad(_entities, _loadUser)
                .ByEduPersonTargetedId(samlResponse.EduPersonTargetedId);
            return user;
        }

        private User GetUserByName(Saml2Response samlResponse)
        {
            var user = _entities.Get<User>()
                .EagerLoad(_entities, _loadUser)
                .ByName(samlResponse.EduPersonPrincipalName);
            return user;
        }

        private Person GetPerson(Saml2Response samlResponse)
        {
            Person person = null;

            // first, look up person by mail attribute (if present)
            var mails = samlResponse.Mails;
            if (mails != null && mails.Any())
            {
                foreach (var mail in mails)
                {
                    person = GetPersonByEmail(mail);
                    if (person != null) break;
                }
            }

            // next, look for person by person principal name
            if (person == null)
                person = GetPersonByEmail(samlResponse.EduPersonPrincipalName);

            // need to create person if they do not exist
            if (person == null)
            {
                person = new Person
                {
                    DisplayName = samlResponse.CommonName ??
                                samlResponse.DisplayName ??
                                samlResponse.EduPersonPrincipalName,
                    FirstName = samlResponse.GivenName,
                    LastName = samlResponse.SurName,
                };
            }

            // make sure there is not a user with person principal name
            var user = GetUserByName(samlResponse);
            if (user != null && user.Person != person)
                throw new InvalidOperationException(string.Format(
                    "The user account '{0}' does not match expected person '{1}'.",
                        user.Name, person.DisplayName));

            return person;
        }

        private Person GetPersonByEmail(string email)
        {
            var person = _entities.Get<Person>()
                .EagerLoad(_entities, _loadPerson)
                .ByEmail(email);
            return person;
        }

        private readonly Expression<Func<Person, object>>[] _loadPerson =
            new Expression<Func<Person, object>>[]
            {
                p => p.Emails,
                p => p.User.EduPersonScopedAffiliations,
            };

        private readonly Expression<Func<User, object>>[] _loadUser =
            new Expression<Func<User, object>>[]
            {
                u => u.EduPersonScopedAffiliations,
                u => u.Person.Emails,
            };
    }
}
