using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentCategory : RevisableEntity
    {
        [Required]
        [StringLength(150)]
        public string EnglishName { get; set; }

        [StringLength(150)]
        public string EnglishPluralName { get; set; }

        [StringLength(4)]
        public string Code { get; set; }
    }

    public static class EstablishmentCategoryCode
    {
        public const string Inst = "INST"; // academic
        public const string Corp = "CORP"; // for profit
        public const string Govt = "GOVT"; // law makers
    }

    public static class EstablishmentCategoryExtensions
    {
        public static EstablishmentCategory ByCode(this IEnumerable<EstablishmentCategory> query, string code)
        {
            return (query != null)
                ? query.SingleOrDefault(c =>
                    c.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
                : null;
        }
    }

}