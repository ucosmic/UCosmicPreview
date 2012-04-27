using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
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

        [UIHint("StrengthMeteredPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your new password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation", Prompt = "Enter the same password again to confirm")]
        [Remote("ValidatePasswordConfirmation", "ResetPassword", "Passwords", HttpMethod = "POST", AdditionalFields = "Password")]
        public string PasswordConfirmation { get; set; }
        public const string PasswordConfirmationPropertyName = "PasswordConfirmation";

        internal string Intent { get; set; }
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public ResetPasswordValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)
                .NotEmpty()
                .Must(ValidateEmailConfirmationTokenMatchesEntity)
            ;

            RuleFor(p => p.Intent)
                .Equal(EmailConfirmationIntent.PasswordReset)
            ;

            RuleFor(p => p.Password)
                .NotEmpty()
                    .WithMessage(FailedBecausePasswordWasEmpty)

                .Length(ValidatePassword.MinimumLength, int.MaxValue)
                    .WithMessage(FailedBecausePasswordWasTooShort,
                        p => ValidatePassword.MinimumLength)
            ;

            RuleFor(p => p.PasswordConfirmation)
                .NotEmpty().WithMessage(FailedBecausePasswordConfirmationWasEmpty)
            ;

            RuleFor(p => p.PasswordConfirmation)
                .Equal(p => p.Password).WithMessage(FailedBecausePasswordConfirmationDidNotEqualPassword)
                .Unless(p => string.IsNullOrWhiteSpace(p.PasswordConfirmation)
                    || string.IsNullOrWhiteSpace(p.PasswordConfirmation)
                    || p.Password.Length < ValidatePassword.MinimumLength)
            ;
        }

        private EmailConfirmation _confirmation;

        private bool ValidateEmailConfirmationTokenMatchesEntity(ResetPasswordForm model, Guid token)
        {
            var isValid = ValidateEmailConfirmation.TokenMatchesEntity(model.Token, _queryProcessor, out _confirmation);
            if (_confirmation != null) model.Intent = _confirmation.Intent;
            return isValid;
        }

        public const string FailedBecausePasswordWasEmpty = "Password is required.";

        public const string FailedBecausePasswordWasTooShort = "Your password must be at least {0} characters long.";

        public const string FailedBecausePasswordConfirmationWasEmpty = "Password confirmation is required.";

        public const string FailedBecausePasswordConfirmationDidNotEqualPassword = "The password and confirmation password do not match.";
    }

    public static class ResetPasswordProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ResetPasswordProfiler));
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