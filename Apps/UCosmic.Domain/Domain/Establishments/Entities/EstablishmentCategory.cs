namespace UCosmic.Domain.Establishments
{
    public class EstablishmentCategory : Entity
    {
        protected internal EstablishmentCategory()
        {
        }

        public string Code { get; protected internal set; }

        public string EnglishName { get; protected internal set; }
        public string EnglishPluralName { get; protected internal set; }
    }
}