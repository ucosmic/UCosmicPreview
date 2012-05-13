using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class SignDownPage : Page
    {
        public SignDownPage(IWebDriver browser) : base(browser) { }
        public const string TitleText = "Sign Down";
        public override string Title { get { return TitleText; } }
        public override string Path { get { return MVC.Identity.SignDown.Get().AsPath(); } }
    }
}
