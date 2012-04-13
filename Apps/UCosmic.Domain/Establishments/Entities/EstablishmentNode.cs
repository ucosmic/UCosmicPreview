namespace UCosmic.Domain.Establishments
{
    public class EstablishmentNode : Entity
    {
        public int AncestorId { get; set; }
        public virtual Establishment Ancestor { get; set; }

        public int OffspringId { get; set; }
        public virtual Establishment Offspring { get; set; }

        public int Separation { get; set; }
    }
}