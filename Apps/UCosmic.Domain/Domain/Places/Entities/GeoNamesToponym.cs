using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UCosmic.Domain.Places
{
    public class GeoNamesToponym : Entity
    {
        protected internal GeoNamesToponym()
        {
            Center = new Coordinates(null, null);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Children = new Collection<GeoNamesToponym>();
            Ancestors = new Collection<GeoNamesToponymNode>();
            Offspring = new Collection<GeoNamesToponymNode>();
            AlternateNames = new Collection<GeoNamesAlternateName>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public const int EarthGeoNameId = 6295630;

        public int GeoNameId { get; protected internal set; }

        public string FeatureCode { get; protected internal set; }
        public virtual GeoNamesFeature Feature { get; protected internal set; }

        public string TimeZoneId { get; protected internal set; }
        public virtual GeoNamesTimeZone TimeZone { get; protected internal set; }
        public Coordinates Center { get; protected internal set; }

        public virtual Place Place { get; protected internal set; }
        public virtual GeoNamesCountry AsCountry { get; protected internal set; }

        public virtual GeoNamesToponym Parent { get; protected internal set; }
        public virtual ICollection<GeoNamesToponym> Children { get; protected set; }

        public virtual ICollection<GeoNamesToponymNode> Ancestors { get; protected set; }
        public virtual ICollection<GeoNamesToponymNode> Offspring { get; protected set; }

        public string Name { get; protected internal set; }
        public string ToponymName { get; protected internal set; }
        public virtual ICollection<GeoNamesAlternateName> AlternateNames { get; protected set; }

        public string ContinentCode { get; protected internal set; }
        public string CountryCode { get; protected internal set; }
        public string CountryName { get; protected internal set; }

        public string Admin1Code { get; protected internal set; }
        public string Admin1Name { get; protected internal set; }

        public string Admin2Code { get; protected internal set; }
        public string Admin2Name { get; protected internal set; }

        public string Admin3Code { get; protected internal set; }
        public string Admin3Name { get; protected internal set; }

        public string Admin4Code { get; protected internal set; }
        public string Admin4Name { get; protected internal set; }

        public long? Population { get; protected internal set; }
        public int? Elevation { get; protected internal set; }

        public override string ToString()
        {
            return Name;
        }
    }
}