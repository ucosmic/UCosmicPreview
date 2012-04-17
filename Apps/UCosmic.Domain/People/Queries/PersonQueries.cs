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
    }
}
