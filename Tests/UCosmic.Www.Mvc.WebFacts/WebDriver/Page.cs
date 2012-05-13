using OpenQA.Selenium;

namespace UCosmic.Www.Mvc
{
    public abstract class Page : Content
    {
        public Page() : this(null) { }
        public Page(IWebDriver browser) : base(browser) { }

        public abstract string Path { get; }

        public string AbsoluteUrl
        {
            get { return Path.ToAbsoluteUrl(); }
        }

        public const string UrlPathVariableToken = "[PathVar]";
    }
}
