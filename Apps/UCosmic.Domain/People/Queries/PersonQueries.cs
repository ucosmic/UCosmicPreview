using System;
using System.Collections.Generic;
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

        internal static int NextNumber(this ICollection<EmailMessage> collection)
        {
            return collection.Any() ? collection.Max(w => w.Number) + 1 : 1;
        }

        internal static int NextNumber(this ICollection<EmailAddress> collection)
        {
            return collection.Any() ? collection.Max(w => w.Number) + 1 : 1;
        }

    }
}
