using System;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class ManageEditPage : ManageFormPage
    {
        public ManageEditPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Institutional Agreement Edit"; } }
        public override string Path
        {
            get
            {
                var guid = Guid.NewGuid();
                return MVC.InstitutionalAgreements.ManagementForms.Post(guid).AsPath()
                    .Replace(guid.ToString(), UrlPathVariableToken);
            }
        }
    }
}
