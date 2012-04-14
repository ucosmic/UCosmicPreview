using System.Collections.Generic;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class User : RevisableEntity
    {
        protected internal User()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Grants = Grants ?? new List<RoleGrant>();
            SubjectNameIdentifiers = SubjectNameIdentifiers ?? new List<SubjectNameIdentifier>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public string Name { get; protected internal set; }

        public bool IsRegistered { get; protected internal set; }

        public virtual ICollection<RoleGrant> Grants { get; protected internal set; }

        public virtual ICollection<SubjectNameIdentifier> SubjectNameIdentifiers { get; protected internal set; }

        public virtual Person Person { get; protected internal set; }
    }

}