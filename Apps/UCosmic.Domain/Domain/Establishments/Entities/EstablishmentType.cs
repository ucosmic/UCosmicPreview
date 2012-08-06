namespace UCosmic.Domain.Establishments
{
    public class EstablishmentType : RevisableEntity
    {
        protected internal EstablishmentType()
        {
        }

        public string CategoryCode { get; protected internal set; }
        public virtual EstablishmentCategory Category { get; protected internal set; }

        public string EnglishName { get; protected internal set; }

        public string EnglishPluralName { get; protected internal set; }
    }
}