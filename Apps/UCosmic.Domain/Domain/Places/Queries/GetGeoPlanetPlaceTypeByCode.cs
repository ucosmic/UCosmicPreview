using System;
using NGeo.Yahoo.GeoPlanet;

namespace UCosmic.Domain.Places
{
    public class GetGeoPlanetPlaceTypeByCodeQuery : BaseEntityQuery<GeoPlanetPlaceType>, IDefineQuery<GeoPlanetPlaceType>
    {
        public int Code { get; set; }
    }

    public class GetGeoPlanetPlaceTypeByCodeHandler : IHandleQueries<GetGeoPlanetPlaceTypeByCodeQuery, GeoPlanetPlaceType>
    {
        private readonly IQueryEntities _entities;
        private readonly IContainGeoPlanet _geoPlanet;

        public GetGeoPlanetPlaceTypeByCodeHandler(IQueryEntities entities, IContainGeoPlanet geoPlanet)
        {
            _entities = entities;
            _geoPlanet = geoPlanet;
        }

        public GeoPlanetPlaceType Handle(GetGeoPlanetPlaceTypeByCodeQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<GeoPlanetPlaceType>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByCode(query.Code)
            ;

            if (result == null)
                result = _geoPlanet.Type(query.Code, RequestView.Long).ToEntity();

            return result;
        }
    }
}
