using System;
using System.Threading;

namespace UCosmic.Domain.People
{
    public class EmailMessage : Entity, IAmNumbered
    {
        protected internal EmailMessage()
        {
            ComposedOnUtc = DateTime.UtcNow;
            ComposedByPrincipal = Thread.CurrentPrincipal.Identity.Name;
        }

        public int ToPersonId { get; protected internal set; }
        public virtual Person ToPerson { get; protected internal set; }
        public int Number { get; protected internal set; }

        public string ToAddress { get; set; }
        public string FromEmailTemplate { get; set; }

        public string Subject { get; set; }

        public string FromAddress { get; set; }

        public string FromDisplayName { get; set; }

        public string ReplyToAddress { get; set; }

        public string ReplyToDisplayName { get; set; }

        public string Body { get; set; }

        public string ComposedByPrincipal { get; set; }

        public DateTime ComposedOnUtc { get; set; }
        public DateTime? SentOnUtc { get; set; }
    }

}