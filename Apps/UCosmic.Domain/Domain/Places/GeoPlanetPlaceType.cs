namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceType : Entity
    {
        public int Code { get; set; }
        
        public string Uri { get; set; }
        
        public string EnglishName { get; set; }

        public string EnglishDescription { get; set; }

        public override string ToString()
        {
            return EnglishName;
        }
    }

    public enum GeoPlanetPlaceTypeEnum
    {
        Admin1 = 8,
        Admin2 = 9,
        Admin3 = 10,
        Country = 12,
        Continent = 29,
        TimeZone = 31,
    }
}