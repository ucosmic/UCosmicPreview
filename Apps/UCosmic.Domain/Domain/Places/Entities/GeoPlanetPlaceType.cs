namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceType : Entity
    {
        protected internal GeoPlanetPlaceType()
        {
        }

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