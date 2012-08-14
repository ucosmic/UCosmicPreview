using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class UpdateEmailSpellingPage : Page
    {
        public UpdateEmailSpellingPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Update Email Spelling"; } }
        public override string Path
        {
            get
            {
                const int number = 1;
                return MVC.Identity.UpdateEmailValue.Get(number).AsPath()
                    .Replace(number.ToObjectString(), UrlPathVariableToken);
            }
        }

        public const string NewSpellingLabel = "New Spelling";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { NewSpellingLabel, "input#Value" },
                { NewSpellingLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Value]" },
                { NewSpellingLabel.ErrorTextKey("Invalid"),
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively },
            };

        public override IDictionary<string, string> Fields
        {
            get
            {
                return FieldCss;
            }
        }
}
}
