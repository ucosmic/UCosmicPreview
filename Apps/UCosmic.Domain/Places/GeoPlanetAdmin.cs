using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoPlanetAdmin
    {
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(50)]
        public string TypeName { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}