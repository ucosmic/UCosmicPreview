using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class QueryGeoNamesToponyms
    {
        internal static GeoNamesToponym ByGeoNameId(this IQueryable<GeoNamesToponym> queryable, int id)
        {
            return queryable.SingleOrDefault(p => p.GeoNameId == id);
        }
    }
}
