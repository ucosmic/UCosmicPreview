using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Roles
{
    public class ManageListPage : Page
    {
        public ManageListPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Role Management"; } }
        public override string Path { get { return MVC.Roles.Roles.Browse().AsPath(); } }
    }
}
