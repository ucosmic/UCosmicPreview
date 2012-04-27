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
        public const string EmailAddressPropertyName = "EmailAddress";
        public const string EmailAddressDisplayName = "Email Address";
        public const string EmailAddressDisplayPrompt = "Enter the email address you used when you signed up";

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }

    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordForm>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ISignMembers _memberSigner;

        public ForgotPasswordValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            _queryProcessor = queryProcessor;
            _memberSigner = memberSigner;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EmailAddress)

                // email address cannot be empty
                .NotEmpty().WithMessage(
                    FailedBecauseEmailAddressWasEmpty)

                // must be valid against email address regular expression
                .EmailAddress().WithMessage(
                    FailedBecauseEmailAddressWasNotValidEmailAddress)

                // the email address must match a person
                .Must(ValidateEmailAddressValueMatchesPerson).WithMessage(
                    FailedBecauseEmailAddressValueMatchedNoPerson,
                        p => p.EmailAddress)

                // the matched person must have a user
                .Must(ValidatePersonUserIsNotNull).WithMessage(
                    FailedBecauseEmailAddressValueMatchedNoUser,
                        p => p.EmailAddress)

                // the user must not have a SAML account
                .Must(ValidateUserEduPersonTargetedIdIsEmpty).WithMessage(
                    FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        p => p.EmailAddress)

                // the email address' person's user's name must match a local member account
                .Must(ValidateUserNameMatchesLocalMember).WithMessage(
                    FailedBecauseUserNameMatchedNoLocalMember,
                        p => p.EmailAddress)

                // the email address must be confirmed
                .Must(ValidateEmailAddressIsConfirmed).WithMessage(
                    ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        p => p.EmailAddress)
            ;
        }

        private Person _person;

        internal const string FailedBecauseEmailAddressWasEmpty
            = "Email Address is required.";//

        internal const string FailedBecauseEmailAddressWasNotValidEmailAddress
            = "This is not a valid email address.";//

        internal const string FailedBecauseEmailAddressValueMatchedNoPerson
            = "A user account for the email address '{0}' could not be found.";

        private bool ValidateEmailAddressValueMatchesPerson(string emailAddress)
        {
            return ValidateEmailAddress.ValueMatchesPerson(emailAddress, _queryProcessor,
                new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails,
                        p => p.User
                    },
                out _person
            );
        }

        internal const string FailedBecauseEmailAddressValueMatchedNoUser
            = FailedBecauseEmailAddressValueMatchedNoPerson;

        private bool ValidatePersonUserIsNotNull(string emailAddress)
        {
            return ValidatePerson.UserIsNotNull(_person);
        }

        internal const string FailedBecauseEduPersonTargetedIdWasNotEmpty
            = "Your password cannot be reset because you have a Single Sign On account with your employer.";

        private bool ValidateUserEduPersonTargetedIdIsEmpty(string emailAddress)
        {
            return ValidateUser.EduPersonTargetedIdIsEmpty(_person.User);
        }

        internal const string FailedBecauseUserNameMatchedNoLocalMember
            = FailedBecauseEmailAddressValueMatchedNoPerson;

        private bool ValidateUserNameMatchesLocalMember(string emailAddress)
        {
            return ValidateUser.NameMatchesLocalMember(_person.User.Name, _memberSigner);
        }

        private bool ValidateEmailAddressIsConfirmed(string emailAddress)
        {
            return ValidateEmailAddress.IsConfirmed(_person.Emails.ByValue(emailAddress));
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