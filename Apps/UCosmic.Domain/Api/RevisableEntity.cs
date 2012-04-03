using System;
using System.Threading;

namespace UCosmic.Domain
{
    public abstract class RevisableEntity : Entity
    {
        protected RevisableEntity()
        {
            EntityId = Guid.NewGuid();
            CreatedOnUtc = DateTime.UtcNow;
            CreatedByPrincipal = Thread.CurrentPrincipal.Identity.Name;
            IsCurrent = true;
        }

        public int RevisionId { get; set; }

        public Guid EntityId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string CreatedByPrincipal { get; set; }

        public DateTime? UpdatedOnUtc { get; protected internal set; }

        public string UpdatedByPrincipal { get; protected internal set; }

        public byte[] Version { get; protected internal set; }

        public bool IsCurrent { get; set; }

        public bool IsArchived { get; protected internal set; }

        public bool IsDeleted { get; protected internal set; }
    }
}