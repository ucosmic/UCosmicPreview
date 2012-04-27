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
        private readonly IProcessQueries _queryProcessor;

        public ResetPasswordValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Token)

                // token cannot be an empty guid
                .NotEmpty().WithMessage(
                    ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)

                // token must match a confirmation
                .Must(ValidateEmailConfirmationTokenMatchesEntity).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)

                // confirmation cannot be expired
                .Must(ValidateEmailConfirmationIsNotExpired).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseIsExpired,
                        p => p.Token, p => _confirmation.ExpiresOnUtc)

                // confirmation must be redeemed
                .Must(ValidateEmailConfirmationIsRedeemed).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseIsNotRedeemed,
                        p => p.Token)
            ;

            RuleFor(p => p.Ticket)

                // ticket cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailConfirmation.FailedBecauseTicketWasEmpty)

                // ticket must match entity
                .Must(ValidateEmailConfirmationTicketIsCorrect).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseTicketWasIncorrect,
                        p => p.Ticket, p => p.Token)
            ;

            RuleFor(p => p.Intent)

                // intent cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailConfirmation.FailedBecauseIntentWasEmpty)

                // intent must match entity
                .Must(ValidateEmailConfirmationIntentIsCorrect).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                        p => p.Intent, p => p.Token)
            ;

            RuleFor(p => p.Password)

                // cannot be empty
                .NotEmpty().WithMessage(
                    ValidatePassword.FailedBecausePasswordWasEmpty)

                // length must be between 6 and 100 characters
                .Length(ValidatePassword.MinimumLength, int.MaxValue).WithMessage(
                    ValidatePassword.FailedBecausePasswordWasTooShort)
            ;

            RuleFor(p => p.PasswordConfirmation)

                // cannot be empty
                .NotEmpty()
                    .WithMessage(ValidatePassword.FailedBecausePasswordConfirmationWasEmpty)
            ;
            RuleFor(p => p.PasswordConfirmation)

                // must match password unless password is invalid or confirmation is empty
                .Equal(p => p.Password)
                    .Unless(
                        p => string.IsNullOrWhiteSpace(p.Password) ||
                        string.IsNullOrWhiteSpace(p.PasswordConfirmation) ||
                        p.Password.Length < ValidatePassword.MinimumLength)
                    .WithMessage(ValidatePassword.FailedBecausePasswordConfirmationDidNotEqualPassword)
            ;
        }

        private EmailConfirmation _confirmation;

        private bool ValidateEmailConfirmationTokenMatchesEntity(Guid token)
        {
            return ValidateEmailConfirmation.TokenMatchesEntity(token, _queryProcessor, out _confirmation);
        }

        private bool ValidateEmailConfirmationIsNotExpired(Guid token)
        {
            return ValidateEmailConfirmation.IsNotExpired(_confirmation);
        }

        private bool ValidateEmailConfirmationIsRedeemed(Guid token)
        {
            return ValidateEmailConfirmation.IsRedeemed(_confirmation);
        }

        private bool ValidateEmailConfirmationTicketIsCorrect(string ticket)
        {
            return ValidateEmailConfirmation.TicketIsCorrect(_confirmation, ticket);
        }

        private bool ValidateEmailConfirmationIntentIsCorrect(string intent)
        {
            return ValidateEmailConfirmation.IntentIsCorrect(_confirmation, intent);
        }
    }
}
