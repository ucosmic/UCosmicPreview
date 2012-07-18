using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class QueryGeoNamesFeatures
    {
        internal static GeoNamesFeature ByCode(this IQueryable<GeoNamesFeature> queryable, string code)
        {
            return queryable.SingleOrDefault(p => p.Code == code);
        }
    }
}
