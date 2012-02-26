using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Places
{
    public class GeoNamesAlternateName
    {
        [Key]
        public long AlternateNameId { get; set; }

        public int GeoNameId { get; set; }

        [StringLength(10)]
        public string Language { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public virtual GeoNamesToponym Toponym { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Language, Name);
        }
    }
}