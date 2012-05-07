using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class SignOnForm : WebPageBase
    {
        public SignOnForm(IWebDriver driver) : base(driver) { }

        public const string EmailAddressLabel = "Email address";
        public const string SubmitButtonLabel = "Next >>";

        protected override Dictionary<string, string> SpecToWeb
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { EmailAddressLabel, "EmailAddress" },
                    { EmailAddressLabel + "[ErrorText='Required']", 
                        SignOnValidator.FailedBecauseEmailAddressWasEmpty },
                    { EmailAddressLabel + "[ErrorText='Invalid']", 
                        SignOnValidator.FailedBecauseEmailAddressIsNotValidEmailAddress },
                    { EmailAddressLabel + "[ErrorText=''test@gmail.com is Ineligible'']", 
                        string.Format(SignOnValidator.FailedBecauseEstablishmentIsNotEligible, "test@gmail.com") },
                };
            }
        }

    }
}
