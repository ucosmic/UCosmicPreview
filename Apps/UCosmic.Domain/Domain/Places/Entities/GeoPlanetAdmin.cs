namespace UCosmic.Domain.Places
{
    public class GeoPlanetAdmin
    {
        protected internal GeoPlanetAdmin()
        {
        }

        public string Code { get; protected internal set; }
        public string TypeName { get; protected internal set; }
        public string Name { get; protected internal set; }

        public override string ToString()
        {
            return Name;
        }
    }
}