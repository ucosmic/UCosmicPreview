using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class UserFacade
    {
        private readonly UserFinder _userFinder;
        private readonly EstablishmentFinder _establishmentFinder;
        private readonly PersonFinder _personFinder;
        private readonly ICommandObjects _objectCommander;

        public UserFacade(ICommandObjects objectCommander, UserFinder userFinder, EstablishmentFinder establishmentFinder, PersonFinder personFinder)
        {
            _userFinder = userFinder;
            _establishmentFinder = establishmentFinder;
            _personFinder = personFinder;
            _objectCommander = objectCommander;
        }

        public User GetOrCreate(string userName, bool isRegistered, string saml2SubjectNameId = null)
        {
            var saveChanges = false;

            // first see if user exists
            var user = _userFinder.FindOne(UserBy.UserName(userName))
                ?? _userFinder.FindOne(UserBy.Saml2SubjectNameId(saml2SubjectNameId).ForInsertOrUpdate());

            if (user == null)
            {
                saveChanges = true;
                user = new User
                {
                    UserName = userName,
                    IsRegistered = isRegistered,
                    Saml2SubjectNameId = saml2SubjectNameId,
                    Person = _personFinder.FindOne(PersonBy.EmailAddress(userName))
                                ?? PersonFactory.Create(userName),
                };

                var establishment = _establishmentFinder.FindOne(EstablishmentBy.EmailDomain(userName));
                if (establishment != null)
                    user.Person.AffiliateWith(establishment);

                var email = user.Person.Emails.ByValue(userName);
                if (isRegistered && !email.IsConfirmed) email.IsConfirmed = true;

                _objectCommander.Insert(user);
            }

            // make sure user has correct registration
            if (!isRegistered.Equals(user.IsRegistered))
            {
                user.IsRegistered = isRegistered;
                saveChanges = true;
            }

            // make sure user has correct subject name id
            if (!string.IsNullOrWhiteSpace(saml2SubjectNameId) && !saml2SubjectNameId.Equals(user.Saml2SubjectNameId))
            {
                user.Saml2SubjectNameId = saml2SubjectNameId;
                saveChanges = true;
            }

            if (saveChanges)
                _objectCommander.SaveChanges();

            return user;
        }
    }
}
