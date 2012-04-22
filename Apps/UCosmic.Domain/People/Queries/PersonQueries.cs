using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    internal static class PersonQueries
    {
        internal static Person ByEmail(this IQueryable<Person> queryable, string email)
        {
            return queryable.WithEmail(email, StringMatchStrategy.Equals).SingleOrDefault();
        }

        internal static IQueryable<Person> WithEmail(this IQueryable<Person> queryable, string term, StringMatchStrategy matchStrategy)
        {
            queryable = queryable.Where(EmailValueMatches(term, matchStrategy));
            if (matchStrategy == StringMatchStrategy.Equals)
            {
                var person = queryable.SingleOrDefault();
                queryable = person != null
                    ? new Collection<Person> { person }.AsQueryable()
                    : Enumerable.Empty<Person>().AsQueryable();
            }
            return queryable;
        }

        private static Expression<Func<Person, bool>> EmailValueMatches(string term, StringMatchStrategy matchStrategy)
        {
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return person => person.Emails.Any(email => email.Value.Equals(term, StringComparison.OrdinalIgnoreCase));

                case StringMatchStrategy.StartsWith:
                    return person => person.Emails.Any(email => email.Value.StartsWith(term, StringComparison.OrdinalIgnoreCase));

                case StringMatchStrategy.Contains:
                    return person => person.Emails.Any(email => email.Value.Contains(term));
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }

        internal static EmailAddress GetEmail(this Person owner, int number)
        {
            return owner != null
                ? owner.Emails.ByNumber(number)
                : null;
        }

        internal static EmailAddress GetDefaultEmail(this Person owner)
        {
            return owner != null
                ? owner.Emails.SingleOrDefault(e => e.IsDefault)
                : null;
        }

        internal static Affiliation GetAffiliation(this Person owner, int establishmentId)
        {
            return owner.Affiliations.ByEstablishmentId(establishmentId);
        }

        internal static IQueryable<Person> WithNonEmptySalutation(this IQueryable<Person> queryable)
        {
            return queryable.Where(p => p.Salutation != null && p.Salutation.Trim() != string.Empty);
        }

        internal static IQueryable<Person> WithNonEmptySuffix(this IQueryable<Person> queryable)
        {
            return queryable.Where(p => p.Suffix != null && p.Suffix.Trim() != string.Empty);
        }

        internal static IQueryable<string> SelectSalutations(this IQueryable<Person> queryable)
        {
            return queryable.Select(p => p.Salutation);
        }

        internal static IQueryable<string> SelectSuffixes(this IQueryable<Person> queryable)
        {
            return queryable.Select(p => p.Suffix);
        }

        internal static IQueryable<Person> WithFirstName(this IQueryable<Person> queryable, string term, StringMatchStrategy matchStrategy)
        {
            return queryable.Where(FirstNameMatches(term, matchStrategy));
        }

        private static Expression<Func<Person, bool>> FirstNameMatches(string term, StringMatchStrategy matchStrategy)
        {
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return person => person.FirstName.Equals(term, StringComparison.OrdinalIgnoreCase);

                case StringMatchStrategy.StartsWith:
                    return person => person.FirstName.StartsWith(term, StringComparison.OrdinalIgnoreCase);

                case StringMatchStrategy.Contains:
                    return person => person.FirstName.Contains(term);
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }

        internal static IQueryable<Person> WithLastName(this IQueryable<Person> queryable, string term, StringMatchStrategy matchStrategy)
        {
            return queryable.Where(LastNameMatches(term, matchStrategy));
        }

        private static Expression<Func<Person, bool>> LastNameMatches(string term, StringMatchStrategy matchStrategy)
        {
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return person => person.LastName.Equals(term, StringComparison.OrdinalIgnoreCase);

                case StringMatchStrategy.StartsWith:
                    return person => person.LastName.StartsWith(term, StringComparison.OrdinalIgnoreCase);

                case StringMatchStrategy.Contains:
                    return person => person.LastName.Contains(term);
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }
    }
}
