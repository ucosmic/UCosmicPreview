using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlace : Entity
    {
        protected internal GeoPlanetPlace()
        {
            Center = new Coordinates(null, null);
            BoundingBox = new BoundingBox(null, null, null, null);
            Country = new GeoPlanetAdmin();
            Admin1 = new GeoPlanetAdmin();
            Admin2 = new GeoPlanetAdmin();
            Admin3 = new GeoPlanetAdmin();
            Locality1 = new GeoPlanetLocality();
            Locality2 = new GeoPlanetLocality();

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Children = new Collection<GeoPlanetPlace>();
            Ancestors = new Collection<GeoPlanetPlaceNode>();
            Offspring = new Collection<GeoPlanetPlaceNode>();
            BelongTos = new Collection<GeoPlanetPlaceBelongTo>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public const int EarthWoeId = 1;

        public int WoeId { get; protected internal set; }

        public virtual GeoPlanetPlace Parent { get; protected internal set; }
        public virtual ICollection<GeoPlanetPlace> Children { get; protected set; }

        public virtual ICollection<GeoPlanetPlaceNode> Ancestors { get; protected set; }
        public virtual ICollection<GeoPlanetPlaceNode> Offspring { get; protected set; }
        public virtual ICollection<GeoPlanetPlaceBelongTo> BelongTos { get; protected internal set; }

        public virtual GeoPlanetPlaceType Type { get; protected internal set; }
        public virtual Place Place { get; protected internal set; }

        public string EnglishName { get; protected internal set; }
        public string Uri { get; protected internal set; }

        public Coordinates Center { get; protected internal set; }
        public BoundingBox BoundingBox { get; protected internal set; }

        public int AreaRank { get; protected internal set; }
        public int PopulationRank { get; protected internal set; }
        public string Postal { get; protected internal set; }

        public GeoPlanetAdmin Country { get; protected internal set; }
        public GeoPlanetAdmin Admin1 { get; protected internal set; }
        public GeoPlanetAdmin Admin2 { get; protected internal set; }
        public GeoPlanetAdmin Admin3 { get; protected internal set; }
        public GeoPlanetLocality Locality1 { get; protected internal set; }
        public GeoPlanetLocality Locality2 { get; protected internal set; }

        public bool IsContinent { get { return Type.Code == (int)GeoPlanetPlaceTypeEnum.Continent; } }
        public bool IsCountry { get { return Type.Code == (int)GeoPlanetPlaceTypeEnum.Country; } }
        public bool IsAdmin1 { get { return Type.Code == (int)GeoPlanetPlaceTypeEnum.Admin1; } }
        public bool IsAdmin2 { get { return Type.Code == (int)GeoPlanetPlaceTypeEnum.Admin2; } }
        public bool IsAdmin3 { get { return Type.Code == (int)GeoPlanetPlaceTypeEnum.Admin3; } }

        public override string ToString()
        {
            return EnglishName;
        }

    }
}