using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class Person : RevisableEntity
    {
        #region Construction & Fields

        public Person()
        {
            _emails = new List<EmailAddress>();
            _affiliations = new List<Affiliation>();
        }

        private ICollection<EmailAddress> _emails;
        private ICollection<Affiliation> _affiliations;

        #endregion
        #region Name

        public bool IsDisplayNameDerived { get; set; }

        public string DisplayName { get; set; }

        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string DeriveDisplayName()
        {
            return PersonFactory.DeriveDisplayName(this);
        }

        #endregion
        #region User

        public virtual User User { get; set; }

        #endregion
        #region EmailAddresses

        public virtual ICollection<EmailAddress> Emails
        {
            get { return _emails; }
            set { _emails = value; }
        }

        public EmailAddress AddEmail(string value, bool isFromSaml = false)
        {
            // email may already exist
            var email = Emails.ByValue(value);
            if (email != null)
            {
                email.IsFromSaml = isFromSaml;
                if (email.IsFromSaml && !email.IsConfirmed)
                    email.IsConfirmed = true;
            }
            else
            {
                // create email
                email = new EmailAddress
                {
                    // if person does not already have a default email, this is it
                    IsDefault = (Emails.Count(a => a.IsDefault) == 0),
                    Value = value,
                    Person = this,
                    IsFromSaml = isFromSaml,
                    IsConfirmed = isFromSaml,
                };

                // add & return email
                Emails.Add(email);
            }

            return email;
        }

        internal void ResetSamlEmails()
        {
            foreach (var email in Emails.FromSaml().ToList())
            {
                // cannot remove email addresses with messages attached
                if (email.Messages.Any())
                {
                    email.IsFromSaml = false;
                }
                else
                {
                    Emails.Remove(email);
                }
            }
        }

        #endregion
        #region Affiliations

        public virtual ICollection<Affiliation> Affiliations
        {
            get { return _affiliations; }
            set { _affiliations = value; }
        }

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
                    var defaultAffiliation = Affiliations.Current().SingleOrDefault(a => a.IsDefault);
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
        #region Operations

        public User SignUp(EmailAddress emailAddress)
        {
            if (User == null || !User.IsRegistered)
            {
                if (emailAddress != null && Emails.Contains(emailAddress))
                {
                    User = User ?? new User();
                    User.Name = User.Name ?? emailAddress.Value;
                    User.IsRegistered = true;
                    return User;
                }
            }
            return null;
        }

        #endregion
        public override string ToString()
        {
            return DisplayName;
        }
    }

    public static class PersonExtensions
    {
        public static Person ForThreadPrincipal(this IQueryable<Person> query)
        {
            return (query != null)
                ? query.Current().SingleOrDefault(p =>
                    p.User != null && p.User.Name.Equals(Thread.CurrentPrincipal.Identity.Name))
                : null;
        }
    }
}