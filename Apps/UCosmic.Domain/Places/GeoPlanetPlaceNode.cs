namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceNode : Entity
    {
        public int AncestorId { get; set; }
        public virtual GeoPlanetPlace Ancestor { get; set; }

        public int OffspringId { get; set; }
        public virtual GeoPlanetPlace Offspring { get; set; }

        public int Separation { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ---{1}--> {2}", Ancestor.EnglishName, Separation, Offspring.EnglishName);
        }
    }
}