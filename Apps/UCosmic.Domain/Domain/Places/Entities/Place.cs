using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UCosmic.Domain.Places
{
    public class Place : RevisableEntity
    {
        protected internal Place()
        {
            Center = new Coordinates(null, null);
            BoundingBox = new BoundingBox(null, null, null, null);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Names = new Collection<PlaceName>();
            Children = new Collection<Place>();
            Ancestors = new Collection<PlaceNode>();
            Offspring = new Collection<PlaceNode>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public Coordinates Center { get; protected internal set; }
        public BoundingBox BoundingBox { get; protected internal set; }

        public string OfficialName { get; protected internal set; }
        public virtual ICollection<PlaceName> Names { get; protected internal set; }

        public virtual Place Parent { get; protected internal set; }
        public virtual ICollection<Place> Children { get; protected internal set; }
        public virtual ICollection<PlaceNode> Ancestors { get; protected set; }
        public virtual ICollection<PlaceNode> Offspring { get; protected set; }

        public virtual GeoNamesToponym GeoNamesToponym { get; protected internal set; }
        public virtual GeoPlanetPlace GeoPlanetPlace { get; protected internal set; }

        public bool IsEarth { get; protected internal set; }
        public bool IsContinent { get; protected internal set; }
        public bool IsCountry { get; protected internal set; }
        public bool IsAdmin1 { get; protected internal set; }
        public bool IsAdmin2 { get; protected internal set; }
        public bool IsAdmin3 { get; protected internal set; }

        public override string ToString()
        {
            return OfficialName;
        }

    }
}