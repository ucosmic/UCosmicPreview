
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.Identity
{
    public class RoleGrant : RevisableEntity
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }

        public virtual Establishment ForEstablishment { get; set; }

        internal int Revoke(ICommandObjects commander)
        {
            commander.Delete(this);
            return 1;
        }
    }
}