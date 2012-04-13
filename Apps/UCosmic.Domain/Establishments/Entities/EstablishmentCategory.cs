using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentCategory : RevisableEntity
    {
        public string EnglishName { get; set; }

        public string EnglishPluralName { get; set; }

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