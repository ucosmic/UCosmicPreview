using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    internal static class RoleQueries
    {
        internal static Role By(this IQueryable<Role> queryable, string name)
        {
            return queryable.SingleOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        internal static IEnumerable<Role> GrantedTo(this IQueryable<Role> queryable, string userName)
        {
            return queryable.Where(r => r.Grants.Any(g => g.User.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
