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
    public class ForgotPasswordForm : IReturnUrl
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = EmailAddressDisplayName, Prompt = EmailAddressDisplayPrompt)]
        [Remote("ValidateEmailAddress", "ForgotPassword", "Passwords", HttpMethod = "POST")]
        public string EmailAddress { get; set; }
        public const string EmailAddressDisplayName = "Email address";
        public const string EmailAddressDisplayPrompt = "Enter the email address you used when you signed up";
        public const string EmailAddressPropertyName = "EmailAddress";

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }

    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordForm>
    {
        public const string FailedBecauseEmailAddressWasEmpty = "Email address is required.";
        public const string FailedBecauseEmailAddressWasNotValidEmailAddress = "This is not a valid email address.";
        public const string FailedBecauseUserNameMatchedNoLocalMember = "A user account for the email address '{0}' could not be found.";
        public const string FailedBecauseEduPersonTargetedIdWasNotEmpty = "Your password cannot be reset because you have a Single Sign On account with your employer.";

        public ForgotPasswordValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            Person person = null;
            var eagerLoad = new Expression<Func<Person, object>>[]
            {
                p => p.Emails,
                p => p.User
            };

            RuleFor(p => p.EmailAddress)

                // email address cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecauseEmailAddressWasEmpty)

                // must be valid against email address regular expression
                .EmailAddress()
                    .WithMessage(FailedBecauseEmailAddressWasNotValidEmailAddress)

                // the email address must match a person
                .Must(p => ValidateEmailAddress.ValueMatchesPerson(p, queryProcessor, eagerLoad, out person))
                    .WithMessage(FailedBecauseUserNameMatchedNoLocalMember,
                        p => p.EmailAddress)

                // the matched person must have a user
                .Must(p => ValidatePerson.UserIsNotNull(person))
                    .WithMessage(FailedBecauseUserNameMatchedNoLocalMember,
                        p => p.EmailAddress)

                // the user must not have a SAML account
                .Must(p => ValidateUser.EduPersonTargetedIdIsEmpty(person.User))
                    .WithMessage(FailedBecauseEduPersonTargetedIdWasNotEmpty)

                // the email address' person's user's name must match a local member account
                .Must(p => ValidateUser.NameMatchesLocalMember(person.User.Name, memberSigner))
                    .WithMessage(FailedBecauseUserNameMatchedNoLocalMember,
                        p => p.EmailAddress)

                // the email address must be confirmed
                .Must(p => ValidateEmailAddress.IsConfirmed(person.Emails.ByValue(p)))
                    .WithMessage(ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        p => p.EmailAddress)
            ;
        }
    }

    public static class ForgotPasswordProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ForgotPasswordProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ForgotPasswordForm, SendConfirmEmailMessageCommand>()
                    .ForMember(d => d.Intent, o => o.UseValue(EmailConfirmationIntent.PasswordReset))
                    .ForMember(d => d.ConfirmationToken, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}