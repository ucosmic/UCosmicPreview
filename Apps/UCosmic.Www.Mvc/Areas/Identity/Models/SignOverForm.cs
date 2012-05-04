using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation;
using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class SignOverForm : IModelSigningEmail
    {
        [UIHint(SignOnForm.EmailAddressUiHint)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Username", Prompt = "Type a username to impersonate")]
        [Remote("ValidateEmailAddress", "SignOver", "Identity", HttpMethod = "POST")]
        public string EmailAddress { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        public bool RememberMe
        {
            get { return false; }
        }
    }

    public class SignOverValidator : AbstractValidator<SignOverForm>
    {
        public const string FailedBecauseEmailAddressWasEmpty = "Username is required.";
        public const string FailedBecauseEmailAddressMatchedNoUser = "Username '{0}' could not be found.";

        public SignOverValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EmailAddress)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseEmailAddressWasEmpty)
                // must be in db
                .Must(p => ValidateUser.NameMatchesEntity(p, queryProcessor))
                    .WithMessage(FailedBecauseEmailAddressMatchedNoUser,
                        p => p.EmailAddress)
            ;
        }
    }
}