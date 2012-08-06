namespace UCosmic.Domain.Places
{
    public class Coordinates
    {
        protected Coordinates()
        {
        }

        protected internal Coordinates(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double? Latitude { get; protected set; }
        public double? Longitude { get; protected set; }

        public bool HasValue
        {
            get { return Latitude.HasValue && Longitude.HasValue; }
        }
    }
}