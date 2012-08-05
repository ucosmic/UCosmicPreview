namespace UCosmic.Domain.Establishments
{
    public class EstablishmentCategory : Entity
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
}