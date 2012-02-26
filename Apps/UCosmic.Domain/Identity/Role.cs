using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class Role : RevisableEntity
    {
        protected Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }

        [Required]
        [StringLength(200)]
        public string Name { get; protected set; }

        public string Slug { get { return Name.Replace(" ", "-").ToLower(); } }

        [StringLength(4000)]
        public string Description { get; set; }

        public virtual ICollection<RoleGrant> Grants { get; set; }

        internal int RevokeUser(Guid userEntityId, ICommandObjects commander)
        {
            var grant = Grants.SingleOrDefault(g => g.User.EntityId == userEntityId);
            return (grant != null) ? grant.Revoke(commander) : 0;
        }

        internal int GrantUser(Guid userEntityId, UserFinder userFinder)
        {
            var grant = Grants.SingleOrDefault(g => g.User.EntityId == userEntityId);
            if (grant != null) return 0;

            var user = userFinder.FindOne(By<User>.EntityId(userEntityId).ForInsertOrUpdate());
            Grants.Add(new RoleGrant { User = user, });
            return 1;
        }
    }

    public static class RoleExtensions
    {
        public static Role ByName(this IEnumerable<Role> query, string name)
        {
            return (query != null)
                ? query.Current().SingleOrDefault(r =>
                    r.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                : null;
        }

    }
}