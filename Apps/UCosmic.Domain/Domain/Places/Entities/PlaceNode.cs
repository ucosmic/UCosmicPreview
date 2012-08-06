namespace UCosmic.Domain.Places
{
    public class PlaceNode : Entity
    {
        protected internal PlaceNode()
        {
        }

        public int AncestorId { get; protected internal set; }
        public virtual Place Ancestor { get; protected internal set; }

        public int OffspringId { get; protected internal set; }
        public virtual Place Offspring { get; protected internal set; }

        public int Separation { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("({0}) {1}", Separation, Offspring.OfficialName);
        }
    }
}