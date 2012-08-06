namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceBelongTo : Entity
    {
        protected internal GeoPlanetPlaceBelongTo()
        {
        }

        public int PlaceWoeId { get; protected internal set; }
        public virtual GeoPlanetPlace GeoPlanetPlace { get; protected internal set; }

        public int Rank { get; protected internal set; }

        public int BelongToWoeId { get; protected internal set; }
        public virtual GeoPlanetPlace BelongsTo { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", BelongsTo, BelongsTo.Type);
        }
    }
}