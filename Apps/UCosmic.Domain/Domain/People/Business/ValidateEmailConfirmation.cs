using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace UCosmic.Domain.People
{
    public static class ValidateEmailConfirmation
    {
        #region Token cannot be empty

        public const string FailedBecauseTokenWasEmpty =
            "Email confirmation token '{0}' is not valid.";

        #endregion
        #region Token matches entity

        public const string FailedBecauseTokenMatchedNoEntity =
            "Email confirmation '{0}' could not be found.";

        public static bool TokenMatchesEntity(Guid token, IQueryEntities entities,
            IEnumerable<Expression<Func<EmailConfirmation, object>>> eagerLoad, out EmailConfirmation entity)
        {
            if (token == Guid.Empty)
            {
                entity = null;
                return false;
            }

            entity = entities.Query<EmailConfirmation>().EagerLoad(entities, eagerLoad).ByToken(token);

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool TokenMatchesEntity(Guid token, IQueryEntities entities,
            IEnumerable<Expression<Func<EmailConfirmation, object>>> eagerLoad = null)
        {
            EmailConfirmation entity;
            return TokenMatchesEntity(token, entities, eagerLoad, out entity);
        }

        public static bool TokenMatchesEntity(Guid token, IQueryEntities entities, out EmailConfirmation entity)
        {
            return TokenMatchesEntity(token, entities, null, out entity);
        }

        #endregion
        #region Is not expired

        public const string FailedBecauseIsExpired =
            "The email confirmation '{0}' expired on {1}.";

        #endregion
        #region Is not redeemed

        public const string FailedBecauseIsRedeemed =
            "The email confirmation '{0}' was redeemed on {1}.";

        #endregion
        #region Is redeemed

        public const string FailedBecauseIsNotRedeemed =
            "The email confirmation '{0}' has not been redeemed.";

        #endregion
        #region Is not retired

        public const string FailedBecauseIsRetired =
            "The email confirmation '{0}' was retired on {1}.";

        #endregion
        #region Secret cannot be empty

        public const string FailedBecauseSecretCodeWasEmpty =
            "A secret code is required to redeem an email confirmation.";

        #endregion
        #region Secret must match

        public const string FailedBecauseSecretCodeWasIncorrect =
            "The secret code '{0}' is not valid for email confirmation '{1}'.";

        public static bool SecretCodeIsCorrect(EmailConfirmation confirmation, string secretCode)
        {
            // return true (valid) if confirmation secret matches
            return confirmation != null && confirmation.SecretCode == secretCode;
        }

        #endregion
        #region Ticket cannot be empty

        public const string FailedBecauseTicketWasEmpty =
            "A ticket is required to redeem a password reset.";

        #endregion
        #region Ticket must match

        public const string FailedBecauseTicketWasIncorrect =
            "The ticket '{0}' is not valid for email confirmation '{1}'.";

        public static bool TicketIsCorrect(EmailConfirmation confirmation, string ticket)
        {
            // return true (valid) if confirmation ticket matches
            return confirmation != null && confirmation.Ticket == ticket;
        }

        #endregion
        #region Intent cannot be empty

        public const string FailedBecauseIntentWasEmpty =
            "An intent is required to redeem an email confirmation.";

        #endregion
        #region Intent must match

        public const string FailedBecauseIntentWasIncorrect =
            "The intent '{0}' is not valid for email confirmation '{1}'.";

        public static bool IntentIsCorrect(EmailConfirmation confirmation, EmailConfirmationIntent intent)
        {
            // return true (valid) if confirmation intent matches
            return confirmation != null && confirmation.Intent == intent;
        }

        #endregion
    }
}
