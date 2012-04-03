using System;
using System.Linq;
using System.Collections.Generic;

namespace UCosmic.Domain.Identity
{
    internal static class RoleGrantQueries
    {
        internal static RoleGrant ByUser(this IEnumerable<RoleGrant> enumerable, Guid userEntityId)
        {
            if (userEntityId == Guid.Empty)
                throw new InvalidOperationException(string.Format("EntityId Guid is empty ({0}).", Guid.Empty));
            return enumerable.SingleOrDefault(g => g.User.EntityId == userEntityId);
        }
    }
}
