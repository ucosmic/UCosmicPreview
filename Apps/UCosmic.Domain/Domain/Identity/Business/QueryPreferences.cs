using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public static class QueryPreferences
    {
        internal static IQueryable<Preference> ByUserName(this IQueryable<Preference> queryable, string userName)
        {
            return queryable.Where(x => x.User != null && x.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        internal static IQueryable<Preference> ByPrincipal(this IQueryable<Preference> queryable, IPrincipal principal)
        {
            return queryable.ByUserName(principal.Identity.Name);
        }

        internal static IQueryable<Preference> ByAnonymousId(this IQueryable<Preference> queryable, string anonymousId)
        {
            return queryable.Where(x => anonymousId.Equals(x.AnonymousId, StringComparison.OrdinalIgnoreCase));
        }

        internal static IQueryable<Preference> ByCategory(this IQueryable<Preference> queryable, Enum category)
        {
            var categoryText = category.ToString();
            return queryable.Where(x => x.Category.Equals(categoryText, StringComparison.OrdinalIgnoreCase));
        }

        internal static IQueryable<Preference> ByKey(this IQueryable<Preference> queryable, Enum key)
        {
            var keyText = key.ToString();
            return queryable.Where(x => x.Key.Equals(keyText, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Preference> ByKey(this IEnumerable<Preference> enumerable, Enum key)
        {
            return enumerable.AsQueryable().ByKey(key);
        }
    }
}
