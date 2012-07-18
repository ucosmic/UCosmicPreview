using System;

namespace UCosmic.Domain.Places
{
    public class GetPlaceByGeoNameIdQuery : BaseEntityQuery<Place>, IDefineQuery<Place>
    {
        public int GeoNameId { get; set; }
    }

    public class GetPlaceByGeoNameIdHandler : IHandleQueries<GetPlaceByGeoNameIdQuery, Place>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;

        public GetPlaceByGeoNameIdHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IUnitOfWork unitOfWork
        )
        {
            _entities = entities;
            _queryProcessor = queryProcessor;
            _unitOfWork = unitOfWork;
        }

        public Place Handle(GetPlaceByGeoNameIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // first look in the db
            //var place = _places.FindOne(PlaceBy.GeoNameId(geoNameId).ForInsertOrUpdate());
            var place = _entities.Places
                .EagerLoad(query.EagerLoad, _entities)
                .ByGeoNameId(query.GeoNameId)
            ;
            if (place != null) return place;

            // load toponym from storage
            //var toponym = _toponyms.FindOne(geoNameId);
            var toponym = _queryProcessor.Execute(
                new GetGeoNamesToponymByGeoNameIdQuery
                {
                    GeoNameId = query.GeoNameId,
                });

            // convert toponym to place
            place = toponym.ToPlace();

            // match place hierarchy to toponym hierarchy
            if (toponym.Parent != null)
            {
                //place.Parent = FromGeoNameId(toponym.Parent.GeoNameId);
                place.Parent = Handle(
                    new GetPlaceByGeoNameIdQuery
                    {
                        GeoNameId = toponym.Parent.GeoNameId,
                    });
            }

            // try to match to geoplanet
            if (place.GeoPlanetPlace == null && place.GeoNamesToponym != null)
            {
                //place.GeoPlanetPlace = _geoPlanetPlaces.FindByGeoNameId(place.GeoNamesToponym.GeoNameId);
                place.GeoPlanetPlace = _queryProcessor.Execute(
                    new GetGeoPlanetPlaceByGeoNameIdQuery
                    {
                        GeoNameId = place.GeoNamesToponym.GeoNameId,
                    });
            }

            // add to db & save
            _entities.Create(place);
            _unitOfWork.SaveChanges();

            return place;
        }
    }
}
