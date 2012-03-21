using System;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentSamlSignOn : Entity
    {
        public int Id { get; set; }

        public string EntityId { get; set; }

        public string MetadataUrl { get; set; }

        public string MetadataXml { get; set; }

        public string SsoLocation { get; set; }
        public string SsoBinding { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}