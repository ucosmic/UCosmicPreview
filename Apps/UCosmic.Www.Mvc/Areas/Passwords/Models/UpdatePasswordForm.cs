using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    public class UpdatePasswordForm : IReturnUrl
    {
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current password", Prompt = "Type the password you want to change")]
        [Remote("ValidateCurrentPassword", "UpdatePassword", "Passwords", HttpMethod = "POST")]
        public string CurrentPassword { get; set; }
        public const string CurrentPasswordPropertyName = "CurrentPassword";

        [UIHint("StrengthMeteredPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "New password", Prompt = "Type your new password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password confirmation", Prompt = "Type your new password again to confirm")]
        [Remote("ValidateNewPasswordConfirmation", "UpdatePassword", "Passwords", HttpMethod = "POST", AdditionalFields = "NewPassword")]
        public string ConfirmPassword { get; set; }
        public const string NewPasswordConfirmationPropertyName = "ConfirmPassword";
    }

    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordForm>
    {
        public const string FailedBecauseOldPasswordWasEmpty = "You must enter your current password.";
        public const string FailedBecauseIsLockedOut = "Your account has been locked after {0} incorrect password attempts. Please reset your password.";
        public const string FailedBecauseCurrentPasswordWasIncorrect = "The current password you entered is incorrect. You have {0} more attempt{1} before your account is locked.";

        public const string FailedBecauseNewPasswordWasEmpty = "Enter your new password.";
        public const string FailedBecauseNewPasswordWasTooShort = "Your new password must be at least {0} characters long.";

        public const string FailedBecauseNewPasswordConfirmationWasEmpty = "Confirm your new password by typing it again in this field.";
        public const string FailedBecauseNewPasswordConfirmationDidNotEqualPassword = "Your new password and confirmation do not match.";

        private readonly ISignMembers _memberSigner;
        private readonly HttpContextBase _httpContext;
        private readonly HttpSessionStateBase _session;

        public UpdatePasswordValidator(ISignMembers memberSigner)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            _memberSigner = memberSigner;

            _httpContext = HttpContext.Current != null ? new HttpContextWrapper(HttpContext.Current) : null;
            _session = _httpContext != null && _httpContext.Session != null
                ? _httpContext.Session
                : null;

            RuleFor(p => p.CurrentPassword)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseOldPasswordWasEmpty)
                // account cannot be locked out
                .Must(ValidateIsNotLockedOut)
                    .WithMessage(FailedBecauseIsLockedOut,
                        p => _memberSigner.MaximumPasswordAttempts)
                // validate the password
                .Must(ValidatePasswordIsCorrect)
                    .WithMessage(FailedBecauseCurrentPasswordWasIncorrect,
                        p => _memberSigner.MaximumPasswordAttempts - _session.FailedPasswordAttempts(),
                        p => (_memberSigner.MaximumPasswordAttempts - _session.FailedPasswordAttempts() == 1) ? string.Empty : "s")
                // check lockout again, this may be last attempt
                .Must(ValidateIsNotLockedOut)
                    .WithMessage(FailedBecauseIsLockedOut,
                        p => _memberSigner.MaximumPasswordAttempts)
            ;

            RuleFor(p => p.NewPassword)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseNewPasswordWasEmpty)
                // at least 6 characters long
                .Length(memberSigner.MinimumPasswordLength, int.MaxValue)
                    .WithMessage(FailedBecauseNewPasswordWasTooShort,
                        p => memberSigner.MinimumPasswordLength)
            ;

            RuleFor(p => p.ConfirmPassword)
                // can never be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseNewPasswordConfirmationWasEmpty)
            ;

            RuleFor(p => p.ConfirmPassword)
                // equals password unless empty or password failed validation
                .Equal(p => p.NewPassword)
                    .Unless(p =>
                        string.IsNullOrWhiteSpace(p.ConfirmPassword) ||
                        string.IsNullOrWhiteSpace(p.NewPassword) ||
                        p.NewPassword.Length < memberSigner.MinimumPasswordLength)
                    .WithMessage(FailedBecauseNewPasswordConfirmationDidNotEqualPassword)
            ;
        }

        private bool ValidatePasswordIsCorrect(UpdatePasswordForm model, string password)
        {
            var isValid = _memberSigner.Validate(_httpContext.User.Identity.Name, password);
            if (!isValid)
            {
                _session.FailedPasswordAttempt();
                var isLockedOut = !ValidateIsNotLockedOut(model, password);
                if (isLockedOut) return true;
            }
            return isValid;
        }

        private bool ValidateIsNotLockedOut(UpdatePasswordForm model, string password)
        {
            var isLockedOut = _memberSigner.IsLockedOut(_httpContext.User.Identity.Name);
            if (isLockedOut) _session.FailedPasswordAttempts(false);
            return !isLockedOut;
        }
    }
}