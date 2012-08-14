using System;

namespace UCosmic.Domain.Identity
{
    public class Preference : Entity
    {
        protected Preference() { }

        protected internal Preference(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            User = user;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            UserId = user.RevisionId;
            Owner = user.RevisionId.ToObjectString();
        }

        protected internal Preference(string anonymousId)
        {
            if (string.IsNullOrWhiteSpace(anonymousId)) throw new ArgumentException("Cannot be null or whitespace.", "anonymousId");

            AnonymousId = anonymousId;
            Owner = AnonymousId;
        }

        public string Owner { get; protected set; }
        public string AnonymousId { get; protected set; }
        public int? UserId { get; protected set; }
        public virtual User User { get; protected set; }

        public string Category { get; protected internal set; }
        public string Key { get; protected internal set; }
        public string Value { get; protected internal set; }
    }
}
