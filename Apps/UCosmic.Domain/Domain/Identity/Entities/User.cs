using System.Collections.Generic;
using System.Collections.ObjectModel;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class User : RevisableEntity
    {
        protected internal User()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Grants = new Collection<RoleGrant>();
            Preferences = new Collection<Preference>();
            SubjectNameIdentifiers = new Collection<SubjectNameIdentifier>();
            EduPersonScopedAffiliations = new Collection<EduPersonScopedAffiliation>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public virtual Person Person { get; protected internal set; }
        public string Name { get; protected internal set; }
        public bool IsRegistered { get; protected internal set; }

        public string EduPersonTargetedId { get; protected internal set; }
        public virtual ICollection<EduPersonScopedAffiliation> EduPersonScopedAffiliations { get; protected set; }
        public virtual ICollection<SubjectNameIdentifier> SubjectNameIdentifiers { get; protected set; }

        public virtual ICollection<Preference> Preferences { get; protected internal set; }

        public virtual ICollection<RoleGrant> Grants { get; protected internal set; }
        public bool IsInRole(string roleName)
        {
            return Grants.ByRole(roleName) != null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}