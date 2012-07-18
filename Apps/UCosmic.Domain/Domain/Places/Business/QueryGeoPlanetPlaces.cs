using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class QueryGeoPlanetPlaces
    {
        internal static GeoPlanetPlace ByWoeId(this IQueryable<GeoPlanetPlace> queryable, int woeId)
        {
            return queryable.SingleOrDefault(p => p.WoeId == woeId);
        }
    }
}
