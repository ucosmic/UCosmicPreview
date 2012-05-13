using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class ManageListPage : Page
    {
        public ManageListPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Institutional Agreement Management"; } }
        public override string Path { get { return MVC.InstitutionalAgreements.ManagementForms.Browse().AsPath(); } }
    }
}
