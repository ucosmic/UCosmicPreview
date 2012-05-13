using System.Collections.Generic;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class MyHomePage : Page
    {
        public MyHomePage(IWebDriver browser) : base(browser) { }
        public const string TitleText = "Personal Home";
        public override string Title { get { return TitleText; } }
        public override string Path { get { return MVC.Identity.MyHome.Get().AsPath(); } }

        public override IEnumerable<Content> NestedContents
        {
            get { return new[] { new UpdateNameFormContent(Browser), }; }
        }

        public const string AffiliationDetailsLabel = "Affiliation Details";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { AffiliationDetailsLabel, "div.affiliation div.affiliation-type-info" },
            };

        public override IDictionary<string, string> Fields { get { return FieldCss; } }
    }
}
