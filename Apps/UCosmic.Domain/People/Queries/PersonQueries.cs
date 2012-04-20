using System;
using System.Linq;

namespace UCosmic.Domain.People
{
    internal static class PersonQueries
    {
        internal static Person ByEmail(this IQueryable<Person> queryable, string email)
        {
            var person = queryable.SingleOrDefault
            (
                p => 
                p.Emails.Any
                (
                    e => 
                    e.Value.Equals(email, StringComparison.OrdinalIgnoreCase)
                )
            );
            return person;
        }

        internal static EmailAddress GetEmail(this Person owner, int number)
        {
            return owner.Emails.ByNumber(number);
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
    }
}
