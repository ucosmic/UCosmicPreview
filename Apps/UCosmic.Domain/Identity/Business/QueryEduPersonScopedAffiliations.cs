using System;
using System.Linq;
using System.Collections.Generic;

namespace UCosmic.Domain.Identity
{
    internal static class QueryEduPersonScopedAffiliations
    {
        internal static EduPersonScopedAffiliation ByValue(this ICollection<EduPersonScopedAffiliation> enumerable, string value)
        {
            if (enumerable == null || !enumerable.Any()) return null;
            return enumerable.SingleOrDefault(a => a.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
