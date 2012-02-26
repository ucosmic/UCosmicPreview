namespace UCosmic.Www.Mvc.Areas.Common.Models.Features
{
    public class FeaturePreview
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsCurrentlyViewed { get; set; }
        public bool IsLatest { get; set; }
    }
}