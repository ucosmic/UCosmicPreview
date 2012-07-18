using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NGeo.Yahoo.GeoPlanet;

namespace UCosmic.Domain.Places
{
    public class GetGeoPlanetPlaceByWoeIdQuery : BaseEntityQuery<GeoPlanetPlace>, IDefineQuery<GeoPlanetPlace>
    {
        public int WoeId { get; set; }
    }

    public class GetGeoPlanetPlaceByWoeIdHandler : IHandleQueries<GetGeoPlanetPlaceByWoeIdQuery, GeoPlanetPlace>
    {
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContainGeoPlanet _geoPlanet;

        public GetGeoPlanetPlaceByWoeIdHandler(ICommandEntities entities, IUnitOfWork unitOfWork, IContainGeoPlanet geoPlanet)
        {
            _entities = entities;
            _unitOfWork = unitOfWork;
            _geoPlanet = geoPlanet;
        }

        public GeoPlanetPlace Handle(GetGeoPlanetPlaceByWoeIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // first look in the db
            var place = _entities.GeoPlanetPlaces
                .EagerLoad(query.EagerLoad, _entities)
                .ByWoeId(query.WoeId)
            ;
            if (place != null) return place;

            // invoke geoplanet service
            var geoPlanetPlace = _geoPlanet.Place(query.WoeId);
            if (geoPlanetPlace == null) return null;

            // convert yahoo type to entity
            place = geoPlanetPlace.ToEntity();

            // map parent
            var ancestors = _geoPlanet.Ancestors(query.WoeId, RequestView.Long);
            if (ancestors != null && ancestors.Items.Count > 0)
            {
                //place.Parent = FindOne(ancestors.First().WoeId);
                place.Parent = Handle(
                    new GetGeoPlanetPlaceByWoeIdQuery
                    {
                        WoeId = ancestors.First().WoeId,
                    });
            }

            // add all belongtos
            place.BelongTos = place.BelongTos ?? new List<GeoPlanetPlaceBelongTo>();
            var geoPlanetBelongTos = _geoPlanet.BelongTos(query.WoeId);
            if (geoPlanetBelongTos != null && geoPlanetBelongTos.Items.Count > 0)
            {
                var rank = 0;
                foreach (var geoPlanetBelongTo in geoPlanetBelongTos.Items)
                {
                    place.BelongTos.Add(new GeoPlanetPlaceBelongTo
                    {
                        Rank = rank++,
                        //BelongsTo = FindOne(geoPlanetBelongTo.WoeId)
                        BelongsTo = Handle(
                            new GetGeoPlanetPlaceByWoeIdQuery
                            {
                                WoeId = geoPlanetBelongTo.WoeId,
                            }
                        )
                    });
                }
            }

            // ensure no duplicate place types are added to db
            //place.Type = EntityQueries.FindByPrimaryKey(EntityQueries.GeoPlanetPlaceTypes, place.Type.Code)
            //    ?? _geoPlanet.Type(place.Type.Code, _config.GeoPlanetAppId, RequestView.Long)
            //        .ToEntity();
            place.Type = new GetGeoPlanetPlaceTypeByCodeHandler(_entities, _geoPlanet)
                .Handle(
                    new GetGeoPlanetPlaceTypeByCodeQuery
                    {
                        Code = place.Type.Code,
                    });

            // map ancestors
            DeriveNodes(place);

            // add to db and save
            _entities.Create(place);
            _unitOfWork.SaveChanges();

            return place;
        }

        private void DeriveNodes(GeoPlanetPlace place)
        {
            place.Ancestors = place.Ancestors ?? new Collection<GeoPlanetPlaceNode>();
            place.Offspring = place.Offspring ?? new Collection<GeoPlanetPlaceNode>();

            place.Ancestors.ToList().ForEach(node =>
                _entities.Purge(node));

            var separation = 1;
            var parent = place.Parent;
            while (parent != null)
            {
                place.Ancestors.Add(new GeoPlanetPlaceNode
                {
                    Ancestor = parent,
                    Separation = separation++,
                });
                parent = parent.Parent;
            }
        }
    }
}
