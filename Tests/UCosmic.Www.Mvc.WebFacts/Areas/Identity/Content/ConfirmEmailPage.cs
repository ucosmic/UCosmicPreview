using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class ConfirmEmailPage : Page
    {
        public ConfirmEmailPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Confirm Email Ownership"; } }
        public override string Path
        {
            get
            {
                var guid = Guid.NewGuid();
                return MVC.Identity.ConfirmEmail.Get(guid, null).AsPath()
                    .Replace(guid.ToString(), UrlPathVariableToken);
            }
        }

        protected override string MailExcerptStart { get { return "enter the following Confirmation Code:\r\n"; } }
        protected override string MailExcerptEnd { get { return "\r\n"; } }

        public const string ConfirmationCodeLabel = "Confirmation Code";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { ConfirmationCodeLabel, "input#SecretCode" },
                { ConfirmationCodeLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=SecretCode]" },
                { ConfirmationCodeLabel.ErrorTextKey("Required"), ConfirmEmailValidator.FailedBecauseSecretCodeWasEmpty },
                { ConfirmationCodeLabel.ErrorTextKey("Invalid"), ConfirmEmailValidator.FailedBecauseSecretCodeWasIncorrect },
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
