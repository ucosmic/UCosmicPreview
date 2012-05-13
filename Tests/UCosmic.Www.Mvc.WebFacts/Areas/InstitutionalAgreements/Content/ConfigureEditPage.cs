using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class ConfigureEditPage : ConfigureFormPage
    {
        public ConfigureEditPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Institutional Agreement Configure Module"; } }
        public override string Path { get { return MVC.InstitutionalAgreements.ConfigurationForms.Edit().AsPath(); } }
    }
}
