using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class SignOnPage : Page
    {
        public SignOnPage(IWebDriver browser) : base(browser) { }
        public const string TitleText = "Sign On";
        public override string Title { get { return TitleText; } }
        public override string Path { get { return MVC.Identity.SignOn.Get().AsPath(); } }

        public const string EmailAddressLabel = "Email Address";
        public const string SubmitButtonLabel = "Next >>";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { EmailAddressLabel, "input#EmailAddress" },
                { EmailAddressLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=EmailAddress]" },
                { EmailAddressLabel.ErrorTextKey("Required"),
                    SignOnValidator.FailedBecauseEmailAddressWasEmpty },
                { EmailAddressLabel.ErrorTextKey("Invalid"),
                    SignOnValidator.FailedBecauseEmailAddressIsNotValidEmailAddress },
                { EmailAddressLabel.ErrorTextKey("'test@gmail.com is Ineligible'"),
                    SignOnValidator.FailedBecauseEstablishmentIsNotEligible
                        .FormatWith("test@gmail.com") },
            };

        public override IDictionary<string, string> Fields
        {
            get { return FieldCss; }
        }
    }
}
