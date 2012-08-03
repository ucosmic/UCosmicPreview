using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace UCosmic.Domain.Places
{
    public class GetPlaceByWoeIdQuery : BaseEntityQuery<Place>, IDefineQuery<Place>
    {
        public int WoeId { get; set; }
    }

    public class GetPlaceByWoeIdHandler : IHandleQueries<GetPlaceByWoeIdQuery, Place>
    {
        private static readonly object Lock = new object();
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;

        public GetPlaceByWoeIdHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IUnitOfWork unitOfWork
        )
        {
            _entities = entities;
            _queryProcessor = queryProcessor;
            _unitOfWork = unitOfWork;
        }

        public Place Handle(GetPlaceByWoeIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            lock (Lock)
            {
                // first look in the db
                //var place = _places.FindOne(PlaceBy.WoeId(woeId).ForInsertOrUpdate());
                var place = _entities.Get<Place>()
                    .EagerLoad(_entities, query.EagerLoad)
                    .ByWoeId(query.WoeId)
                ;
                if (place != null) return place;

                // load toponym from storage
                //var woe = _geoPlanetPlaces.FindOne(woeId);
                var woe = _queryProcessor.Execute(new SingleGeoPlanetPlace(query.WoeId));

                // convert to entity
                place = woe.ToPlace();

                // continent Australia should be named Oceania
                if (woe.IsContinent && woe.EnglishName == "Australia")
                    place.OfficialName = "Oceania";

                // try to match to geonames
                if (place.GeoNamesToponym == null && place.GeoPlanetPlace != null)
                {
                    //var geoNameId = _geoPlanetPlaces.FindGeoNameId(place.GeoPlanetPlace.WoeId);
                    var geoNameId = _queryProcessor.Execute(
                        new GeoNameIdByWoeId(place.GeoPlanetPlace.WoeId));
                    if (geoNameId.HasValue)
                    {
                        //place.GeoNamesToponym = _toponyms.FindOne(geoNameId.Value);
                        place.GeoNamesToponym = _queryProcessor.Execute(
                            new SingleGeoNamesToponym(geoNameId.Value));
                        if (place.GeoNamesToponym != null)
                        {
                            place.Names = place.GeoNamesToponym.AlternateNames.ToEntities(_entities);
                        }
                    }
                }

                // configure hierarchy
                if (woe.Parent != null)
                {
                    //place.Parent = FromWoeId(woe.Parent.WoeId);
                    place.Parent = Handle(
                        new GetPlaceByWoeIdQuery
                        {
                            WoeId = woe.Parent.WoeId,
                        });
                }

                // when no parent exists, map country to continent
                else if (woe.IsCountry)
                {
                    if (place.GeoNamesToponym != null)
                    {
                        var geoNamesContinent =
                            place.GeoNamesToponym.Ancestors.Select(a => a.Ancestor)
                                .SingleOrDefault(
                                    g =>
                                        g.FeatureCode == GeoNamesFeatureEnum.Continent.GetCode() &&
                                        g.Feature.ClassCode == GeoNamesFeatureClassEnum.Area.GetCode());
                        if (geoNamesContinent != null)
                        {
                            //place.Parent = FromGeoNameId(geoNamesContinent.GeoNameId);
                            place.Parent = _queryProcessor.Execute(
                                new GetPlaceByGeoNameIdInternal(geoNamesContinent.GeoNameId));
                        }
                    }
                    else
                    {
                        var geoPlanetContinent = woe.BelongTos.Select(b => b.BelongsTo)
                            .FirstOrDefault(c => c.Type.Code == (int)GeoPlanetPlaceTypeEnum.Continent);
                        if (geoPlanetContinent != null)
                        {
                            //place.Parent = FromWoeId(geoPlanetContinent.WoeId);
                            place.Parent = Handle(
                                new GetPlaceByWoeIdQuery
                                {
                                    WoeId = geoPlanetContinent.WoeId,
                                });
                        }
                    }
                }

                // when no parent exists, map continent to earth
                else if (woe.IsContinent)
                {
                    //place.Parent = FromWoeId(GeoPlanetPlace.EarthWoeId);
                    place.Parent = Handle(
                        new GetPlaceByWoeIdQuery
                        {
                            WoeId = GeoPlanetPlace.EarthWoeId,
                        }
                    );
                }

                // map ancestors
                DeriveNodes(place);

                // add to db & save
                _entities.Create(place);
                _unitOfWork.SaveChanges();

                return place;
            }
        }

        private void DeriveNodes(Place entity)
        {
            entity.Ancestors = entity.Ancestors ?? new Collection<PlaceNode>();
            entity.Offspring = entity.Offspring ?? new Collection<PlaceNode>();

            entity.Ancestors.ToList().ForEach(node =>
                _entities.Purge(node));

            var separation = 1;
            var parent = entity.Parent;
            while (parent != null)
            {
                entity.Ancestors.Add(new PlaceNode
                {
                    Ancestor = parent,
                    Separation = separation++,
                });
                parent = parent.Parent;
            }
        }
    }
}
