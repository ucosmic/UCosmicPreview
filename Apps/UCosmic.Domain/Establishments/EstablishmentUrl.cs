using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentUrl : RevisableEntity
    {
        public virtual Establishment ForEstablishment { get; set; }

        [Required, StringLength(200)]
        public string Value { get; set; }

        public bool IsOfficialUrl { get; set; }

        public bool IsFormerUrl { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}