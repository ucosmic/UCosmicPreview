using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UCosmic.Domain.Identity
{
    public class Role : RevisableEntity
    {
        protected internal Role()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Grants = new Collection<RoleGrant>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public string Name { get; protected internal set; }
        public string Description { get; protected internal set; }

        public virtual ICollection<RoleGrant> Grants { get; protected internal set; }
    }
}