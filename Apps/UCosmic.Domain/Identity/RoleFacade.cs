using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public class RoleFacade : RevisableEntityFacade<Role>
    {
        public RoleFacade(ICommandEntities entities) : base(entities)
        {
        }

        public virtual IEnumerable<Role> Get(params Expression<Func<Role, object>>[] eagerLoads)
        {
            return Get(Entities.Roles, eagerLoads);
        }

        public Role Get(string name, params Expression<Func<Role, object>>[] eagerLoads)
        {
            if (name == null) throw new ArgumentNullException("name");
            var query = EagerLoad(Entities.Roles, eagerLoads);
            var role = query.By(name);
            return role;
        }

        public Role Get(Guid entityId, params Expression<Func<Role, object>>[] eagerLoads)
        {
            return Get(Entities.Roles, entityId, eagerLoads);
        }

        public Role Get(int revisionId, params Expression<Func<Role, object>>[] eagerLoads)
        {
            return Get(Entities.Roles, revisionId, eagerLoads);
        }

        public virtual Role GetBySlug(string slug, params Expression<Func<Role, object>>[] eagerLoads)
        {
            if (slug == null) throw new ArgumentNullException("slug");
            return Get(slug.Replace("-", " "), eagerLoads);
        }

        public IEnumerable<Role> GetGrantedTo(string userName, params Expression<Func<Role, object>>[] eagerLoads)
        {
            var query = EagerLoad(Entities.Roles, eagerLoads);
            return query.ByGrantedTo(userName);
        }

        public Role CreateOrUpdate(string name, string description)
        {
            var role = Get(name) ?? new Role { Name = name };
            role.Description = description;
            return role;
        }

        public int Update(IPrincipal principal, Guid entityId, string description,
            IEnumerable<Guid> revokedUserEntityIds, IEnumerable<Guid> grantedUserEntityIds)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            if (!principal.IsInRole(RoleName.AuthorizationAgent))
                throw new SecurityAccessDeniedException(string.Format(
                    "User '{0}' does not have privileges to invoke this function.", principal.Identity.Name));

            var entity = Get(entityId, r => r.Grants.Select(g => g.User));

            if (entity == null) throw new InvalidOperationException(string.Format(
                "Entity '{0}' could not be found.", entityId));
            var changes = 0;

            if (entity.Description != description) ++changes;
            entity.Description = description;

            // ReSharper disable LoopCanBeConvertedToQuery
            if (revokedUserEntityIds != null)
                foreach (var revokedUserEntityId in revokedUserEntityIds)
                    changes += entity.RevokeUser(revokedUserEntityId, Entities);

            if (grantedUserEntityIds != null)
                foreach (var grantedUserEntityId in grantedUserEntityIds)
                    changes += entity.GrantUser(grantedUserEntityId, Entities);
            // ReSharper restore LoopCanBeConvertedToQuery

            if (changes < 1) return 0;

            entity.UpdatedByPrincipal = principal.Identity.Name;
            entity.UpdatedOnUtc = DateTime.UtcNow;

            Entities.Update(entity);
            return changes;
        }

    }
}
