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

        public string ToAddress { get; protected internal set; }
        public string FromEmailTemplate { get; protected internal set; }

        public string Subject { get; protected internal set; }
        public string Body { get; protected internal set; }

        public string FromAddress { get; protected internal set; }
        public string FromDisplayName { get; protected internal set; }

        public string ReplyToAddress { get; protected internal set; }
        public string ReplyToDisplayName { get; protected internal set; }

        public string ComposedByPrincipal { get; protected internal set; }
        public DateTime ComposedOnUtc { get; protected internal set; }
        public DateTime? SentOnUtc { get; protected internal set; }
    }

}