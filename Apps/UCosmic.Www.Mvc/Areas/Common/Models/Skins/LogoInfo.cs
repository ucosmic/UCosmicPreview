namespace UCosmic.Www.Mvc.Areas.Common.Models.Skins
{
    public class LogoInfo
    {
        private const string DefaultSkinName = "default";

        public LogoInfo()
        {
            SkinName = DefaultSkinName;
        }

        public string SkinName { get; set; }

        public bool IsDefault
        {
            get { return SkinName == DefaultSkinName; }
        }

        public string Href
        {
            get
            {
                return (SkinName != DefaultSkinName)
                    ? string.Format("http://{0}", SkinName)
                    : "/";
            }
        }
    }
}