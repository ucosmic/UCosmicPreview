using System;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class ManageAddPage : ManageFormPage
    {
        public ManageAddPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Institutional Agreement Add"; } }
        public override string Path { get { return MVC.InstitutionalAgreements.ManagementForms.Post(null as Guid?).AsPath(); } }
    }
}
