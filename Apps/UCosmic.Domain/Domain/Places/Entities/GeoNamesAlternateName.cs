namespace UCosmic.Domain.Places
{
    public class GeoNamesAlternateName : Entity
    {
        protected internal GeoNamesAlternateName()
        {
        }

        public long AlternateNameId { get; protected internal set; }
        public int GeoNameId { get; protected internal set; }
        public string Language { get; protected internal set; }
        public string Name { get; protected internal set; }
        public virtual GeoNamesToponym Toponym { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Language, Name);
        }
    }
}