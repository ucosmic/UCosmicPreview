namespace UCosmic.Domain.Places
{
    public class GeoNamesCountry
    {
        public GeoNamesCountry()
        {
            BoundingBox = new BoundingBox();
        }

        public int GeoNameId { get; set; }
        public virtual GeoNamesToponym AsToponym { get; set; }

        public BoundingBox BoundingBox { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ContinentCode { get; set; }

        public string ContinentName { get; set; }

        public int IsoNumericCode { get; set; }

        public string IsoAlpha3Code { get; set; }

        public string FipsCode { get; set; }

        public string CapitalCityName { get; set; }

        public string AreaInSqKm { get; set; }

        public string CurrencyCode { get; set; }

        public string Languages { get; set; }

        public long Population { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}