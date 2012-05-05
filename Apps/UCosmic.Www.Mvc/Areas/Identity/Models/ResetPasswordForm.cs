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
    public class ResetPasswordForm : IModelConfirmAndRedeem
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Token { get; set; }

        [UIHint(PasswordUiHint)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordDisplayName, Prompt = PasswordDisplayPrompt)]
        public string Password { get; set; }
        public const string PasswordUiHint = "StrengthMeteredPassword";
        public const string PasswordDisplayName = "Password";
        public const string PasswordDisplayPrompt = "Enter your new password";

        [DataType(DataType.Password)]
        [Display(Name = PasswordConfirmationDisplayName, Prompt = PasswordConfirmationDisplayPrompt)]
        [Remote("ValidatePasswordConfirmation", "ResetPassword", "Identity", HttpMethod = "POST", AdditionalFields = "Password")]
        public string PasswordConfirmation { get; set; }
        public const string PasswordConfirmationDisplayName = "Confirmation";
        public const string PasswordConfirmationDisplayPrompt = "Enter the same password again to confirm";
        public const string PasswordConfirmationPropertyName = "PasswordConfirmation";
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordForm>
    {
        public const string FailedBecausePasswordWasEmpty = "Password is required.";
        public const string FailedBecausePasswordWasTooShort = "Your password must be at least {0} characters long.";
        public const string FailedBecausePasswordConfirmationWasEmpty = "Password confirmation is required.";
        public const string FailedBecausePasswordConfirmationDidNotEqualPassword = "The password and confirmation password do not match.";

        public ResetPasswordValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)
                // cannot be empty guid
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)
                // matches email confirmation entity
                .Must(p => ValidateEmailConfirmation.TokenMatchesEntity(p, queryProcessor))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)
            ;

            RuleFor(p => p.Password)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecausePasswordWasEmpty)
                // at least 6 characters long
                .Length(memberSigner.MinimumPasswordLength, int.MaxValue)
                    .WithMessage(FailedBecausePasswordWasTooShort,
                        p => memberSigner.MinimumPasswordLength)
            ;

            RuleFor(p => p.PasswordConfirmation)
                // can never be empty
                .NotEmpty()
                    .WithMessage(FailedBecausePasswordConfirmationWasEmpty)
            ;

            RuleFor(p => p.PasswordConfirmation)
                // equals password unless empty or password failed validation
                .Equal(p => p.Password)
                    .Unless(p =>
                        string.IsNullOrWhiteSpace(p.PasswordConfirmation) ||
                        string.IsNullOrWhiteSpace(p.Password) ||
                        p.Password.Length < memberSigner.MinimumPasswordLength)
                    .WithMessage(FailedBecausePasswordConfirmationDidNotEqualPassword)
            ;
        }
    }

    public static class ResetPasswordProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ResetPasswordProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailConfirmation, ResetPasswordForm>()
                    .ForMember(d => d.Password, o => o.Ignore())
                    .ForMember(d => d.PasswordConfirmation, o => o.Ignore())
                ;
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ResetPasswordForm, ResetPasswordCommand>()
                    .ForMember(d => d.Ticket, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}