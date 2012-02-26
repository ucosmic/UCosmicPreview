using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoNamesToponym : Entity
    {
        public GeoNamesToponym()
        {
            Center = new Coordinates();
        }

        public const int EarthGeoNameId = 6295630;

        [Key]
        public int GeoNameId { get; set; }

        public string FeatureCode { get; set; }
        public virtual GeoNamesFeature Feature { get; set; }

        public string TimeZoneId { get; set; }
        public virtual GeoNamesTimeZone TimeZone { get; set; }

        public virtual Place Place { get; set; }

        public virtual GeoNamesCountry AsCountry { get; set; }

        public virtual GeoNamesToponym Parent { get; set; }
        public virtual ICollection<GeoNamesToponym> Children { get; set; }

        public virtual ICollection<GeoNamesToponymNode> Ancestors { get; set; }
        public virtual ICollection<GeoNamesToponymNode> Offspring { get; set; } 

        public Coordinates Center { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string ToponymName { get; set; }

        public virtual ICollection<GeoNamesAlternateName> AlternateNames { get; set; }

        [StringLength(2)]
        public string ContinentCode { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        [StringLength(200)]
        public string CountryName { get; set; }

        [StringLength(15)]
        public string Admin1Code { get; set; }

        [StringLength(200)]
        public string Admin1Name { get; set; }

        [StringLength(15)]
        public string Admin2Code { get; set; }

        [StringLength(200)]
        public string Admin2Name { get; set; }

        [StringLength(15)]
        public string Admin3Code { get; set; }

        [StringLength(200)]
        public string Admin3Name { get; set; }

        [StringLength(15)]
        public string Admin4Code { get; set; }

        [StringLength(200)]
        public string Admin4Name { get; set; }

        public long? Population { get; set; }
        public int? Elevation { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}