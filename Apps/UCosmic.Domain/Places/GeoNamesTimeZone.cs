using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoNamesTimeZone : Entity
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        public double DstOffset { get; set; }
        public double GmtOffset { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}