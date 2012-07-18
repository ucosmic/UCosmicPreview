
namespace UCosmic.Domain.Places
{
    public class Coordinates
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool HasValue
        {
            get
            {
                return Latitude.HasValue && Longitude.HasValue;
            }
        }

    }
}