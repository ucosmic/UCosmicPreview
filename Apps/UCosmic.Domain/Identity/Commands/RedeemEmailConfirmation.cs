using System;
using FluentValidation;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class RedeemEmailConfirmationCommand
    {
        public Guid Token { get; set; }
        public string SecretCode { get; set; }
        public string Intent { get; set; }
        public string Ticket { get; internal set; }
    }

    public class RedeemEmailConfirmationHandler : IHandleCommands<RedeemEmailConfirmationCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public RedeemEmailConfirmationHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(RedeemEmailConfirmationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the confirmation
            var confirmation = _queryProcessor.Execute(
                new GetEmailConfirmationQuery(command.Token)
            );

            // redeem
            if (!confirmation.RedeemedOnUtc.HasValue)
            {
                confirmation.EmailAddress.IsConfirmed = true;
                confirmation.RedeemedOnUtc = DateTime.UtcNow;
                confirmation.Ticket = _queryProcessor.Execute(
                    new GenerateRandomSecretQuery(256));
                _entities.Update(confirmation);
            }

            command.Ticket = confirmation.Ticket;
        }
    }

    public class RedeemEmailConfirmationValidator : AbstractValidator<RedeemEmailConfirmationCommand>
    {
        public RedeemEmailConfirmationValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailConfirmation confirmation = null;

            RuleFor(p => p.Token)
                // token cannot be an empty guid
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)
                // token must match a confirmation
                .Must(p => ValidateEmailConfirmation.TokenMatchesEntity(p, queryProcessor, out confirmation))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)
            ;

            RuleFor(p => p.SecretCode)
                // secret cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseSecretCodeWasEmpty)
            ;

            RuleFor(p => p.Intent)
                // intent cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseIntentWasEmpty)
            ;

            // when confirmation is not null,
            When(p => confirmation != null, () =>
            {
                RuleFor(p => p.Token)
                    // it cannot be expired
                    .Must(p => !confirmation.IsExpired)
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsExpired,
                            p => confirmation.Token, p => confirmation.ExpiresOnUtc)
                    // it cannot be retired
                    .Must(p => !confirmation.IsRetired)
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsRetired,
                            p => confirmation.Token, p => confirmation.RetiredOnUtc)
                ;

                RuleFor(p => p.SecretCode)
                    // secret must match entity
                    .Must(p => ValidateEmailConfirmation.SecretCodeIsCorrect(confirmation, p))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseSecretCodeWasIncorrect,
                            p => p.SecretCode, p => confirmation.Token)
                ;

                RuleFor(p => p.Intent)
                    // intent must match entity
                    .Must(p => ValidateEmailConfirmation.IntentIsCorrect(confirmation, p))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                            p => p.Intent, p => confirmation.Token)
                ;
            });
        }
    }
}
