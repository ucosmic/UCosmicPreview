using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public class RoleChanger
    {
        private readonly ICommandObjects _commander;
        private readonly IQueryEntities _entityQueries;

        public RoleChanger(ICommandObjects objectCommander, IQueryEntities entityQueries)
        {
            if (objectCommander == null)
                throw new ArgumentNullException("objectCommander");

            if (entityQueries == null)
                throw new ArgumentNullException("entityQueries");

            _commander = objectCommander;
            _entityQueries = entityQueries;
        }

        public int Change(IPrincipal principal, Guid entityId, string description,
            IEnumerable<Guid> revokedUserEntityIds, IEnumerable<Guid> grantedUserEntityIds
        )
        {
            if (principal == null) throw new ArgumentNullException("principal");

            var roleFinder = new RoleFinder(_entityQueries);
            var entity = roleFinder.FindOne(By<Role>.EntityId(entityId)
                .EagerLoad(r => r.Grants.Select(g => g.User))
                .ForInsertOrUpdate()
            );
            if (entity == null) throw new InvalidOperationException(string.Format(
                "Entity '{0}' could not be found.", entityId));
            var changes = 0;

            if (entity.Description != description) ++changes;
            entity.Description = description;

            // ReSharper disable LoopCanBeConvertedToQuery
            if (revokedUserEntityIds != null)
                foreach (var revokedUserEntityId in revokedUserEntityIds)
                    changes += entity.RevokeUser(revokedUserEntityId, _commander);

            if (grantedUserEntityIds != null)
                foreach (var grantedUserEntityId in grantedUserEntityIds)
                    changes += entity.GrantUser(grantedUserEntityId, new UserFinder(_entityQueries));
            // ReSharper restore LoopCanBeConvertedToQuery

            if (changes < 1) return 0;

            entity.UpdatedByPrincipal = principal.Identity.Name;
            entity.UpdatedOnUtc = DateTime.UtcNow;

            return _commander.SaveChanges();
        }
    }
}
