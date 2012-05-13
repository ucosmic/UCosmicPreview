using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class ConfigureAddPage : ConfigureFormPage
    {
        public ConfigureAddPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Institutional Agreement Set Up Module"; } }
        public override string Path { get { return MVC.InstitutionalAgreements.ConfigurationForms.Add().AsPath(); } }
    }
}
