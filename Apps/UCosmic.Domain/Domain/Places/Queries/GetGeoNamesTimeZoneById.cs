using System;

namespace UCosmic.Domain.Places
{
    public class GetGeoNamesTimeZoneByIdQuery : BaseEntityQuery<GeoNamesTimeZone>, IDefineQuery<GeoNamesTimeZone>
    {
        public string TimeZoneId { get; set; }
    }

    public class GetGeoNamesTimeZoneByIdHandler : IHandleQueries<GetGeoNamesTimeZoneByIdQuery, GeoNamesTimeZone>
    {
        private readonly IQueryEntities _entities;

        public GetGeoNamesTimeZoneByIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public GeoNamesTimeZone Handle(GetGeoNamesTimeZoneByIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.GeoNamesTimeZones
                .EagerLoad(query.EagerLoad, _entities)
                .ByTimeZoneId(query.TimeZoneId)
            ;

            return result;
        }
    }
}
