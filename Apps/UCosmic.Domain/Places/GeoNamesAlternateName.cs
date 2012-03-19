namespace UCosmic.Domain.Places
{
    public class GeoNamesAlternateName
    {
        public long AlternateNameId { get; set; }

        public int GeoNameId { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }

        public virtual GeoNamesToponym Toponym { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Language, Name);
        }
    }
}