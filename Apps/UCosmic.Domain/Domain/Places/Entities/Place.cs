using System.Collections.Generic;

namespace UCosmic.Domain.Places
{
    public class Place : RevisableEntity
    {
        public Place()
        {
            Center = new Coordinates();
            BoundingBox = new BoundingBox();
        }

        public string OfficialName { get; set; }

        public virtual Place Parent { get; set; }
        public virtual ICollection<Place> Children { get; set; }

        public virtual ICollection<PlaceNode> Ancestors { get; set; }
        public virtual ICollection<PlaceNode> Offspring { get; set; }

        public virtual GeoNamesToponym GeoNamesToponym { get; set; }
        public virtual GeoPlanetPlace GeoPlanetPlace { get; set; }

        public bool IsEarth { get; set; }
        public bool IsContinent { get; set; }
        public bool IsCountry { get; set; }
        public bool IsAdmin1 { get; set; }
        public bool IsAdmin2 { get; set; }
        public bool IsAdmin3 { get; set; }

        public Coordinates Center { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public virtual ICollection<PlaceName> Names { get; set; }

        public override string ToString()
        {
            return OfficialName;
        }

    }
}