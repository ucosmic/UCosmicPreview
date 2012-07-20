using System;
using System.Linq;
using NGeo.Yahoo.GeoPlanet;

namespace UCosmic.Domain.Places
{
    public class SingleGeoPlanetPlaceByGeoNameId : IDefineQuery<GeoPlanetPlace>
    {
        public SingleGeoPlanetPlaceByGeoNameId(int geoNameId)
        {
            GeoNameId = geoNameId;
        }

        public int GeoNameId { get; private set; }
    }

    public class SingleGeoPlanetPlaceByGeoNameIdHandler : IHandleQueries<SingleGeoPlanetPlaceByGeoNameId, GeoPlanetPlace>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IContainGeoPlanet _geoPlanet;

        public SingleGeoPlanetPlaceByGeoNameIdHandler(IProcessQueries queryProcessor, IContainGeoPlanet geoPlanet)
        {
            _queryProcessor = queryProcessor;
            _geoPlanet = geoPlanet;
        }

        public GeoPlanetPlace Handle(SingleGeoPlanetPlaceByGeoNameId query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // try manual mappings first
            if (KnownWoeIds.ToGeoNameIds.ContainsValue(query.GeoNameId))
                return _queryProcessor.Execute(new SingleGeoPlanetPlace(
                    KnownWoeIds.ToGeoNameIds.Single(d => d.Value == query.GeoNameId).Key));

            // try concordance
            var concordance = _geoPlanet.Concordance(
                ConcordanceNamespace.GeoNames, query.GeoNameId);
            if (concordance != null && concordance.GeoNameId != 0 && concordance.WoeId != 0)
                return _queryProcessor.Execute(
                    new SingleGeoPlanetPlace(concordance.WoeId));

            return null;
        }
    }
}
