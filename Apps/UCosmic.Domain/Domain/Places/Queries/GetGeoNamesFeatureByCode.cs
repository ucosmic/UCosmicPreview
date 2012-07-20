using System;

namespace UCosmic.Domain.Places
{
    public class GetGeoNamesFeatureByCodeQuery : BaseEntityQuery<GeoNamesFeature>, IDefineQuery<GeoNamesFeature>
    {
        public string Code { get; set; }
    }

    public class GetGeoNamesFeatureByCodeHandler : IHandleQueries<GetGeoNamesFeatureByCodeQuery, GeoNamesFeature>
    {
        private readonly IQueryEntities _entities;

        public GetGeoNamesFeatureByCodeHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public GeoNamesFeature Handle(GetGeoNamesFeatureByCodeQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<GeoNamesFeature>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByCode(query.Code)
            ;

            return result;
        }
    }
}
