namespace UCosmic.Domain.Places
{
    public class GeoPlanetLocality
    {
        protected internal GeoPlanetLocality()
        {
        }

        public string TypeName { get; protected internal set; }
        public string Name { get; protected internal set; }

        public override string ToString()
        {
            return Name;
        }
    }
}