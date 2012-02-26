namespace UCosmic.Domain.Places
{
    public class GeoPlanetPlaceBelongTo
    {
        public int PlaceWoeId { get; set; }
        public virtual GeoPlanetPlace GeoPlanetPlace { get; set; }

        public int Rank { get; set; }

        public int BelongToWoeId { get; set; }
        public virtual GeoPlanetPlace BelongsTo { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", BelongsTo, BelongsTo.Type);
        }
    }
}