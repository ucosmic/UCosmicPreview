using System;
using FluentValidation;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class RedeemEmailConfirmationValidator : AbstractValidator<RedeemEmailConfirmationCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public RedeemEmailConfirmationValidator(IProcessQueries queryProcessor)
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

                // confirmation cannot be redeemed
                .Must(ValidateEmailConfirmationIsNotRedeemed).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseIRedeemed,
                        p => p.Token, p => _confirmation.RedeemedOnUtc)
            ;

            RuleFor(p => p.SecretCode)

                // secret cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasEmpty)

                // secret must match entity
                .Must(ValidateEmailConfirmationSecretCodeIsCorrect).WithMessage(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasIncorrect,
                        p => p.SecretCode, p => p.Token)
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

        private bool ValidateEmailConfirmationIsNotRedeemed(Guid token)
        {
            return ValidateEmailConfirmation.IsNotRedeemed(_confirmation);
        }

        private bool ValidateEmailConfirmationSecretCodeIsCorrect(string secretCode)
        {
            return ValidateEmailConfirmation.SecretCodeIsCorrect(_confirmation, secretCode);
        }

        private bool ValidateEmailConfirmationIntentIsCorrect(string intent)
        {
            return ValidateEmailConfirmation.IntentIsCorrect(_confirmation, intent);
        }
    }
}
