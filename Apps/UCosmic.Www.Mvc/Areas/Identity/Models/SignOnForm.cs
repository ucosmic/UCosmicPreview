using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class SignOnForm : IModelSigningEmail
    {
        public SignOnForm(HttpContextBase httpContext, string returnUrl)
        {
            var cookieValue = httpContext.SigningEmailAddressCookie();
            EmailAddress = cookieValue;
            RememberMe = !string.IsNullOrWhiteSpace(cookieValue);
            ReturnUrl = returnUrl;
        }

        public SignOnForm() { }

        [UIHint(EmailAddressUiHint)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = EmailAddressDisplayName, Prompt = EmailAddressDisplayPrompt)]
        [Remote("ValidateEmailAddress", "SignOn", "Identity", HttpMethod = "POST")]
        public string EmailAddress { get; set; }
        public const string EmailAddressDisplayName = "Email address";
        public const string EmailAddressDisplayPrompt = "Enter your work email address";
        internal const string EmailAddressUiHint = "EmailAddressField";
        internal const string EmailAddressPropertyName = "EmailAddress";

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [UIHint("RememberMeField")]
        [Display(Name = "Remember my email address on this browser.")]
        public bool RememberMe { get; set; }
    }

    public class SignOnValidator : AbstractValidator<SignOnForm>
    {
        public const string FailedBecauseEmailAddressWasEmpty = "Email address is required.";
        public const string FailedBecauseEmailAddressIsNotValidEmailAddress = "Please enter a valid email address.";
        public const string FailedBecauseEstablishmentIsNotEligible = "Sorry, but the email address \"{0}\" is not eligible at this time.";

        public SignOnValidator(IQueryEntities entities)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            Establishment establishment = null;

            RuleFor(p => p.EmailAddress)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseEmailAddressWasEmpty)
                .EmailAddress()
                    .WithMessage(FailedBecauseEmailAddressIsNotValidEmailAddress)
                // establishment must exist
                .Must(p => ValidateEstablishment.EmailMatchesEntity(p, entities, out establishment))
                    .WithMessage(FailedBecauseEstablishmentIsNotEligible,
                        p => p.EmailAddress)
                // establishment must be a member
                .Must(p => establishment.IsMember)
                    .WithMessage(FailedBecauseEstablishmentIsNotEligible,
                        p => p.EmailAddress)
            ;
        }
    }
}
