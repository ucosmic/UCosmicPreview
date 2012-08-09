namespace UCosmic
{
    public class PagerOptions
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageNumber { get { return PageIndex + 1; } set { PageIndex = value - 1; } }

        public static readonly PagerOptions All = new PagerOptions { PageSize = int.MaxValue, };
    }
}