using System;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentSamlSignOn : Entity
    {
        protected internal EstablishmentSamlSignOn()
        {
            CreatedOnUtc = DateTime.UtcNow;
        }

        public int Id { get; protected set; }
        public string EntityId { get; protected internal set; }

        public string MetadataUrl { get; protected internal set; }
        public string MetadataXml { get; protected internal set; }

        public string SsoLocation { get; protected internal set; }
        public string SsoBinding { get; protected internal set; }

        public DateTime? CreatedOnUtc { get; protected internal set; }
        public DateTime? UpdatedOnUtc { get; protected internal set; }
    }
}