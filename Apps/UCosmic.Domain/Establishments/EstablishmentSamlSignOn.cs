using System;
using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentSamlSignOn
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1024)]
        public string EntityId { get; set; }

        [Required]
        [StringLength(1024)]
        public string MetadataUrl { get; set; }
        public string MetadataXml { get; set; }

        //public string SigningCertificate { get; set; }
        //public string EncryptionCertificate { get; set; }

        public string SsoLocation { get; set; }
        public string SsoBinding { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}