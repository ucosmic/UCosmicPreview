using System;
using NGeo.Yahoo.GeoPlanet;

namespace UCosmic.Domain.Places
{
    public class GetGeoNameIdByWoeIdQuery : IDefineQuery<int?>
    {
        public int WoeId { get; set; }
    }

    public class GetGeoNameIdByWoeIdHandler : IHandleQueries<GetGeoNameIdByWoeIdQuery, int?>
    {
        private readonly IContainGeoPlanet _geoPlanet;

        public GetGeoNameIdByWoeIdHandler(IContainGeoPlanet geoPlanet)
        {
            _geoPlanet = geoPlanet;
        }

        public int? Handle(GetGeoNameIdByWoeIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // try manual mappings first
            if (KnownWoeIds.ToGeoNameIds.ContainsKey(query.WoeId))
                return KnownWoeIds.ToGeoNameIds[query.WoeId];

            // try concordance
            var concordance = _geoPlanet.Concordance(
                ConcordanceNamespace.WoeId, query.WoeId);
            if (concordance != null && concordance.GeoNameId != 0 && concordance.WoeId != 0)
                return concordance.GeoNameId;

            return null;
        }
    }
}
