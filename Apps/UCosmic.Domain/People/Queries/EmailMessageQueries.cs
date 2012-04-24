using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    internal static class EmailMessageQueries
    {
        internal static EmailMessage ByNumber(this IEnumerable<EmailMessage> enumerable, int number)
        {
            return enumerable.SingleOrDefault(message => message.Number == number);
        }
    }
}
