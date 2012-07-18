using System.Collections.Generic;

namespace UCosmic.Domain.Places
{
    public class GeoNamesToponym : Entity
    {
        public GeoNamesToponym()
        {
            Center = new Coordinates();
        }

        public const int EarthGeoNameId = 6295630;

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

        public string Name { get; set; }

        public string ToponymName { get; set; }

        public virtual ICollection<GeoNamesAlternateName> AlternateNames { get; set; }

        public string ContinentCode { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string Admin1Code { get; set; }

        public string Admin1Name { get; set; }

        public string Admin2Code { get; set; }

        public string Admin2Name { get; set; }

        public string Admin3Code { get; set; }

        public string Admin3Name { get; set; }

        public string Admin4Code { get; set; }

        public string Admin4Name { get; set; }

        public long? Population { get; set; }
        public int? Elevation { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}