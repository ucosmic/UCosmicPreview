using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class QueryGeoNamesTimeZones
    {
        internal static GeoNamesTimeZone ByTimeZoneId(this IQueryable<GeoNamesTimeZone> queryable, string timeZoneId)
        {
            return queryable.SingleOrDefault(p => p.Id == timeZoneId);
        }
    }
}
