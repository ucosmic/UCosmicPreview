using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentType : RevisableEntity
    {
        public string CategoryCode { get; set; }
        public virtual EstablishmentCategory Category { get; set; }

        public string EnglishName { get; set; }

        public string EnglishPluralName { get; set; }
    }

    public static class EstablishmentTypeExtensions
    {
        public static EstablishmentType ByEnglishNameAndCategoryCode(
            this IEnumerable<EstablishmentType> query, string englishName, string categoryCode)
        {
            return (query != null)
                ? query.SingleOrDefault(t =>
                    t.EnglishName.Equals(englishName, StringComparison.OrdinalIgnoreCase)
                    && t.Category.Code.Equals(categoryCode, StringComparison.OrdinalIgnoreCase))
                : null;
        }
    }
}