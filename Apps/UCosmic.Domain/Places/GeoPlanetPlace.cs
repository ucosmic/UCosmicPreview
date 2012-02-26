using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlace : Entity
    {
        public GeoPlanetPlace()
        {
            Center = new Coordinates();
            BoundingBox = new BoundingBox();
            Country = new GeoPlanetAdmin();
            Admin1 = new GeoPlanetAdmin();
            Admin2 = new GeoPlanetAdmin();
            Admin3 = new GeoPlanetAdmin();
            Locality1 = new GeoPlanetLocality();
            Locality2 = new GeoPlanetLocality();
        }

        public const int EarthWoeId = 1;

        [Key]
        public int WoeId { get; set; }

        public virtual GeoPlanetPlace Parent { get; set; }
        public virtual ICollection<GeoPlanetPlace> Children { get; set; }

        public virtual ICollection<GeoPlanetPlaceNode> Ancestors { get; set; }
        public virtual ICollection<GeoPlanetPlaceNode> Offspring { get; set; }

        public virtual GeoPlanetPlaceType Type { get; set; }

        public virtual Place Place { get; set; }

        [Required, StringLength(200)]
        public string EnglishName { get; set; }

        [Required, StringLength(200)]
        public string Uri { get; set; }

        public Coordinates Center { get; set; }

        public BoundingBox BoundingBox { get; set; }

        public int AreaRank { get; set; }
        public int PopulationRank { get; set; }


        [StringLength(50)]
        public string Postal { get; set; }

        public GeoPlanetAdmin Country { get; set; }

        public GeoPlanetAdmin Admin1 { get; set; }

        public GeoPlanetAdmin Admin2 { get; set; }

        public GeoPlanetAdmin Admin3 { get; set; }

        public GeoPlanetLocality Locality1 { get; set; }

        public GeoPlanetLocality Locality2 { get; set; }

        public virtual ICollection<GeoPlanetPlaceBelongTo> BelongTos { get; set; }

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