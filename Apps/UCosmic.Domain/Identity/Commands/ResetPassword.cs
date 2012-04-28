using System;
using FluentValidation;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class ResetPasswordCommand
    {
        public Guid Token { get; set; }
        public string Intent { get; set; }
        public string Ticket { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class ResetPasswordHandler : IHandleCommands<ResetPasswordCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly ISignMembers _memberSigner;

        public ResetPasswordHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , ISignMembers memberSigner
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _memberSigner = memberSigner;
        }

        public void Handle(ResetPasswordCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the confirmation
            var confirmation = _queryProcessor.Execute(
                new GetEmailConfirmationQuery(command.Token)
            );

            _memberSigner.ResetPassword(confirmation.EmailAddress.Person.User.Name, command.Password);
            confirmation.RetiredOnUtc = DateTime.UtcNow;
            confirmation.Ticket = null;
            _entities.Update(confirmation);
        }
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            EmailConfirmation confirmation = null;

            RuleFor(p => p.Token)
                // token cannot be an empty guid
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)
                // token must match a confirmation
                .Must(token => ValidateEmailConfirmation.TokenMatchesEntity(token, queryProcessor, out confirmation))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        o => o.Token)
            ;

            RuleFor(p => p.Ticket)
                // ticket cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTicketWasEmpty)
            ;

            RuleFor(p => p.Intent)
                // intent cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseIntentWasEmpty)
            ;

            RuleFor(p => p.Password)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(ValidatePassword.FailedBecausePasswordWasEmpty)
                // length must be between 6 and 100 characters
                .Length(ValidatePassword.MinimumLength, int.MaxValue)
                    .WithMessage(ValidatePassword.FailedBecausePasswordWasTooShort)
            ;

            RuleFor(p => p.PasswordConfirmation)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(ValidatePassword.FailedBecausePasswordConfirmationWasEmpty)
            ;

            RuleFor(p => p.PasswordConfirmation)
                // must match password unless password is invalid or password confirmation is empty
                .Equal(p => p.Password)
                    .Unless(p => 
                        string.IsNullOrWhiteSpace(p.PasswordConfirmation) ||
                        string.IsNullOrWhiteSpace(p.Password) ||
                        p.Password.Length < ValidatePassword.MinimumLength)
                    .WithMessage(ValidatePassword.FailedBecausePasswordConfirmationDidNotEqualPassword)
            ;

            // when confirmation is not null,
            When(p => confirmation != null, () =>
            {
                RuleFor(p => p.Token)
                    // it cannot be expired
                    .Must(p => ValidateEmailConfirmation.IsNotExpired(confirmation))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsExpired,
                            o => o.Token, p => confirmation.ExpiresOnUtc)
                    // it cannot be retired
                    .Must(t => ValidateEmailConfirmation.IsNotRetired(confirmation))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsRetired,
                            o => o.Token, p => confirmation.RetiredOnUtc)
                    // it cannot be redeemed
                    .Must(t => ValidateEmailConfirmation.IsRedeemed(confirmation))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsNotRedeemed,
                            o => o.Token)
                    // email address must be confirmed
                    .Must(t => ValidateEmailAddress.IsConfirmed(confirmation.EmailAddress))
                        .WithMessage(ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                            o => confirmation.EmailAddress.Value)
                    // it must be attached to a user
                    .Must(t => ValidatePerson.UserIsNotNull(confirmation.EmailAddress.Person))
                        .WithMessage(ValidatePerson.FailedBecauseUserWasNull,
                            o => confirmation.EmailAddress.Person.DisplayName)
                    // user cannot have a saml account
                    .Must(t => ValidateUser.EduPersonTargetedIdIsEmpty(confirmation.EmailAddress.Person.User))
                        .WithMessage(ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                            o => confirmation.EmailAddress.Person.User.Name)
                    // user name must match local member account
                    .Must(t => ValidateUser.NameMatchesLocalMember(confirmation.EmailAddress.Person.User.Name, memberSigner))
                        .WithMessage(ValidateUser.FailedBecauseNameMatchedNoLocalMember,
                            o => confirmation.EmailAddress.Person.User.Name)
                ;

                RuleFor(p => p.Ticket)
                    // its ticket must match the command ticket
                    .Must(p => ValidateEmailConfirmation.TicketIsCorrect(confirmation, p))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseTicketWasIncorrect,
                            p => p.Ticket, p => p.Token)
                ;

                RuleFor(p => p.Intent)
                    // its intent must match the command intent
                    .Must(p => ValidateEmailConfirmation.IntentIsCorrect(confirmation, p))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                            p => p.Intent, p => p.Token)
                ;
            });
        }
    }
}
