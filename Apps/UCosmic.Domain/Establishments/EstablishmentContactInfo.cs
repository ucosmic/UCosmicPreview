using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentContactInfo
    {
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public bool HasValue
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Phone)
                    || !string.IsNullOrWhiteSpace(Fax)
                    || !string.IsNullOrWhiteSpace(Email);
            }
        }
    }
}