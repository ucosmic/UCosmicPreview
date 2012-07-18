namespace UCosmic.Domain.Places
{
    public class GeoPlanetAdmin
    {
        public string Code { get; set; }

        public string TypeName { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}