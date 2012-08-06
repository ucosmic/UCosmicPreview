using System.Collections.Generic;

namespace UCosmic.Domain.People
{
    public class EmailAddress : Entity, IAmNumbered
    {
        protected internal EmailAddress()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Confirmations = new List<EmailConfirmation>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int PersonId { get; protected internal set; }
        public virtual Person Person { get; protected internal set; }
        public int Number { get; protected internal set; }

        public string Value { get; protected internal set; }

        public bool IsDefault { get; protected internal set; }
        public bool IsFromSaml { get; protected internal set; }
        public bool IsConfirmed { get; protected internal set; }

        public virtual ICollection<EmailConfirmation> Confirmations { get; protected set; }

        public override string ToString()
        {
            return Value ?? base.ToString();
        }
    }
}