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

        internal static EmailAddress ByNumber(this IEnumerable<EmailAddress> enumerable, int number)
        {
            return enumerable.SingleOrDefault(email => email.Number == number);
        }

        public static IEnumerable<EmailAddress> FromSaml(this IEnumerable<EmailAddress> enumerable)
        {
            return enumerable.Where(email => email.IsFromSaml);
        }

        public static IEnumerable<EmailConfirmation> SelectManyConfirmations(this IEnumerable<EmailAddress> enumerable)
        {
            return enumerable.SelectMany(e => e.Confirmations);
        }

        public static EmailConfirmation ByToken(this IEnumerable<EmailConfirmation> enumerable, Guid token)
        {
            return enumerable.SingleOrDefault(c => c.Token == token);
        }
    }
}
