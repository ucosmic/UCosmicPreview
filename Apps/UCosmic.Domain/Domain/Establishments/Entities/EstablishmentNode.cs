namespace UCosmic.Domain.Establishments
{
    public class EstablishmentNode : Entity
    {
        protected internal EstablishmentNode()
        {
        }

        public int AncestorId { get; protected internal set; }
        public virtual Establishment Ancestor { get; protected internal set; }

        public int OffspringId { get; protected internal set; }
        public virtual Establishment Offspring { get; protected internal set; }

        public int Separation { get; protected internal set; }
    }
}