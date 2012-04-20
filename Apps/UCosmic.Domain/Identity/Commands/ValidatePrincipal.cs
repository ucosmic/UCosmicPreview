using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    internal static class ValidatePrincipal
    {
        #region PrincipalIdentityName matches user

        internal const string FailedBecauseIdentityNameWasEmpty =
            "The principal identity name is required.";

        internal static bool IdentityNameIsNotEmpty(IPrincipal principal)
        {
            return !string.IsNullOrWhiteSpace(principal.Identity.Name);
        }

        internal const string FailedBecauseIdentityNameMatchedNoUser =
            "The principal identity name '{0}' does not have a user account.";

        internal static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor, IEnumerable<Expression<Func<User, object>>> eagerLoad, out User entity)
        {
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

        internal static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor, IEnumerable<Expression<Func<User, object>>> eagerLoad = null)
        {
            User entity;
            return IdentityNameMatchesUser(principal, queryProcessor, eagerLoad, out entity);
        }

        internal static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor, out User entity)
        {
            return IdentityNameMatchesUser(principal, queryProcessor, null, out entity);
        }

        #endregion
    }
}
