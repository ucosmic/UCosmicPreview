//using System.Linq;
//using UCosmic.Domain.Languages;
//using NGeo.GeoNames;
//using NGeo.Yahoo.GeoPlanet;

//namespace UCosmic.Domain.Places
//{
//    public class PlaceFactory
//    {
//        private readonly PlaceFinder _places;
//        private readonly PlaceChanger _placeChanger;
//        private readonly GeoNamesToponymFinder _toponyms;
//        private readonly GeoPlanetPlaceFinder _geoPlanetPlaces;
//        private readonly LanguageFinder _languages;
//        private static readonly object Lock = new object();

//        public PlaceFactory(IQueryEntities entityQueries, ICommandObjects objectCommander, 
//            IConsumeGeoPlanet geoPlanet, IConsumeGeoNames geoNames, IManageConfigurations config)
//        {
//            _places = new PlaceFinder(entityQueries);
//            _placeChanger = new PlaceChanger(objectCommander);
//            _toponyms = new GeoNamesToponymFinder(entityQueries, objectCommander, geoNames, config);
//            _geoPlanetPlaces = new GeoPlanetPlaceFinder(entityQueries, objectCommander, geoPlanet, config);
//            _languages = new LanguageFinder(entityQueries);
//        }

//        //public Place FromWoeId(int woeId)
//        //{
//        //    lock (Lock)
//        //    {
//        //        // woe id's are positive
//        //        if (woeId == 0) return null;

//        //        // first look in the db
//        //        var place = _places.FindOne(PlaceBy.WoeId(woeId).ForInsertOrUpdate());

//        //        // if place does not exist, create from yahoo
//        //        if (place == null)
//        //        {
//        //            // load toponym from storage
//        //            var woe = _geoPlanetPlaces.FindOne(woeId);

//        //            // convert toponym to place
//        //            place = woe.ToPlace();

//        //            // continent Australia should be named Oceania
//        //            if (woe.IsContinent && woe.EnglishName == "Australia")
//        //                place.OfficialName = "Oceania";

//        //            // try to match to geonames
//        //            if (place.GeoNamesToponym == null && place.GeoPlanetPlace != null)
//        //            {
//        //                var geoNameId = _geoPlanetPlaces.FindGeoNameId(place.GeoPlanetPlace.WoeId);
//        //                if (geoNameId.HasValue)
//        //                {
//        //                    place.GeoNamesToponym = _toponyms.FindOne(geoNameId.Value);
//        //                    if (place.GeoNamesToponym != null)
//        //                    {
//        //                        place.Names = place.GeoNamesToponym.AlternateNames.ToEntities(_languages);
//        //                    }
//        //                }
//        //            }

//        //            // configure hierarchy
//        //            if (woe.Parent != null)
//        //            {
//        //                place.Parent = FromWoeId(woe.Parent.WoeId);
//        //            }

//        //            // when no parent exists, map country to continent
//        //            else if (woe.IsCountry)
//        //            {
//        //                if (place.GeoNamesToponym != null)
//        //                {
//        //                    var geoNamesContinent =
//        //                        place.GeoNamesToponym.Ancestors.Select(a => a.Ancestor)
//        //                            .SingleOrDefault(g => g.FeatureCode == GeoNamesFeatureEnum.Continent.GetCode()
//        //                                                  &&
//        //                                                  g.Feature.ClassCode == GeoNamesFeatureClassEnum.Area.GetCode());
//        //                    if (geoNamesContinent != null)
//        //                    {
//        //                        place.Parent = FromGeoNameId(geoNamesContinent.GeoNameId);
//        //                    }
//        //                }
//        //                else
//        //                {
//        //                    var geoPlanetContinent = woe.BelongTos.Select(b => b.BelongsTo)
//        //                        .FirstOrDefault(c => c.Type.Code == (int)GeoPlanetPlaceTypeEnum.Continent);
//        //                    if (geoPlanetContinent != null)
//        //                    {
//        //                        place.Parent = FromWoeId(geoPlanetContinent.WoeId);
//        //                    }
//        //                }
//        //            }

//        //            // when no parent exists, map continent to earth
//        //            else if (woe.IsContinent)
//        //            {
//        //                place.Parent = FromWoeId(GeoPlanetPlace.EarthWoeId);
//        //            }

//        //            // add to db & save
//        //            _placeChanger.Create(place, true);
//        //        }

//        //        return place;
//        //    }
//        //}

//        //// ReSharper disable MemberCanBePrivate.Global
//        //public Place FromGeoNameId(int geoNameId)
//        //// ReSharper restore MemberCanBePrivate.Global
//        //{
//        //    // geoname id's are positive
//        //    if (geoNameId == 0) return null;

//        //    // first look in the db
//        //    var place = _places.FindOne(PlaceBy.GeoNameId(geoNameId).ForInsertOrUpdate());

//        //    // if place does not exist, create from toponym
//        //    if (place == null)
//        //    {
//        //        // load toponym from storage
//        //        var toponym = _toponyms.FindOne(geoNameId);

//        //        // convert toponym to place
//        //        place = toponym.ToPlace();

//        //        // match place hierarchy to toponym hierarchy
//        //        if (toponym.Parent != null)
//        //        {
//        //            place.Parent = FromGeoNameId(toponym.Parent.GeoNameId);
//        //        }

//        //        // try to match to geoplanet
//        //        if (place.GeoPlanetPlace == null && place.GeoNamesToponym != null)
//        //        {
//        //            place.GeoPlanetPlace = _geoPlanetPlaces.FindByGeoNameId(place.GeoNamesToponym.GeoNameId);
//        //        }

//        //        // add to db & save
//        //        _placeChanger.Create(place, true);
//        //    }

//        //    return place;
//        //}

//    }
//}