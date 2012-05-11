using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class Person : RevisableEntity
    {
        #region Construction

        protected internal Person()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Affiliations = new List<Affiliation>();
            Emails = new List<EmailAddress>();
            Messages = new List<EmailMessage>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        #endregion
        #region Name

        public bool IsDisplayNameDerived { get; protected internal set; }

        public string DisplayName { get; protected internal set; }

        public string Salutation { get; protected internal set; }

        public string FirstName { get; protected internal set; }

        public string MiddleName { get; protected internal set; }

        public string LastName { get; protected internal set; }

        public string Suffix { get; protected internal set; }

        #endregion
        #region User

        public virtual User User { get; protected internal set; }

        #endregion
        #region EmailAddresses

        public virtual ICollection<EmailAddress> Emails { get; protected internal set; }

        public EmailAddress DefaultEmail { get { return Emails.Default(); } }

        public EmailAddress GetEmail(int number)
        {
            if (Emails == null || !Emails.Any()) return null;
            return Emails.ByNumber(number);
        }

        public EmailAddress GetEmail(string value)
        {
            if (Emails == null || !Emails.Any()) return null;
            return Emails.ByValue(value);
        }

        public EmailAddress AddEmail(string value)
        {
            // email may already exist
            var email = Emails.ByValue(value);
            if (email != null) return email;

            // create email
            email = new EmailAddress
            {
                // if person does not already have a default email, this is it
                IsDefault = (Emails.Count(a => a.IsDefault) == 0),
                Value = value,
                Person = this,
                Number = Emails.NextNumber(),
            };

            // add & return email
            Emails.Add(email);

            return email;
        }

        #endregion
        #region Messages

        public virtual ICollection<EmailMessage> Messages { get; protected internal set; }

        #endregion
        #region Affiliations

        public virtual ICollection<Affiliation> Affiliations { get; protected internal set; }

        public Affiliation AffiliateWith(Establishment establishment)
        {
            var currentAffiliations = Affiliations.ToList();

            // affiliation may already exist
            var affiliation = currentAffiliations
                .SingleOrDefault(a => a.Establishment == establishment);
            if (affiliation != null) return affiliation;

            // create affiliation
            affiliation = new Affiliation
            {
                // if person does not already have a default affiliation, this is it
                IsDefault = !currentAffiliations.Any(a => a.IsDefault),
                Establishment = establishment, // affiliate with establishment
                Person = this,

                // for non-institutions, person should not be claiming student, faculty, etc
                IsClaimingEmployee = !establishment.IsInstitution,
            };

            // add & return affiliation
            Affiliations.Add(affiliation);
            return affiliation;
        }

        public Affiliation DefaultAffiliation
        {
            get
            {
                if (Affiliations != null)
                {
                    var defaultAffiliation = Affiliations.SingleOrDefault(a => a.IsDefault);
                    if (defaultAffiliation != null) return defaultAffiliation;
                }
                return null;
            }
        }

        public bool IsAffiliatedWith(Establishment establishment)
        {
            if (Affiliations != null)
            {
                foreach (var affiliation in Affiliations)
                {
                    if (affiliation.EstablishmentId == establishment.RevisionId)
                    {
                        return true;
                    }

                    // check all parents of the establishment
                    if (establishment.Ancestors != null && establishment.Ancestors.Count > 0)
                    {
                        //foreach (var ancestor in establishment.Ancestors.Select(a => a.Ancestor))
                        //{
                        //    if (IsAffiliatedWith(ancestor)) return true;
                        //}
                        if (establishment.Ancestors.Select(a => a.Ancestor).Any(IsAffiliatedWith))
                            return true;
                    }
                }
            }
            return false;
        }

        #endregion

        public override string ToString()
        {
            return DisplayName;
        }
    }

    // TODO: get rid of this class (hooked to institutional agreement module)
}