using System.Collections.Generic;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class SignUpPage : Page
    {
        public SignUpPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Sign Up"; } }
        public override string Path { get { return MVC.Identity.SignUp.Get().AsPath(); } }

        public const string AcceptTermsLabel = @"""Email me a confirmation code...""";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { AcceptTermsLabel, "input#AcceptTerms" },
            };

        public override IDictionary<string, string> Fields
        {
            get { return FieldCss; }
        }
    }
}
