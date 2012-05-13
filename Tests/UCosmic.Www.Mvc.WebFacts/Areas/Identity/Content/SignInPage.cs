using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class SignInPage : Page
    {
        public SignInPage(IWebDriver browser) : base(browser) { }
        public const string TitleText = "Enter Password";
        public override string Title { get { return TitleText; } }
        public override string Path { get { return MVC.Identity.SignIn.Get().AsPath(); } }

        public const string PasswordLabel = "Password";
        public const string SubmitButtonLabel = "Sign On";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { PasswordLabel, "input#Password" },
                { PasswordLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Password]" },
                { PasswordLabel.ErrorTextKey("Required"),
                    SignInValidator.FailedBecausePasswordWasEmpty },
                { PasswordLabel.ErrorTextKey("'Invalid with 4 remaining attempts'"),
                    SignInValidator.FailedBecausePasswordWasIncorrect.FormatWith(4, 's') },
            };

        public override IDictionary<string, string> Fields
        {
            get { return FieldCss; }
        }
    }
}
