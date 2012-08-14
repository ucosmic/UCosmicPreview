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
            return queryable.Where(x => x.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        internal static IQueryable<Preference> ByPrincipal(this IQueryable<Preference> queryable, IPrincipal principal)
        {
            return queryable.ByUserName(principal.Identity.Name);
        }

        internal static IQueryable<Preference> ByCategory(this IQueryable<Preference> queryable, PreferenceCategory category)
        {
            var categoryText = category.AsSentenceFragment();
            return queryable.Where(x => x.CategoryText.Equals(categoryText, StringComparison.OrdinalIgnoreCase));
        }

        public static IQueryable<Preference> ByKey(this IQueryable<Preference> queryable, Enum key)
        {
            return queryable.ByKey(key.ToString());
        }

        internal static IQueryable<Preference> ByKey(this IQueryable<Preference> queryable, string key)
        {
            return queryable.Where(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Preference> ByKey(this IEnumerable<Preference> enumerable, Enum key)
        {
            return enumerable.ByKey(key.ToString());
        }

        internal static IEnumerable<Preference> ByKey(this IEnumerable<Preference> enumerable, string key)
        {
            return enumerable.Where(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
}
