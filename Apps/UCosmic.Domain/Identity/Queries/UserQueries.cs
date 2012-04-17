using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    internal static class UserQueries
    {
        internal static User ByName(this IQueryable<User> queryable, string name)
        {
            return queryable.SingleOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        internal static User ByEduPersonTargetedId(this IQueryable<User> queryable, string eduPersonTargetedId)
        {
            return queryable.SingleOrDefault
            (
                u =>
                u.EduPersonTargetedId != null &&
                u.EduPersonTargetedId.Equals(eduPersonTargetedId)
            );
        }

        internal static IQueryable<User> AutoComplete(this IQueryable<User> queryable, string term)
        {
            return queryable.Where(u => u.Name.Contains(term));
        }

        internal static int NextNumber(this ICollection<SubjectNameIdentifier> collection)
        {
            return collection.Any() ? collection.Max(w => w.Number) + 1 : 1;
        }

        internal static int NextNumber(this ICollection<EduPersonScopedAffiliation> collection)
        {
            return collection.Any() ? collection.Max(w => w.Number) + 1 : 1;
        }
    }
}
