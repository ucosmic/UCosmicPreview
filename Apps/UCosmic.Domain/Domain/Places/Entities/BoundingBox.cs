namespace UCosmic.Domain.Places
{
    public class BoundingBox
    {
        protected BoundingBox()
            :this(null, null, null, null)
        {
        }

        protected internal BoundingBox(double? northLatitude, double? eastLongitude,
            double? southLatitude, double? westLongitude)
        {
            Northeast = new Coordinates(northLatitude, eastLongitude);
            Southwest = new Coordinates(southLatitude, westLongitude);
        }

        public Coordinates Northeast { get; protected set; }
        public Coordinates Southwest { get; protected set; }

        public bool HasValue
        {
            get
            {
                return Northeast != null && Southwest != null
                    && Northeast.HasValue && Southwest.HasValue;
            }
        }
    }
}