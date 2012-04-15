using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    public static class EmailAddressQueries
    {
        public static EmailAddress ByValue(this IEnumerable<EmailAddress> enumerable, string value)
        {
            return enumerable.SingleOrDefault(email => email.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<EmailConfirmation> SelectManyConfirmations(this IEnumerable<EmailAddress> enumerable)
        {
            return enumerable.SelectMany(e => e.Confirmations);
        }
    }
}
