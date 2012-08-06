namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceNode : Entity
    {
        protected internal GeoPlanetPlaceNode()
        {
        }

        public int AncestorId { get; protected internal set; }
        public virtual GeoPlanetPlace Ancestor { get; protected internal set; }

        public int OffspringId { get; protected internal set; }
        public virtual GeoPlanetPlace Offspring { get; protected internal set; }

        public int Separation { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0} ---{1}--> {2}", Ancestor.EnglishName, Separation, Offspring.EnglishName);
        }
    }
}