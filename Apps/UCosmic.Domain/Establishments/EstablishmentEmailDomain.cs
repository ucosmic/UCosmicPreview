namespace UCosmic.Domain.Establishments
{
    public class EstablishmentEmailDomain : RevisableEntity
    {
        public string Value { get; set; }

        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
    }

}