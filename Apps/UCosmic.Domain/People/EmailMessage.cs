using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using UCosmic.Domain.Email;

namespace UCosmic.Domain.People
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            EntityId = Guid.NewGuid();
            ComposedOnUtc = DateTime.UtcNow;
            ComposedByPrincipal = Thread.CurrentPrincipal.Identity.Name;
            IsArchived = false;
            IsDeleted = false;
        }

        [Key] // email message should not be revisable
        public int Id { get; set; }
        public Guid EntityId { get; set; }

        public int ToEmailAddressId { get; set; }
        public virtual EmailAddress To { get; set; }

        public int? FromEmailTemplateId { get; set; }
        public virtual EmailTemplate FromEmailTemplate { get; set; }

        [Required]
        [StringLength(250)]
        public string Subject { get; set; }

        [Required]
        [StringLength(256)]
        public string FromAddress { get; set; }

        [StringLength(150)]
        public string FromDisplayName { get; set; }

        [StringLength(256)]
        public string ReplyToAddress { get; set; }

        [StringLength(150)]
        public string ReplyToDisplayName { get; set; }

        [Required]
        public string Body { get; set; }

        [StringLength(256)]
        public string ComposedByPrincipal { get; set; }

        public DateTime ComposedOnUtc { get; set; }
        public DateTime? SentOnUtc { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
    }

}