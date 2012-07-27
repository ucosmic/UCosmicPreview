using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using System.Web.Security;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class ChangePasswordPage : Page
    {
        public ChangePasswordPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Change Password"; } }
        public override string Path
        {
            get
            {
                return MVC.Identity.UpdatePassword.Get().AsPath();
            }
        }

        public const string CurrentPasswordLabel = "Current Password";
        public const string NewPasswordLabel = "New Password";
        public const string NewPasswordConfirmationLabel = "New Password Confirmation";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { CurrentPasswordLabel, "input#CurrentPassword" },
                { CurrentPasswordLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=CurrentPassword]" },
                { CurrentPasswordLabel.ErrorTextKey("Required"),
                    UpdatePasswordValidator.FailedBecauseOldPasswordWasEmpty },
                { CurrentPasswordLabel.ErrorTextKey("Invalid"),
                    UpdatePasswordValidator.FailedBecauseCurrentPasswordWasIncorrect
                        .FormatWith(4, 's') },

                { NewPasswordLabel, "input#NewPassword" },
                { NewPasswordLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=NewPassword]" },
                { NewPasswordLabel.ErrorTextKey("Required"),
                    UpdatePasswordValidator.FailedBecauseNewPasswordWasEmpty },
                { NewPasswordLabel.ErrorTextKey("Too Short"),
                    UpdatePasswordValidator.FailedBecauseNewPasswordWasTooShort
                        .FormatWith(Membership.MinRequiredPasswordLength) },

                { NewPasswordConfirmationLabel, "input#ConfirmPassword" },
                { NewPasswordConfirmationLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=ConfirmPassword]" },
                { NewPasswordConfirmationLabel.ErrorTextKey("Required"),
                    UpdatePasswordValidator.FailedBecauseNewPasswordConfirmationWasEmpty },
                { NewPasswordConfirmationLabel.ErrorTextKey("No Match"),
                    UpdatePasswordValidator.FailedBecauseNewPasswordConfirmationDidNotEqualPassword },
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
