using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class SendEmailConfirmationMessageValidator : AbstractValidator<SendEmailConfirmationMessageCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ISignMembers _memberSigner;

        public SendEmailConfirmationMessageValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            _queryProcessor = queryProcessor;
            _memberSigner = memberSigner;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EmailAddress)

                // email address cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailAddress.FailedBecauseValueWasEmpty)

                // must be valid against email address regular expression
                .EmailAddress().WithMessage(
                    ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress)

                // the email address must match a person
                .Must(ValidateEmailAddressValueMatchesPerson).WithMessage(
                    ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                        p => p.EmailAddress)

                // the matched person must have a user
                .Must(ValidatePersonUserIsNotNull).WithMessage(
                    ValidatePerson.FailedBecauseUserWasNull,
                        p => p.EmailAddress)

                // the user must not have a SAML account
                .Must(ValidateUserEduPersonTargetedIdIsEmpty).WithMessage(
                    ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        p => p.EmailAddress)

                // the email address' person's user's name must match a local member account
                .Must(ValidateUserNameMatchesLocalMember).WithMessage(
                    ValidateUser.FailedBecauseNameMatchedNoLocalMember,
                        p => p.EmailAddress)

                // the email address must be confirmed
                .Must(ValidateEmailAddressIsConfirmed).WithMessage(
                    ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        p => p.EmailAddress)
            ;
        }

        private Person _person;

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

        private bool ValidatePersonUserIsNotNull(string emailAddress)
        {
            return ValidatePerson.UserIsNotNull(_person);
        }

        private bool ValidateUserEduPersonTargetedIdIsEmpty(string emailAddress)
        {
            return ValidateUser.EduPersonTargetedIdIsEmpty(_person.User);
        }

        private bool ValidateUserNameMatchesLocalMember(string emailAddress)
        {
            return ValidateUser.NameMatchesLocalMember(_person.User.Name, _memberSigner);
        }

        private bool ValidateEmailAddressIsConfirmed(string emailAddress)
        {
            return ValidateEmailAddress.IsConfirmed(_person.GetEmail(emailAddress));
        }
    }
}
