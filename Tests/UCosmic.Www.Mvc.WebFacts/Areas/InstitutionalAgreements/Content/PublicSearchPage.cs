using System;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class PublicSearchPage : Page
    {
        public PublicSearchPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Institutional Agreement Public Search"; } }
        public override string Path
        {
            get
            {
                var guid = Guid.NewGuid();
                return MVC.InstitutionalAgreements.PublicSearch.Index(guid.ToString()).AsPath()
                    .Replace(guid.ToString(), UrlPathVariableToken);
            }
        }
    }
}
