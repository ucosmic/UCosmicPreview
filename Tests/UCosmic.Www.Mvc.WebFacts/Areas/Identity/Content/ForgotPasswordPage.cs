using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class ForgotPasswordPage : Page
    {
        public ForgotPasswordPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Forgot Password"; } }
        public override string Path { get { return MVC.Identity.ForgotPassword.Get().AsPath(); } }

        public const string EmailAddressLabel = "Email Address";
        public const string SubmitButtonLabel = "Next >>";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { EmailAddressLabel, "input#EmailAddress" },
                { EmailAddressLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=EmailAddress]" },
                { EmailAddressLabel.ErrorTextKey("Required"),
                    ForgotPasswordValidator.FailedBecauseEmailAddressWasEmpty },
                { EmailAddressLabel.ErrorTextKey("Invalid"),
                    ForgotPasswordValidator.FailedBecauseEmailAddressWasNotValidEmailAddress },
                { EmailAddressLabel.ErrorTextKey("'test@gmail.com not found'"),
                    ForgotPasswordValidator.FailedBecauseUserNameMatchedNoLocalMember
                        .FormatWith("test@gmail.com") },
            };

        public override IDictionary<string, string> Fields
        {
            get { return FieldCss; }
        }
    }
}
