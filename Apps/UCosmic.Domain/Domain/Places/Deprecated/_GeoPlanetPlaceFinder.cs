//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using NGeo.Yahoo.GeoPlanet;

//namespace UCosmic.Domain.Places
//{
//    public class GeoPlanetPlaceFinder : EntityFinder<GeoPlanetPlace>
//    {
//        private readonly IConsumeGeoPlanet _geoPlanet;
//        private readonly IManageConfigurations _config;
//        private readonly ICommandObjects _objectCommander;

//        //private static readonly Dictionary<int, int> KnownWoeIdsToGeoNameIds = new Dictionary<int, int>
//        //{
//        //    { GeoPlanetPlace.EarthWoeId, GeoNamesToponym.EarthGeoNameId }, // earth
//        //    { 24865670, 6255146 },  // Africa
//        //    { 24865671, 6255147 },  // Asia
//        //    { 24865675, 6255148 },  // Europe
//        //    { 24865672, 6255149 },  // North America
//        //    { 24865673, 6255150 },  // South America
//        //    { 55949070, 6255151 },  // Oceania / Australia
//        //    { 28289421, 6255152 },  // Antarctic(a) / South Pole continent
//        //    { 12577865, 661882 },   // Aland Islands
//        //    { 28289409, 6697173 },  // Antarctica country
//        //    { 23424886, 1024031 },  // Mayotte country
//        //    { 28289413, 607072 },   // Svalbard and Jan Mayen country
//        //    { 23424990, 2461445 },  // Western Sahara country
//        //    { 23424749, 2077507 },  // Ashmore and Cartier Islands
//        //    { 23424790, 2170371 },  // Coral Sea Islands
//        //    { 23424920, 1821073 },  // Paracel Islands
//        //    { 56120896, 7603259 },  // Luxor
//        //    { 23424847, 3042225 },  // Isle of Man
//        //    { 23424788, 4041468 },  // Northern Mariana Islands
//        //};

//        public GeoPlanetPlaceFinder(IQueryEntities entityQueries, ICommandObjects objectCommander, IConsumeGeoPlanet geoPlanet, IManageConfigurations config)
//            : base(entityQueries)
//        {
//            _geoPlanet = geoPlanet;
//            _config = config;
//            _objectCommander = objectCommander;
//        }

//        public override ICollection<GeoPlanetPlace> FindMany(EntityQueryCriteria<GeoPlanetPlace> criteria)
//        {
//            var query = InitializeQuery(EntityQueries.GeoPlanetPlaces, criteria);
//            var finder = criteria as GeoPlanetPlaceQuery ?? new GeoPlanetPlaceQuery();

//            // apply WoeId
//            if (finder.WoeId.HasValue)
//                return new[] { query.SingleOrDefault(e => e.WoeId == finder.WoeId.Value) };

//            query = FinalizeQuery(query, criteria);

//            var results = query.ToList();
//            return results;
//        }

//        public GeoPlanetPlace FindOne(int woeId)
//        {
//            // first look in the db
//            var place = FindOne(GeoPlanetPlaceBy.WoeId(woeId));

//            // if not in db, add it
//            if (place == null)
//            {
//                // invoke geoplanet service
//                var geoPlanetPlace = _geoPlanet.Place(woeId, _config.GeoPlanetAppId);
//                if (geoPlanetPlace == null) return null;

//                // convert yahoo type to entity
//                place = geoPlanetPlace.ToEntity();

//                // map parent
//                var ancestors = _geoPlanet.Ancestors(woeId, _config.GeoPlanetAppId, RequestView.Long);
//                if (ancestors != null && ancestors.Items.Count > 0)
//                {
//                    place.Parent = FindOne(ancestors.First().WoeId);
//                }

//                // add all belongtos
//                place.BelongTos = place.BelongTos ?? new List<GeoPlanetPlaceBelongTo>();
//                var geoPlanetBelongTos = _geoPlanet.BelongTos(woeId, _config.GeoPlanetAppId);
//                if (geoPlanetBelongTos != null && geoPlanetBelongTos.Items.Count > 0)
//                {
//                    var rank = 0;
//                    foreach (var geoPlanetBelongTo in geoPlanetBelongTos.Items)
//                    {
//                        place.BelongTos.Add(new GeoPlanetPlaceBelongTo
//                        {
//                            Rank = rank++,
//                            BelongsTo = FindOne(geoPlanetBelongTo.WoeId)
//                        });
//                    }
//                }

//                // ensure no duplicate place types are added to db
//                place.Type = EntityQueries.FindByPrimaryKey(EntityQueries.GeoPlanetPlaceTypes, place.Type.Code)
//                    ?? _geoPlanet.Type(place.Type.Code, _config.GeoPlanetAppId, RequestView.Long)
//                        .ToEntity();

//                // map ancestors
//                DeriveNodes(place);

//                // add to db and save
//                _objectCommander.Insert(place, true);
//            }

//            return place;
//        }

//        public GeoPlanetPlace FindByGeoNameId(int geoNameId)
//        {
//            // try manual mappings first
//            if (KnownWoeIds.ToGeoNameIds.ContainsValue(geoNameId))
//                return FindOne(KnownWoeIds.ToGeoNameIds.Single(d => d.Value == geoNameId).Key);

//            // try concordance
//            var concordance = _geoPlanet.Concordance(
//                ConcordanceNamespace.GeoNames, geoNameId, _config.GeoPlanetAppId);
//            if (concordance != null && concordance.GeoNameId != 0 && concordance.WoeId != 0)
//                return FindOne(concordance.WoeId);

//            return null;
//        }

//        public int? FindGeoNameId(int woeId)
//        {
//            // try manual mappings first
//            if (KnownWoeIds.ToGeoNameIds.ContainsKey(woeId))
//                return KnownWoeIds.ToGeoNameIds[woeId];

//            // try concordance
//            var concordance = _geoPlanet.Concordance(
//                ConcordanceNamespace.WoeId, woeId, _config.GeoPlanetAppId);
//            if (concordance != null && concordance.GeoNameId != 0 && concordance.WoeId != 0)
//                return concordance.GeoNameId;

//            return null;
//        }

//        private void DeriveNodes(GeoPlanetPlace place)
//        {
//            place.Ancestors = place.Ancestors ?? new Collection<GeoPlanetPlaceNode>();
//            place.Offspring = place.Offspring ?? new Collection<GeoPlanetPlaceNode>();

//            place.Ancestors.ToList().ForEach(node =>
//                _objectCommander.Delete(node));

//            var separation = 1;
//            var parent = place.Parent;
//            while (parent != null)
//            {
//                place.Ancestors.Add(new GeoPlanetPlaceNode
//                {
//                    Ancestor = parent,
//                    Separation = separation++,
//                });
//                parent = parent.Parent;
//            }
//        }
//    }
//}