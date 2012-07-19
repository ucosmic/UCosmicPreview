using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    internal static class QueryRoles
    {
        internal static Role By(this IQueryable<Role> queryable, string name)
        {
            return queryable.SingleOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        internal static Role By(this IQueryable<Role> queryable, Guid guid)
        {
            return queryable.SingleOrDefault(r => r.EntityId == guid);
        }

        internal static IEnumerable<Role> GrantedTo(this IQueryable<Role> queryable, string userName)
        {
            return queryable.Where(r => r.Grants.Any(g => g.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
