using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Common.WebPages;

namespace UCosmic.Www.Mvc.Areas.Identity.WebPages
{
    public class SignUpSendEmailPage : WebPageBase
    {
        public SignUpSendEmailPage(IWebDriver driver)
            : base(driver) { }

        protected override string EditorSelector { get { return "#sign_up_editor"; } }

        protected override Dictionary<string, string> SpecToWeb
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "Email Address", "EmailAddress" },
                    { "Check Eligibility", "button" },
                    { "Send Confirmation Email", "submit" },
                    { "Congratulations, your email address is eligible!", "p .eligible" },
                    { "green checkmark icon", ".check-eligibility img.eligible" },
                };
            }
        }

    }
}
