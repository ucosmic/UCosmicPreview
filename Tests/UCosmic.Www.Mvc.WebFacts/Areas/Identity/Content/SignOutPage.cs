using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class SignOutPage : Page
    {
        public SignOutPage(IWebDriver browser) : base(browser) { }
        public const string TitleText = "Sign Out";
        public override string Title { get { return TitleText; } }
        public override string Path { get { return MVC.Identity.SignOut.Get().AsPath(); } }
    }
}
