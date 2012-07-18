using System;

namespace UCosmic.Domain.Places
{
    public class GetGeoNamesFeatureClassByCodeQuery : BaseEntityQuery<GeoNamesFeatureClass>, IDefineQuery<GeoNamesFeatureClass>
    {
        public string Code { get; set; }
    }

    public class GetGeoNamesFeatureClassByCodeHandler : IHandleQueries<GetGeoNamesFeatureClassByCodeQuery, GeoNamesFeatureClass>
    {
        private readonly IQueryEntities _entities;

        public GetGeoNamesFeatureClassByCodeHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public GeoNamesFeatureClass Handle(GetGeoNamesFeatureClassByCodeQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.GeoNamesFeatureClasses
                .EagerLoad(query.EagerLoad, _entities)
                .ByCode(query.Code)
            ;

            return result;
        }
    }
}
