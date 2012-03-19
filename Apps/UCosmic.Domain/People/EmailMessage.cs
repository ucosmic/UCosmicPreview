using System;
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

        public int Id { get; set; }
        public Guid EntityId { get; set; }

        public int ToEmailAddressId { get; set; }
        public virtual EmailAddress To { get; set; }

        public int? FromEmailTemplateId { get; set; }
        public virtual EmailTemplate FromEmailTemplate { get; set; }

        public string Subject { get; set; }

        public string FromAddress { get; set; }

        public string FromDisplayName { get; set; }

        public string ReplyToAddress { get; set; }

        public string ReplyToDisplayName { get; set; }

        public string Body { get; set; }

        public string ComposedByPrincipal { get; set; }

        public DateTime ComposedOnUtc { get; set; }
        public DateTime? SentOnUtc { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
    }

}