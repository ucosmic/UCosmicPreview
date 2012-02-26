using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceType : Entity
    {
        [Key]
        public int Code { get; set; }
        
        [Required, StringLength(200)]
        public string Uri { get; set; }
        
        [Required, StringLength(100)]
        public string EnglishName { get; set; }

        [Required, StringLength(500)]
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