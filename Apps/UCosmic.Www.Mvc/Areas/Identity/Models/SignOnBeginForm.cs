using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class SignOnBeginForm : IReturnUrl
    {
        [UIHint("SignOnEmailAddress")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = EmailAddressDisplayName, Prompt = EmailAddressDisplayPrompt)]
        [Remote("ValidateEmailAddress", "SignOn", "Identity", HttpMethod = "POST")]
        public string EmailAddress { get; set; }
        public const string EmailAddressDisplayName = "Email Address";
        public const string EmailAddressDisplayPrompt = "Enter your work email address";
        public const string EmailAddressPropertyName = "EmailAddress";

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }

    public class SignOnBeginValidator : AbstractValidator<SignOnBeginForm>
    {
        public const string FailedBecauseEmailAddressWasEmpty = "Email address is required.";
        public const string FailedBecauseEmailAddressIsNotValidEmailAddress = "Please enter a valid email address.";
        public const string FailedBecauseEstablishmentIsNotEligible = "Sorry, but the email address \"{0}\" is not eligible at this time.";

        public SignOnBeginValidator(IProcessQueries queryProcessor)
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
                .Must(p => ValidateEstablishment.EmailMatchesEntity(p, queryProcessor, out establishment))
                    .WithMessage(FailedBecauseEstablishmentIsNotEligible,
                        p => p.EmailAddress)
                // establishment must be a member
                .Must(p => ValidateEstablishment.IsMember(establishment))
                    .WithMessage(FailedBecauseEstablishmentIsNotEligible,
                        p => p.EmailAddress)
            ;
        }
    }
}
