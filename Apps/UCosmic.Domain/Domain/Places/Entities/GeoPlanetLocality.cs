namespace UCosmic.Domain.Places
{
    public class GeoPlanetLocality
    {
        public string TypeName { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}