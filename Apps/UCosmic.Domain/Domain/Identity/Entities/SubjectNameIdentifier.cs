using System;

namespace UCosmic.Domain.Identity
{
    public class SubjectNameIdentifier : Entity, IAmNumbered
    {
        protected internal SubjectNameIdentifier()
        {
            CreatedOnUtc = DateTime.UtcNow;
        }

        public int Number { get; protected internal set; }
        public int UserId { get; protected internal set; }
        public virtual User User { get; protected internal set; }
        public string Value { get; protected internal set; }

        public DateTime CreatedOnUtc { get; protected internal set; }
        public DateTime? UpdatedOnUtc { get; protected internal set; }
    }
}
