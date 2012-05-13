using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using System.Web.Security;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class CreatePasswordPage : Page
    {
        public CreatePasswordPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Create Password"; } }
        public override string Path
        {
            get
            {
                var guid = Guid.NewGuid();
                return MVC.Identity.CreatePassword.Get(guid).AsPath()
                    .Replace(guid.ToString(), UrlPathVariableToken);
            }
        }

        public const string PasswordLabel = "Password";
        public const string PasswordConfirmationLabel = "Password Confirmation";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { PasswordLabel, "input#Password" },
                { PasswordLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Password]" },
                { PasswordLabel.ErrorTextKey("Required"), 
                    CreatePasswordValidator.FailedBecausePasswordWasEmpty },
                { PasswordLabel.ErrorTextKey("'Too Short'"), 
                    CreatePasswordValidator.FailedBecausePasswordWasTooShort
                        .FormatWith(Membership.MinRequiredPasswordLength) },

                { PasswordConfirmationLabel, "input#PasswordConfirmation" },
                { PasswordConfirmationLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=PasswordConfirmation]" },
                { PasswordConfirmationLabel.ErrorTextKey("Required"), 
                    CreatePasswordValidator.FailedBecausePasswordConfirmationWasEmpty },
                { PasswordConfirmationLabel.ErrorTextKey("'No Match'"), 
                    CreatePasswordValidator.FailedBecausePasswordConfirmationDidNotEqualPassword },
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
