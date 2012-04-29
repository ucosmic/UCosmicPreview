using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class ConfirmEmailForm
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Token { get; set; }
        public const string TokenPropertyName = "Token";

        [DataType(DataType.Password)]
        [Display(Name = SecretCodeDisplayName, Prompt = SecretCodeDisplayPrompt)]
        [Remote("ValidateSecretCode", "ConfirmEmail", "Identity", HttpMethod = "POST", AdditionalFields = "Token,Intent")]
        public string SecretCode { get; set; }
        public const string SecretCodePropertyName = "SecretCode";
        public const string SecretCodeDisplayName = "Confirmation Code";
        public const string SecretCodeDisplayPrompt = "Copy & paste your secret Confirmation Code here";

        [HiddenInput(DisplayValue = false)]
        public string Intent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsUrlConfirmation { get; set; }

        public bool IsExpired { get; set; }
        public const string IsExpiredPropertyName = "IsExpired";

        public bool IsRedeemed { get; set; }
        public const string IsRedeemedPropertyName = "IsRedeemed";
    }

    public class ConfirmEmailFormValidator : AbstractValidator<ConfirmEmailForm>
    {
        public const string FailedBecauseSecretCodeWasEmpty = "Please enter a confirmation code.";
        public const string FailedBecauseSecretCodeWasIncorrect = "Invalid confirmation code, please try again.";
        public const string FailedBecauseOfInconsistentData = "An unexpected error occurred while trying to confirm your email address.";

        public ConfirmEmailFormValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailConfirmation confirmation = null;

            RuleFor(p => p.SecretCode)
                // secret cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseSecretCodeWasEmpty)
                // token must match a confirmation
                .Must((o, p) => ValidateEmailConfirmation.TokenMatchesEntity(o.Token, queryProcessor, out confirmation))
                    .WithMessage(FailedBecauseOfInconsistentData)
                // intent cannot be empty
                .Must((o, p) => !string.IsNullOrWhiteSpace(o.Intent))
                    .WithMessage(FailedBecauseOfInconsistentData)
                // intent must match entity
                .Must((o, p) => ValidateEmailConfirmation.IntentIsCorrect(confirmation, o.Intent))
                    .WithMessage(FailedBecauseOfInconsistentData)
            ;

            RuleFor(p => p.SecretCode)
                // secret must match entity
                .Must(p => ValidateEmailConfirmation.SecretCodeIsCorrect(confirmation, p))
                // allow redeemed confirmation to pass through for redirect..?
                    //.Unless(p => confirmation != null && confirmation.IsRedeemed)
                    .When(p =>
                        !string.IsNullOrWhiteSpace(p.SecretCode) &&
                        !string.IsNullOrWhiteSpace(p.Intent) &&
                        confirmation != null &&
                        !confirmation.IsRedeemed &&
                        ValidateEmailConfirmation.IntentIsCorrect(confirmation, p.Intent))
                    .WithMessage(FailedBecauseSecretCodeWasIncorrect)
            ;

            RuleFor(p => p.IsExpired)
                // cannot be expired
                .Must(p => ValidateEmailConfirmation.IsNotExpired(confirmation))
                    .When(p => confirmation != null)
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseIsExpired,
                        p => confirmation.Token, p => confirmation.ExpiresOnUtc)
            ;

            RuleFor(p => p.IsRedeemed)
                // cannot be redeemed
                .Must(p => ValidateEmailConfirmation.IsNotRedeemed(confirmation))
                    .When(p => confirmation != null)
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseIsRedeemed,
                        p => confirmation.Token, p => confirmation.RedeemedOnUtc)
            ;
        }
    }

    public static class ConfirmEmailFormProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ConfirmEmailFormProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailConfirmation, ConfirmEmailForm>()
                    .ForMember(d => d.SecretCode, opt => opt.Ignore())
                    .ForMember(d => d.IsUrlConfirmation, o => o.Ignore())
                ;
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ConfirmEmailForm, RedeemEmailConfirmationCommand>()
                    .ForMember(d => d.Ticket, opt => opt.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}