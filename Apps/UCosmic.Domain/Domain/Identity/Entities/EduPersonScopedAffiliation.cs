using System;

namespace UCosmic.Domain.Identity
{
    public class EduPersonScopedAffiliation : Entity, IAmNumbered
    {
        protected internal EduPersonScopedAffiliation()
        {
            CreatedOnUtc = DateTime.UtcNow;
        }

        public int Number { get; protected internal set; }
        public int UserId { get; protected internal set; }
        public virtual User User { get; protected internal set; }
        public string Value { get; protected internal set; }

        public DateTime CreatedOnUtc { get; protected set; }
    }
}
