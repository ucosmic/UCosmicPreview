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

        public EmailAddress AddEmail(string emailAddress)
        {
            var currentEmails = Emails.Current().ToList();

            // email may already exist
            var email = currentEmails.SingleOrDefault(e =>
                e.Value.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
            if (email != null) return email;

            // create email
            email = new EmailAddress
            {
                // if person does not already have a default email, this is it
                IsDefault = (currentEmails.Count(a => a.IsDefault) == 0),
                Value = emailAddress,
                Person = this,
            };

            // add & return email
            Emails.Add(email);
            return email;
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
            var currentAffiliations = Affiliations.Current().ToList();

            // affiliation may already exist
            var affiliation = currentAffiliations
                .SingleOrDefault(a => a.Establishment == establishment);
            if (affiliation != null) return affiliation;

            // create affiliation
            affiliation = new Affiliation
            {
                // if person does not already have a default affiliation, this is it
                IsDefault = (currentAffiliations.Count(a => a.IsDefault) == 0),
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
                foreach (var affiliation in Affiliations.Current())
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
                    User.UserName = User.UserName ?? emailAddress.Value;
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
                    p.User != null && p.User.UserName.Equals(Thread.CurrentPrincipal.Identity.Name))
                : null;
        }

        public static Person ByAffiliation(this IQueryable<Person> query, int affiliationId)
        {
            return (query != null)
                ? query.Current().SingleOrDefault(p =>
                    p.Affiliations.Any(a => a.IsCurrent && !a.IsArchived && !a.IsDeleted
                        && a.RevisionId == affiliationId))
                : null;
        }

        public static Person ByEmail(this IQueryable<Person> query, string email)
        {
            return (query != null)
                ? query.Current().SingleOrDefault(p =>
                    p.Emails.Any(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
                        && e.Value.Equals(email, StringComparison.OrdinalIgnoreCase)))
                : null;
        }

        public static Person ByEmail(this IQueryable<Person> query, Guid entityId)
        {
            return (query != null)
                ? query.Current().SingleOrDefault(p =>
                    p.Emails.Any(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
                        && e.EntityId.Equals(entityId)))
                : null;
        }
       
        public static Person ByConfirmation(this IQueryable<Person> query, Guid token)
        {
            return (query != null && token != Guid.Empty)
                ? query.Current().SingleOrDefault(p =>
                    p.Emails.Any(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
                        && e.Confirmations.Any(c => c.Token == token)))
                : null;
        }

        //public static Person ByMessage(this IQueryable<Person> query, Guid entityId)
        //{
        //    return (query != null && entityId != Guid.Empty)
        //        ? query.Current().SingleOrDefault(p =>
        //            p.Emails.Any(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
        //                && e.Messages.Any(c => c.EntityId == entityId)))
        //        : null;
        //}

    }
}