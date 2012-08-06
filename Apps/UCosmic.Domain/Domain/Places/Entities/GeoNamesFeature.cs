namespace UCosmic.Domain.Places
{
    public class GeoNamesFeature : Entity
    {
        protected internal GeoNamesFeature()
        {
        }

        public string ClassCode { get; protected internal set; }
        public virtual GeoNamesFeatureClass Class { get; protected internal set; }

        public string Code { get; protected internal set; }
        public string Name { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}:{2}", ClassCode, Code, Name);
        }
    }
}