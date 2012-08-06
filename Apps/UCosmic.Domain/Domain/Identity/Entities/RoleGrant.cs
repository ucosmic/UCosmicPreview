using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.Identity
{
    public class RoleGrant : RevisableEntity
    {
        protected internal RoleGrant()
        {
        }

        public virtual User User { get; protected internal set; }
        public virtual Role Role { get; protected internal set; }
        public virtual Establishment ForEstablishment { get; protected internal set; }
    }
}