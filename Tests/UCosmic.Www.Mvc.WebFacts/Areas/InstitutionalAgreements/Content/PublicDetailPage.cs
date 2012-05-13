using System;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class PublicDetailPage : Page
    {
        public PublicDetailPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Public Institutional Agreement Detail"; } }
        public override string Path
        {
            get
            {
                var guid = Guid.NewGuid();
                return MVC.InstitutionalAgreements.PublicSearch.Info(guid).AsPath()
                    .Replace(guid.ToString(), UrlPathVariableToken);
            }
        }
    }
}
