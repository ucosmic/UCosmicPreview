
namespace UCosmic.Domain.Places
{
    public class BoundingBox
    {
        public BoundingBox()
        {
            Northeast = new Coordinates();
            Southwest = new Coordinates();
        }

        public Coordinates Northeast { get; set; }
        public Coordinates Southwest { get; set; }

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