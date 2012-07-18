using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class QueryGeoPlanetPlaceTypes
    {
        internal static GeoPlanetPlaceType ByCode(this IQueryable<GeoPlanetPlaceType> queryable, int code)
        {
            return queryable.SingleOrDefault(p => p.Code == code);
        }
    }
}
