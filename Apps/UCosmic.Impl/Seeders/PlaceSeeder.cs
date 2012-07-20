using System.Collections.Generic;
using System.Linq;
using NGeo.GeoNames;
using NGeo.Yahoo.GeoPlanet;
using UCosmic.Impl.Orm;
using UCosmic.Domain.Places;

namespace UCosmic.Impl.Seeders
{
    public class PlaceSeeder : ISeedDb
    {
        public void Seed(UCosmicContext context)
        {
            // do not seed in development (uncomment to enable)
            // if (WebConfig.IsDeployedToDev) return;

            new PlaceDevelopmentSqlSeeder().Seed(context);
            //new PlaceByGeoPlanetEntitySeeder().Seed(context);
        }
    }

    public class PlaceDevelopmentSqlSeeder : NonContentFileSqlDbSeeder
    {
        protected override IEnumerable<string> SqlScripts
        {
            get
            {
                return new[]
                {
                    "PlacesData.sql",
                    "PlacesGeoAltNamesDataA.sql",
                    "PlaceNamesDataA.sql",
                };
            }
        }

        public override void Seed(UCosmicContext context)
        {
            if (!context.Places.Any())
                base.Seed(context);
        }

    }

    // high-level place data has already been seeded in production.

    //// ReSharper disable UnusedMember.Global
    //public class PlaceProductionSqlSeeder : NonContentFileSqlDbSeeder
    //// ReSharper restore UnusedMember.Global
    //{
    //    protected override IEnumerable<string> SqlScripts
    //    {
    //        get
    //        {
    //            return new[]
    //            {
    //                "PlacesProductionSqlA",
    //                "PlacesProductionSqlB",
    //                "PlacesProductionSqlC",
    //                "PlacesProductionSqlD",
    //                "PlacesProductionSqlE",
    //                "PlacesProductionSqlF1",
    //                "PlacesProductionSqlF2",
    //                "PlacesProductionSqlF3",
    //                "PlacesProductionSqlF4",
    //                "PlacesProductionSqlF5",
    //                "PlacesProductionSqlF6",
    //                "PlacesProductionSqlF7",
    //                "PlacesProductionSqlG",
    //                "PlacesProductionSqlH",
    //                "PlacesGeoAltNamesProductionSqlA",
    //                "PlacesGeoAltNamesProductionSqlB",
    //                "PlacesGeoAltNamesProductionSqlC",
    //                "PlacesGeoAltNamesProductionSqlD",
    //                "PlacesGeoAltNamesProductionSqlE",
    //                "PlacesGeoAltNamesProductionSqlF",
    //                "PlacesGeoAltNamesProductionSqlG",
    //                "PlacesGeoAltNamesProductionSqlH",
    //                "PlacesGeoAltNamesProductionSqlI",
    //                "PlacesGeoAltNamesProductionSqlJ",
    //                "PlaceNamesProductionSqlA",
    //                "PlaceNamesProductionSqlB",
    //                "PlaceNamesProductionSqlC",
    //                "PlaceNamesProductionSqlD",
    //                "PlaceNamesProductionSqlE",
    //                "PlaceNamesProductionSqlF",
    //                "PlaceNamesProductionSqlG",
    //                "PlaceNamesProductionSqlH",
    //                "PlaceNamesProductionSqlI",
    //                "PlaceNamesProductionSqlJ",
    //            };
    //        }
    //    }

    //    public override void Seed(UCosmicContext context)
    //    {
    //        if (!Context.Places.Any())
    //            base.Seed(context);
    //    }

    //}

    // ReSharper disable UnusedMember.Global
    public class PlaceByGeoPlanetEntitySeeder : UCosmicDbSeeder
    // ReSharper restore UnusedMember.Global
    {
        public override void Seed(UCosmicContext context)
        {
            Context = context;

            var geoNames = new GeoNamesClient();
            var geoPlanet = new GeoPlanetClient();
            var configurationManager = new DotNetConfigurationManager();
            //var objectCommander = new ObjectCommander(context);

            //// find out which geonames countries were not imported
            //var geoNamesCountries = geoNames.Countries(configurationManager.GeoNamesUserName);
            //var countryGeoNameIds = geoNamesStorage.FindMany(new GeoNameFinder { IsCountry = true }).Select(t => t.GeoNameId);
            //var nonPopulatedCountries = geoNamesCountries.Where(c => !countryGeoNameIds.Contains(c.GeoNameId));

            //var placeFactory = new PlaceFactory(context, objectCommander, geoPlanet, geoNames, configurationManager);

            var queryProcessor = ServiceLocatorPattern.ServiceProviderLocator.Current.GetService<IProcessQueries>();

            //placeFactory.FromWoeId(GeoPlanetPlace.EarthWoeId);
            var earth = queryProcessor.Execute(new GetPlaceByWoeIdQuery { WoeId = GeoPlanetPlace.EarthWoeId });

            var geoPlanetContinents = geoPlanet.Continents(configurationManager.GeoPlanetAppId)
                .OrderBy(c => c.Name)
                .ToList()
            ;
            foreach (var geoPlanetContinent in geoPlanetContinents)
            {
                //placeFactory.FromWoeId(geoPlanetContinent.WoeId);
                var continent = queryProcessor.Execute(new GetPlaceByWoeIdQuery { WoeId = geoPlanetContinent.WoeId });
            }

            //var countriesToImport = new[]
            //{
            //    "United States", "China", "United Kingdom", "Peru", "South Africa", "Australia", "India", "Egypt",
            //};
            var countriesToImport = new[]
            {
                "United States", "China", "United Kingdom",
            };
            var geoPlanetCountries = geoPlanet.Countries(configurationManager.GeoPlanetAppId)
                .Where(c => countriesToImport.Contains(c.Name))
                .OrderBy(c => c.Name)
                .ToList()
            ;
            foreach (var geoPlanetCountry in geoPlanetCountries)
            {
                //placeFactory.FromWoeId(geoPlanetCountry.WoeId);
                var country = queryProcessor.Execute(new GetPlaceByWoeIdQuery { WoeId = geoPlanetCountry.WoeId });
            }

            //foreach (var geoPlanetCountry in geoPlanetCountries)
            //{
            //    var geoPlanetStates = geoPlanet.States(geoPlanetCountry.WoeId, configurationManager.GeoPlanetAppId);
            //    if (geoPlanetStates == null) continue;
            //    foreach (var geoPlanetState in geoPlanetStates)
            //    {
            //        //placeFactory.FromWoeId(geoPlanetState.WoeId);
            //        var state = queryProcessor.Execute(new GetPlaceByWoeIdQuery { WoeId = geoPlanetState.WoeId });
            //    }
            //}
        }
    }
}