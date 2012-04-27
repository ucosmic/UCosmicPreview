using System;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    public class ResetPasswordQuery
    {
        public Guid Token { get; set; }
        public string Intent { get; set; }
        public const string IntentPropertyName = "Intent";
    }

    public class ResetQueryValidator : AbstractValidator<ResetPasswordQuery>
    {
        private readonly IProcessQueries _queryProcessor;

        public ResetQueryValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)

                // token must match a confirmation
                .Must(ValidateEmailConfirmationTokenMatchesEntity)
            ;

            RuleFor(p => p.Intent)

                // intent must match confirmation
                .Equal(EmailConfirmationIntent.PasswordReset)
                .Unless(p => _confirmation == null)
            ;
        }

        private EmailConfirmation _confirmation;

        private bool ValidateEmailConfirmationTokenMatchesEntity(ResetPasswordQuery model, Guid token)
        {
            var isValid = ValidateEmailConfirmation.TokenMatchesEntity(token, _queryProcessor, out _confirmation);
            if (_confirmation == null) return isValid;
            model.Intent = _confirmation.Intent;
            return isValid;
        }
    }

    public static class ResetQueryProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ResetQueryProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class QueryToFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ResetPasswordQuery, ResetPasswordForm>()
                    .ForMember(d => d.Password, o => o.Ignore())
                    .ForMember(d => d.PasswordConfirmation, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}