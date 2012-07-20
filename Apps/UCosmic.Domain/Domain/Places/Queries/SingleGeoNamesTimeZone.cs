using System;

namespace UCosmic.Domain.Places
{
    public class SingleGeoNamesTimeZone : IDefineQuery<GeoNamesTimeZone>
    {
        public SingleGeoNamesTimeZone(string timeZoneId)
        {
            TimeZoneId = timeZoneId;
        }

        public string TimeZoneId { get; private set; }
    }

    public class SingleGeoNamesTimeZoneHandler : IHandleQueries<SingleGeoNamesTimeZone, GeoNamesTimeZone>
    {
        private readonly IQueryEntities _entities;

        public SingleGeoNamesTimeZoneHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public GeoNamesTimeZone Handle(SingleGeoNamesTimeZone query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.FindByPrimaryKey<GeoNamesTimeZone>(query.TimeZoneId);
            return result;
        }
    }
}
