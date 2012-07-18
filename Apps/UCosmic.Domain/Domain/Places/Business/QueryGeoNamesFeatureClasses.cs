using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class QueryGeoNamesFeatureClasses
    {
        internal static GeoNamesFeatureClass ByCode(this IQueryable<GeoNamesFeatureClass> queryable, string code)
        {
            return queryable.SingleOrDefault(p => p.Code == code);
        }
    }
}
