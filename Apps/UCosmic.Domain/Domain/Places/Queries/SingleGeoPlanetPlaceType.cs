using System;
using NGeo.Yahoo.GeoPlanet;

namespace UCosmic.Domain.Places
{
    public class SingleGeoPlanetPlaceType : IDefineQuery<GeoPlanetPlaceType>
    {
        public SingleGeoPlanetPlaceType(int code)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }

    public class SingleGeoPlanetPlaceTypeHandler : IHandleQueries<SingleGeoPlanetPlaceType, GeoPlanetPlaceType>
    {
        private readonly ICommandEntities _entities;
        private readonly IContainGeoPlanet _geoPlanet;

        public SingleGeoPlanetPlaceTypeHandler(ICommandEntities entities, IContainGeoPlanet geoPlanet)
        {
            _entities = entities;
            _geoPlanet = geoPlanet;
        }

        public GeoPlanetPlaceType Handle(SingleGeoPlanetPlaceType query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.FindByPrimaryKey<GeoPlanetPlaceType>(query.Code);
            if (result != null) return result;

            result = _geoPlanet.Type(query.Code, RequestView.Long).ToEntity();
            _entities.Create(result);

            return result;
        }
    }
}
