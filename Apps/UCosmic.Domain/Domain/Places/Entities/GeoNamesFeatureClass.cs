namespace UCosmic.Domain.Places
{
    public class GeoNamesFeatureClass : Entity
    {
        public string Code { get; protected internal set; }
        public string Name { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Code, Name);
        }
    }
}