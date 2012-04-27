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

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public ConfirmEmailValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.SecretCode)

                // secret cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailConfirmationFailedBecauseSecretCodeWasEmpty)

                // token must match a confirmation
                .Must(ValidateEmailConfirmationTokenMatchesEntity).WithMessage(
                    ValidateEmailConfirmationFailedBecauseOfInconsistentData)

                // intent cannot be empty
                .Must(ValidateEmailConfirmationIntentIsNotEmpty).WithMessage(
                    ValidateEmailConfirmationFailedBecauseOfInconsistentData)

                // intent must match entity
                .Must(ValidateEmailConfirmationIntentIsCorrect).WithMessage(
                    ValidateEmailConfirmationFailedBecauseOfInconsistentData)
            ;

            RuleFor(p => p.SecretCode)

                // secret must match entity
                .Must(ValidateEmailConfirmationSecretCodeIsCorrect).WithMessage(
                    ValidateEmailConfirmationFailedBecauseSecretCodeWasIncorrect)
                    .Unless(p => _confirmation != null && _confirmation.IsRedeemed)
            ;

            RuleFor(p => p.IsExpired)

                // cannot be expired
                .Must(ValidateEmailConfirmationIsNotExpired)
                .Unless(p => _confirmation == null)
            ;

            RuleFor(p => p.IsRedeemed)

                // cannot be redeemed
                .Must(ValidateEmailConfirmationIsNotRedeemed)
                .Unless(p => _confirmation == null)
            ;
        }

        private EmailConfirmation _confirmation;

        public const string ValidateEmailConfirmationFailedBecauseSecretCodeWasEmpty = "Please enter a confirmation code.";

        public const string ValidateEmailConfirmationFailedBecauseOfInconsistentData = "An unexpected error occurred while trying to confirm your email address.";

        private bool ValidateEmailConfirmationTokenMatchesEntity(ConfirmEmailForm model, string secretCode)
        {
            return ValidateEmailConfirmation.TokenMatchesEntity(model.Token, _queryProcessor, out _confirmation);
        }

        public const string ValidateEmailConfirmationFailedBecauseSecretCodeWasIncorrect = "Invalid confirmation code, please try again.";

        private bool ValidateEmailConfirmationSecretCodeIsCorrect(string secretCode)
        {
            return ValidateEmailConfirmation.SecretCodeIsCorrect(_confirmation, secretCode);
        }

        private static bool ValidateEmailConfirmationIntentIsNotEmpty(ConfirmEmailForm model, string secretCode)
        {
            return !string.IsNullOrWhiteSpace(model.Intent);
        }

        private bool ValidateEmailConfirmationIntentIsCorrect(ConfirmEmailForm model, string secretCode)
        {
            return ValidateEmailConfirmation.IntentIsCorrect(_confirmation, model.Intent);
        }

        private bool ValidateEmailConfirmationIsNotExpired(bool isExpired)
        {
            return ValidateEmailConfirmation.IsNotExpired(_confirmation);
        }

        private bool ValidateEmailConfirmationIsNotRedeemed(bool isRedeemed)
        {
            return ValidateEmailConfirmation.IsNotRedeemed(_confirmation);
        }
    }

    public static class ConfirmEmailProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ConfirmEmailProfiler));
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