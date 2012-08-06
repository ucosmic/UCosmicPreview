namespace UCosmic.Domain.Places
{
    public class GeoNamesCountry : Entity
    {
        protected internal GeoNamesCountry()
        {
            BoundingBox = new BoundingBox(null, null, null, null);
        }

        public int GeoNameId { get; protected internal set; }
        public virtual GeoNamesToponym AsToponym { get; protected internal set; }

        public BoundingBox BoundingBox { get; protected internal set; }

        public string Code { get; protected internal set; }
        public string Name { get; protected internal set; }

        public string ContinentCode { get; protected internal set; }
        public string ContinentName { get; protected internal set; }

        public int IsoNumericCode { get; protected internal set; }
        public string IsoAlpha3Code { get; protected internal set; }
        public string FipsCode { get; protected internal set; }

        public string CapitalCityName { get; protected internal set; }
        public string AreaInSqKm { get; protected internal set; }
        public string CurrencyCode { get; protected internal set; }
        public string Languages { get; protected internal set; }
        public long Population { get; protected internal set; }

        public override string ToString()
        {
            return Name;
        }
    }
}