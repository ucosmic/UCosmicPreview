using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    public static class QueryAffiliations
    {
        public static Affiliation ByEstablishmentId(this IEnumerable<Affiliation> enumerable, int establishmentId)
        {
            return enumerable.SingleOrDefault(affiliation => affiliation.EstablishmentId == establishmentId);
        }
    }
}
