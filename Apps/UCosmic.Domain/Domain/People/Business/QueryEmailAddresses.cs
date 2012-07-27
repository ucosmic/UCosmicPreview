using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    internal static class QueryEmailAddresses
    {
        internal static EmailAddress Default(this IEnumerable<EmailAddress> enumerable)
        {
            return enumerable != null ? enumerable.SingleOrDefault(email => email.IsDefault) : null;
        }

        internal static EmailAddress ByValue(this IEnumerable<EmailAddress> enumerable, string value)
        {
            return enumerable.SingleOrDefault(email => email.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        internal static EmailAddress ByUserNameAndNumber(this IQueryable<EmailAddress> queryable, string userName, int number)
        {
            return queryable.SingleOrDefault(email =>
                email.Person.User != null &&
                email.Person.User.Name != null &&
                email.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                email.Number == number);
        }

        internal static EmailAddress ByNumber(this IEnumerable<EmailAddress> enumerable, int number)
        {
            return enumerable.SingleOrDefault(email => email.Number == number);
        }

        internal static IEnumerable<EmailAddress> FromSaml(this IEnumerable<EmailAddress> enumerable)
        {
            return enumerable.Where(email => email.IsFromSaml);
        }

        internal static IEnumerable<EmailConfirmation> SelectManyConfirmations(this IEnumerable<EmailAddress> enumerable)
        {
            return enumerable.SelectMany(e => e.Confirmations);
        }

        internal static EmailConfirmation ByToken(this IEnumerable<EmailConfirmation> enumerable, Guid token)
        {
            return enumerable.SingleOrDefault(c => c.Token == token);
        }
    }
}
