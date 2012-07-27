using System;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    internal static class QueryLanguages
    {
        internal static Language ByIsoCode(this IQueryable<Language> queryable, string isoCode)
        {
            if (string.IsNullOrWhiteSpace(isoCode))
                throw new ArgumentException("Cannot be null or white space.", "isoCode");

            return queryable.SingleOrDefault(
                l =>
                isoCode.Equals(l.TwoLetterIsoCode, StringComparison.OrdinalIgnoreCase) ||
                isoCode.Equals(l.ThreeLetterIsoCode, StringComparison.OrdinalIgnoreCase) ||
                isoCode.Equals(l.ThreeLetterIsoBibliographicCode, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
