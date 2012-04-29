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

        public static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor, IEnumerable<Expression<Func<User, object>>> eagerLoad, out User entity)
        {
            if (queryProcessor == null)
            {
                entity = null;
                return false;
            }

            entity = queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = principal.Identity.Name,
                    EagerLoad = eagerLoad,
                }
            );

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor, IEnumerable<Expression<Func<User, object>>> eagerLoad = null)
        {
            User entity;
            return IdentityNameMatchesUser(principal, queryProcessor, eagerLoad, out entity);
        }

        public static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor, out User entity)
        {
            return IdentityNameMatchesUser(principal, queryProcessor, null, out entity);
        }

        #endregion
    }
}
