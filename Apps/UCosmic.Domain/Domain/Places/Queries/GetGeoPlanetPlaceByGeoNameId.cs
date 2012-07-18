using System;
using System.Linq;
using NGeo.Yahoo.GeoPlanet;

namespace UCosmic.Domain.Places
{
    public class GetGeoPlanetPlaceByGeoNameIdQuery : BaseEntityQuery<GeoPlanetPlace>, IDefineQuery<GeoPlanetPlace>
    {
        public int GeoNameId { get; set; }
    }

    public class GetGeoPlanetPlaceByGeoNameIdHandler : IHandleQueries<GetGeoPlanetPlaceByGeoNameIdQuery, GeoPlanetPlace>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IContainGeoPlanet _geoPlanet;

        public GetGeoPlanetPlaceByGeoNameIdHandler(IProcessQueries queryProcessor, IContainGeoPlanet geoPlanet)
        {
            _queryProcessor = queryProcessor;
            _geoPlanet = geoPlanet;
        }

        public GeoPlanetPlace Handle(GetGeoPlanetPlaceByGeoNameIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // try manual mappings first
            if (KnownWoeIds.ToGeoNameIds.ContainsValue(query.GeoNameId))
                //return FindOne(KnownWoeIds.ToGeoNameIds.Single(d => d.Value == geoNameId).Key);
                return _queryProcessor.Execute(
                    new GetGeoPlanetPlaceByWoeIdQuery
                    {
                        WoeId = KnownWoeIds.ToGeoNameIds.Single(d => d.Value == query.GeoNameId).Key,
                    });

            // try concordance
            var concordance = _geoPlanet.Concordance(
                ConcordanceNamespace.GeoNames, query.GeoNameId);
            if (concordance != null && concordance.GeoNameId != 0 && concordance.WoeId != 0)
                //return FindOne(concordance.WoeId);
                return _queryProcessor.Execute(
                    new GetGeoPlanetPlaceByWoeIdQuery
                    {
                        WoeId = concordance.WoeId,
                    });

            return null;
        }
    }
}
