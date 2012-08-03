using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public static class ValidatePrincipal
    {
        #region Principal cannot be null

        public const string FailedBecausePrincipalWasNull =
            "The principal was null.";

        #endregion
        #region PrincipalIdentityName cannot be empty

        public const string FailedBecauseIdentityNameWasEmpty =
            "The principal identity name is required.";

        public static bool IdentityNameIsNotEmpty(IPrincipal principal)
        {
            return !string.IsNullOrWhiteSpace(principal.Identity.Name);
        }

        #endregion
        #region PrincipalIdentityName matches user

        public const string FailedBecauseIdentityNameMatchedNoUser =
            "The principal identity name '{0}' does not have a user account.";

        public static bool IdentityNameMatchesUser(IPrincipal principal, IQueryEntities entities, IEnumerable<Expression<Func<User, object>>> eagerLoad, out User entity)
        {
            if (entities == null)
            {
                entity = null;
                return false;
            }

            entity = entities.Query<User>()
                .EagerLoad(entities, eagerLoad)
                .ByName(principal.Identity.Name);

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool IdentityNameMatchesUser(IPrincipal principal, IQueryEntities entities, IEnumerable<Expression<Func<User, object>>> eagerLoad = null)
        {
            User entity;
            return IdentityNameMatchesUser(principal, entities, eagerLoad, out entity);
        }

        public static bool IdentityNameMatchesUser(IPrincipal principal, IQueryEntities entities, out User entity)
        {
            return IdentityNameMatchesUser(principal, entities, null, out entity);
        }

        #endregion
    }
}
