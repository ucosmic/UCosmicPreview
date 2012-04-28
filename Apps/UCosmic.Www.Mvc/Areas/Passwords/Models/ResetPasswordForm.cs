using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    public class ResetPasswordForm
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Token { get; set; }
        public const string TokenPropertyName = "Token";

        [UIHint(PasswordUiHint)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordDisplayName, Prompt = PasswordDisplayPrompt)]
        public string Password { get; set; }
        public const string PasswordUiHint = "StrengthMeteredPassword";
        public const string PasswordDisplayName = "Password";
        public const string PasswordDisplayPrompt = "Enter your new password";

        [DataType(DataType.Password)]
        [Display(Name = PasswordConfirmationDisplayName, Prompt = PasswordConfirmationDisplayPrompt)]
        [Remote("ValidatePasswordConfirmation", "ResetPassword", "Passwords", HttpMethod = "POST", AdditionalFields = "Password")]
        public string PasswordConfirmation { get; set; }
        public const string PasswordConfirmationDisplayName = "Confirmation";
        public const string PasswordConfirmationDisplayPrompt = "Enter the same password again to confirm";
        public const string PasswordConfirmationPropertyName = "PasswordConfirmation";
    }

    public class ResetPasswordFormValidator : AbstractValidator<ResetPasswordForm>
    {
        public const string FailedBecausePasswordWasEmpty = "Password is required.";
        public const string FailedBecausePasswordWasTooShort = "Your password must be at least {0} characters long.";
        public const string FailedBecausePasswordConfirmationWasEmpty = "Password confirmation is required.";
        public const string FailedBecausePasswordConfirmationDidNotEqualPassword = "The password and confirmation password do not match.";

        public ResetPasswordFormValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)
                .Must(p => ValidateEmailConfirmation.TokenMatchesEntity(p, queryProcessor))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)
            ;

            RuleFor(p => p.Password)
                .NotEmpty()
                    .WithMessage(FailedBecausePasswordWasEmpty)

                .Length(ValidatePassword.MinimumLength, int.MaxValue)
                    .WithMessage(FailedBecausePasswordWasTooShort,
                        p => ValidatePassword.MinimumLength)
            ;

            RuleFor(p => p.PasswordConfirmation)
                .NotEmpty()
                    .WithMessage(FailedBecausePasswordConfirmationWasEmpty)
            ;

            RuleFor(p => p.PasswordConfirmation)
                .Equal(p => p.Password)
                    .Unless(p =>
                        string.IsNullOrWhiteSpace(p.PasswordConfirmation) ||
                        string.IsNullOrWhiteSpace(p.Password) ||
                        p.Password.Length < ValidatePassword.MinimumLength)
                    .WithMessage(FailedBecausePasswordConfirmationDidNotEqualPassword)
            ;
        }
    }

    public static class ResetPasswordFormProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ResetPasswordFormProfiler));
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
                    .ForMember(d => d.Intent, o => o.UseValue(EmailConfirmationIntent.PasswordReset))
                    .ForMember(d => d.Ticket, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}