using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class SignInForm : IModelSigningEmail
    {
        public SignInForm(HttpContextBase httpContext, TempDataDictionary tempData, string returnUrl)
        {
            var cookieValue = httpContext.SigningEmailAddressCookie();
            EmailAddress = cookieValue ?? tempData.SigningEmailAddress();
            RememberMe = !string.IsNullOrWhiteSpace(cookieValue);
            ReturnUrl = returnUrl;
        }

        public SignInForm() { }

        [DataType(DataType.EmailAddress)]
        [UIHint(SignOnForm.EmailAddressUiHint)]
        [Display(Name = SignOnForm.EmailAddressDisplayName, Prompt = SignOnForm.EmailAddressDisplayPrompt)]
        public string EmailAddress { get; set; }

        [UIHint("PasswordField")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your password")]
        [Remote("ValidatePassword", "SignIn", "Identity", HttpMethod = "POST", AdditionalFields = "EmailAddress")]
        public string Password { get; set; }
        internal const string PasswordPropertyName = "Password";

        [UIHint("RememberMeField")]
        [Display(Name = "Remember my email address on this browser.")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }

    public class SignInValidator : AbstractValidator<SignInForm>
    {
        public const string FailedBecausePasswordWasEmpty = "Password is required.";
        public const string FailedBecauseIsLockedOut = "Your account has been locked after {0} incorrect password attempts. Please reset your password.";
        public const string FailedBecausePasswordWasIncorrect = "You entered an incorrect password. You have {0} more attempt{1} before your account is locked.";

        private readonly IStorePasswords _passwords;
        private readonly HttpSessionStateBase _session;

        public SignInValidator(IStorePasswords passwords)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            _passwords = passwords;

            _session = HttpContext.Current != null && HttpContext.Current.Session != null
                ? new HttpSessionStateWrapper(HttpContext.Current.Session)
                : null;

            RuleFor(p => p.Password)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecausePasswordWasEmpty)
                // account cannot be locked out
                .Must(ValidateIsNotLockedOut)
                    .WithMessage(FailedBecauseIsLockedOut,
                        p => _passwords.MaximumPasswordAttempts)
                // validate the password
                .Must(ValidatePasswordIsCorrect)
                    .WithMessage(FailedBecausePasswordWasIncorrect,
                        p => _passwords.MaximumPasswordAttempts - _session.FailedPasswordAttempts(),
                        p => (_passwords.MaximumPasswordAttempts - _session.FailedPasswordAttempts() == 1) ? string.Empty : "s")
                // check lockout again, this may be last attempt
                .Must(ValidateIsNotLockedOut)
                    .WithMessage(FailedBecauseIsLockedOut,
                        p => _passwords.MaximumPasswordAttempts)
            ;
        }

        private bool ValidatePasswordIsCorrect(SignInForm model, string password)
        {
            var isValid = _passwords.Validate(model.EmailAddress, password);
            if (!isValid)
            {
                _session.FailedPasswordAttempt();
                var isLockedOut = !ValidateIsNotLockedOut(model, password);
                if (isLockedOut) return true;
            }
            return isValid;
        }

        private bool ValidateIsNotLockedOut(SignInForm model, string password)
        {
            var isLockedOut = _passwords.IsLockedOut(model.EmailAddress);
            if (isLockedOut) _session.FailedPasswordAttempts(false);
            return !isLockedOut;
        }
    }
}