using System;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class ConfirmEmailQuery
    {
        public Guid Token { get; set; }
        public string SecretCode { get; set; }
        public bool IsExpired { get; set; }
        public bool IsRedeemed { get; set; }
    }

    public class ConfirmEmailQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailQueryValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailConfirmation confirmation = null;

            RuleFor(p => p.Token)
                // cannot be empty guid
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)
                // must match a confirmation
                .Must(p => ValidateEmailConfirmation.TokenMatchesEntity(p, queryProcessor, out confirmation))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)
            ;

            RuleFor(p => p.IsExpired)
                // cannot be expired
                .Must(p => !confirmation.IsExpired)
                    .When(p => confirmation != null)
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseIsExpired,
                        p => confirmation.Token, p => confirmation.ExpiresOnUtc)
            ;

            RuleFor(p => p.IsRedeemed)
                // cannot be redeemed
                .Must(p => !confirmation.IsRedeemed)
                    .When(p => confirmation != null)
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseIsRedeemed,
                        p => confirmation.Token, p => confirmation.RedeemedOnUtc)
            ;
        }
    }

    public static class ConfirmEmailQueryProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ConfirmEmailQueryProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class QueryToFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ConfirmEmailQuery, ConfirmEmailForm>()
                    .ForMember(d => d.IsUrlConfirmation, o => o
                        .MapFrom(s => !string.IsNullOrWhiteSpace(s.SecretCode)))
                    .ForMember(d => d.Intent, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}