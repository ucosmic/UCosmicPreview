using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoNamesCountry
    {
        public GeoNamesCountry()
        {
            BoundingBox = new BoundingBox();
        }

        [Key]
        public int GeoNameId { get; set; }
        public virtual GeoNamesToponym AsToponym { get; set; }

        public BoundingBox BoundingBox { get; set; }

        [Required, StringLength(2)]
        public string Code { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(2)]
        public string ContinentCode { get; set; }

        [Required, StringLength(200)]
        public string ContinentName { get; set; }

        [Required]
        public int IsoNumericCode { get; set; }

        [Required, StringLength(3)]
        public string IsoAlpha3Code { get; set; }

        [StringLength(2)]
        public string FipsCode { get; set; }

        [StringLength(200)]
        public string CapitalCityName { get; set; }

        [StringLength(15)]
        public string AreaInSqKm { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [StringLength(150)]
        public string Languages { get; set; }

        public long Population { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}