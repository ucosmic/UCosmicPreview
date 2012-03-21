namespace UCosmic.Domain.Places
{
    public class GeoNamesToponymNode : Entity
    {
        public int AncestorId { get; set; }
        public virtual GeoNamesToponym Ancestor { get; set; }

        public int OffspringId { get; set; }
        public virtual GeoNamesToponym Offspring { get; set; }

        public int Separation { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ---{1}--> {2}", Ancestor.Name, Separation, Offspring.Name);
        }
    }
}