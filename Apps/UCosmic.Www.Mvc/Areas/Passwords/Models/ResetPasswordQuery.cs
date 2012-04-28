using System;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Email;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    public class ResetPasswordQuery
    {
        public Guid Token { get; set; }
    }

    public class ResetPasswordQueryValidator : AbstractValidator<ResetPasswordQuery>
    {
        public ResetPasswordQueryValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)
                // cannot be empty guid
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty, p =>
                        p.Token)
                // token must match a confirmation
                .Must(p => ValidateEmailConfirmation.TokenMatchesEntity(p, queryProcessor))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)
            ;
        }
    }

    public static class ResetPasswordQueryProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ResetPasswordQueryProfiler));
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