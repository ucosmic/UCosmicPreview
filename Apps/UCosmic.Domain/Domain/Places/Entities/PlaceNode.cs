namespace UCosmic.Domain.Places
{
    public class PlaceNode : Entity
    {
        public int AncestorId { get; set; }
        public virtual Place Ancestor { get; set; }

        public int OffspringId { get; set; }
        public virtual Place Offspring { get; set; }

        public int Separation { get; set; }

        public override string ToString()
        {
            return string.Format("({0}) {1}", Separation, Offspring.OfficialName);
        }
    }
}