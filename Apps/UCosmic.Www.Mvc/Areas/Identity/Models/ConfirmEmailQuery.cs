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
        public string Intent { get; set; }
        public bool IsExpired { get; set; }
        public bool IsRedeemed { get; set; }
    }

    public class ConfirmQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {
        private readonly IProcessQueries _queryProcessor;

        public ConfirmQueryValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)

                // token must match a confirmation
                .Must(ValidateEmailConfirmationTokenMatchesEntity)
            ;

            RuleFor(p => p.IsExpired)

                // cannot be expired
                .Equal(false)
                .Unless(p => _confirmation == null)
            ;

            RuleFor(p => p.IsRedeemed)

                // cannot be redeemed
                .Equal(false)
                .Unless(p => _confirmation == null)
            ;
        }

        private EmailConfirmation _confirmation;

        private bool ValidateEmailConfirmationTokenMatchesEntity(ConfirmEmailQuery model, Guid token)
        {
            var isValid = ValidateEmailConfirmation.TokenMatchesEntity(token, _queryProcessor, out _confirmation);
            if (_confirmation == null) return isValid;
            model.Intent = _confirmation.Intent;
            model.IsExpired = _confirmation.IsExpired;
            model.IsRedeemed = _confirmation.IsRedeemed;
            return isValid;
        }
    }

    public static class ConfirmQueryProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ConfirmQueryProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class QueryToFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ConfirmEmailQuery, ConfirmEmailForm>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}