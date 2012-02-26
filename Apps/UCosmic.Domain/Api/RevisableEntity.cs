using System;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        [Required]
        public int RevisionId { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required]
        public DateTime CreatedOnUtc { get; set; }

        [StringLength(256)]
        public string CreatedByPrincipal { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }

        [StringLength(256)]
        public string UpdatedByPrincipal { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public bool IsCurrent { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDeleted { get; set; }
    }
}