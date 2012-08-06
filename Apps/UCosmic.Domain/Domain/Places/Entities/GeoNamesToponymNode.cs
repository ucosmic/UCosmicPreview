namespace UCosmic.Domain.Places
{
    public class GeoNamesToponymNode : Entity
    {
        protected internal GeoNamesToponymNode()
        {
        }

        public int AncestorId { get; protected internal set; }
        public virtual GeoNamesToponym Ancestor { get; protected internal set; }

        public int OffspringId { get; protected internal set; }
        public virtual GeoNamesToponym Offspring { get; protected internal set; }

        public int Separation { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0} ---{1}--> {2}", Ancestor.Name, Separation, Offspring.Name);
        }
    }
}