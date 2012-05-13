using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Common
{
    public class HomePage : Page
    {
        public HomePage(IWebDriver browser) : base(browser) { }
        public const string TitleText = "Home";
        public override string Title { get { return TitleText; } }
        public override string Path { get { return MVC.Common.Features.Releases().AsPath(); } }
    }
}
