using System;
using System.Linq;
using System.Collections.Generic;

namespace UCosmic.Domain.Identity
{
    internal static class QueryRoleGrants
    {
        internal static RoleGrant ByUser(this ICollection<RoleGrant> enumerable, Guid userEntityId)
        {
            if (enumerable == null || !enumerable.Any()) return null;
            if (userEntityId == Guid.Empty)
                throw new InvalidOperationException(string.Format("EntityId Guid is empty ({0}).", Guid.Empty));
            return enumerable.SingleOrDefault(g => g.User.EntityId == userEntityId);
        }

        internal static RoleGrant ByRole(this ICollection<RoleGrant> enumerable, string roleName)
        {
            if (enumerable == null || !enumerable.Any()) return null;
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Cannot be null or whitespace.", "roleName");
            return enumerable.SingleOrDefault(g => g.Role.Name == roleName);
        }
    }
}
