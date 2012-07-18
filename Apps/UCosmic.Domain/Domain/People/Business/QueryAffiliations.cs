using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    internal static class QueryAffiliations
    {
        internal static Affiliation ByEstablishmentId(this IEnumerable<Affiliation> enumerable, int establishmentId)
        {
            return enumerable.SingleOrDefault(affiliation => affiliation.EstablishmentId == establishmentId);
        }
    }
}
