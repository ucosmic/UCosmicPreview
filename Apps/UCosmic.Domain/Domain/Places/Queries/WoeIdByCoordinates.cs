using System;
using System.Linq;
using NGeo.Yahoo.PlaceFinder;

namespace UCosmic.Domain.Places
{
    public class WoeIdByCoordinates : IDefineQuery<int>
    {
        public WoeIdByCoordinates(double latitude, double longitude)
        {
            Coordinates = new Coordinates { Latitude = latitude, Longitude = longitude, };
        }

        public Coordinates Coordinates { get; private set; }
    }

    public class HandleWoeIdByCoordinatesCommand : IHandleQueries<WoeIdByCoordinates, int>
    {
        private readonly IConsumePlaceFinder _placeFinder;

        public HandleWoeIdByCoordinatesCommand(IConsumePlaceFinder placeFinder)
        {
            _placeFinder = placeFinder;
        }

        public int Handle(WoeIdByCoordinates query)
        {
            if (query == null) throw new ArgumentNullException("query");

            int? woeId = null;
            var retries = 0;
            const int retryLimit = 6;
            Result placeFinderResult = null;
            // ReSharper disable PossibleInvalidOperationException
            var latitude = query.Coordinates.Latitude.Value;
            var longitude = query.Coordinates.Longitude.Value;
            // ReSharper restore PossibleInvalidOperationException

            while (!woeId.HasValue && retries++ < retryLimit)
            {
                placeFinderResult = _placeFinder.Find(
                    new PlaceByCoordinates(latitude, longitude)).FirstOrDefault();
                if (placeFinderResult != null)
                {
                    woeId = placeFinderResult.WoeId;
                }
                if (!woeId.HasValue)
                {
                    latitude += 0.00001;
                    longitude += 0.00001;
                }
            }

            if (!woeId.HasValue && placeFinderResult != null)
            {
                var freeformText = string.Format("{0} {1}", placeFinderResult.CityName, placeFinderResult.CountryName);
                var result = _placeFinder.Find(new PlaceByFreeformText(freeformText)).FirstOrDefault();
                if (result != null) woeId = result.WoeId;
            }

            if (woeId.HasValue) return woeId.Value;
            return GeoPlanetPlace.EarthWoeId;
        }
    }
}
