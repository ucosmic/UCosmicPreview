namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceType : Entity
    {
        public int Code { get; protected internal set; }
        public string Uri { get; protected internal set; }
        public string EnglishName { get; protected internal set; }
        public string EnglishDescription { get; protected internal set; }

        public override string ToString()
        {
            return EnglishName;
        }
    }
}