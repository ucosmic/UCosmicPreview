using System;
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

        internal static Affiliation ByUserNameAndEstablishmentId(this IQueryable<Affiliation> queryable,
            string userName, int establishmentId)
        {
            return queryable.SingleOrDefault(a =>
                a.EstablishmentId == establishmentId &&
                a.Person.User != null && a.Person.User.Name.Equals(userName,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
