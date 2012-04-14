using System;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    internal static class UserQueries
    {
        internal static User By(this IQueryable<User> queryable, string name)
        {
            return queryable.SingleOrDefault(u => u.UserName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        internal static IQueryable<User> AutoComplete(this IQueryable<User> queryable, string term)
        {
            return queryable.Where(u => u.UserName.Contains(term));
        }
    }
}
