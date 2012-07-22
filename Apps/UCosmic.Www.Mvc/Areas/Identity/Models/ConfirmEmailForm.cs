using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class ConfirmEmailForm : IModelConfirmAndRedeem
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Token { get; set; }
        public const string TokenPropertyName = "Token";

        [HiddenInput(DisplayValue = false)]
        public EmailConfirmationIntent Intent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsUrlConfirmation { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = SecretCodeDisplayName, Prompt = SecretCodeDisplayPrompt)]
        [Remote("ValidateSecretCode", "ConfirmEmail", "Identity", HttpMethod = "POST", AdditionalFields = "Token,Intent")]
        public string SecretCode { get; set; }
        public const string SecretCodePropertyName = "SecretCode";
        public const string SecretCodeDisplayName = "Confirmation Code";
        public const string SecretCodeDisplayPrompt = "Copy & paste your secret Confirmation Code here";
    }

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailForm>
    {
        public const string FailedBecauseSecretCodeWasEmpty = "Please enter a confirmation code.";
        public const string FailedBecauseSecretCodeWasIncorrect = "Invalid confirmation code, please try again.";
        public const string FailedBecauseOfInconsistentData = "An unexpected error occurred while trying to confirm your email address.";

        public ConfirmEmailValidator(IQueryEntities entities)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailConfirmation confirmation = null;

            RuleFor(p => p.SecretCode)
                // secret cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseSecretCodeWasEmpty)
                // token must match a confirmation
                .Must((o, p) => ValidateEmailConfirmation.TokenMatchesEntity(o.Token, entities, out confirmation))
                    .WithMessage(FailedBecauseOfInconsistentData)
                // intent must match entity
                .Must((o, p) => ValidateEmailConfirmation.IntentIsCorrect(confirmation, o.Intent))
                    .WithMessage(FailedBecauseOfInconsistentData)
            ;

            RuleFor(p => p.SecretCode)
                // secret must match entity
                .Must(p => ValidateEmailConfirmation.SecretCodeIsCorrect(confirmation, p))
                    .When(p =>
                        !string.IsNullOrWhiteSpace(p.SecretCode) &&
                        confirmation != null &&
                        ValidateEmailConfirmation.IntentIsCorrect(confirmation, p.Intent))
                    .WithMessage(FailedBecauseSecretCodeWasIncorrect)
            ;
        }
    }

    public static class ConfirmEmailProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ConfirmEmailProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailConfirmation, ConfirmEmailForm>()
                    // SECRET CODE MUST NOT MAP FROM ENTITY TO MODEL!
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