namespace UCosmic.Domain.Establishments
{
    public class EstablishmentEmailDomain : RevisableEntity
    {
        protected internal EstablishmentEmailDomain()
        {
        }

        public string Value { get; protected internal set; }

        public int EstablishmentId { get; protected internal set; }
        public virtual Establishment Establishment { get; protected internal set; }
    }

}