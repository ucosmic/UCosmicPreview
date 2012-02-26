using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentType : RevisableEntity
    {
        public int CategoryId { get; set; }
        public virtual EstablishmentCategory Category { get; set; }

        [Required]
        [StringLength(150)]
        public string EnglishName { get; set; }

        [StringLength(150)]
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