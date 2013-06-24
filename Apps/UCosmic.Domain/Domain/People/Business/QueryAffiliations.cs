using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    internal static class QueryAffiliations
    {
        internal static Affiliation ByEstablishmentId(this IEnumerable<Affiliation> enumerable, int establishmentId)
        {
            var affiliations = enumerable as Affiliation[] ?? enumerable.ToArray();
            if (affiliations.Count(x => x.EstablishmentId == establishmentId) > 1)
            {
                return affiliations.Count(x => x.EstablishmentId == establishmentId && x.IsDefault) == 1
                    ? affiliations.Single(x => x.EstablishmentId == establishmentId && x.IsDefault)
                    : affiliations.FirstOrDefault(x => x.EstablishmentId == establishmentId);
            }
            return affiliations.SingleOrDefault(x => x.EstablishmentId == establishmentId);
        }

        internal static Affiliation ByUserNameAndEstablishmentId(this IQueryable<Affiliation> queryable,
            string userName, int establishmentId)
        {
            if (queryable.Count(x => x.EstablishmentId == establishmentId && x.Person.User != null
                && x.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase)) > 1)
            {
                return queryable.Count(x => x.EstablishmentId == establishmentId && x.Person.User != null
                        && x.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.IsDefault) == 1
                    ? queryable.Single(x => x.EstablishmentId == establishmentId && x.Person.User != null
                        && x.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.IsDefault)
                    : queryable.FirstOrDefault(a =>  a.EstablishmentId == establishmentId && a.Person.User != null
                        && a.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            }
            return queryable.SingleOrDefault(a => a.EstablishmentId == establishmentId && a.Person.User != null && a.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
