using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
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
}