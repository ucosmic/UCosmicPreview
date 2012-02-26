using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentEmailDomain : RevisableEntity
    {
        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
    }

}