using System;
using UCosmic.Domain.People;
using System.Linq.Expressions;
using System.Collections.Generic;
namespace UCosmic.Domain.Email
{
    public static class ValidateEmailConfirmation
    {
        #region Token cannot be empty

        public const string FailedBecauseTokenWasEmpty =
            "The email confirmation token '{0}' is not valid.";

        #endregion
        #region Token matches entity

        public const string FailedBecauseTokenMatchedNoEntity =
            "Email confirmation '{0}' could not be found.";

        public static bool TokenMatchesEntity(Guid token, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Person, object>>> eagerLoad, out EmailConfirmation entity)
        {
            if (token == Guid.Empty)
            {
                entity = null;
                return false;
            }

            entity = queryProcessor.Execute(
                new GetEmailConfirmationQuery(token)
                {
                    EagerLoad = eagerLoad,
                }
            );

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool TokenMatchesEntity(Guid token, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Person, object>>> eagerLoad = null)
        {
            EmailConfirmation entity;
            return TokenMatchesEntity(token, queryProcessor, eagerLoad, out entity);
        }

        public static bool TokenMatchesEntity(Guid token, IProcessQueries queryProcessor, out EmailConfirmation entity)
        {
            return TokenMatchesEntity(token, queryProcessor, null, out entity);
        }

        #endregion
        #region Is not expired

        public const string FailedBecauseIsExpired =
            "The email confirmation '{0}' expired on {1}.";

        public static bool IsNotExpired(EmailConfirmation confirmation)
        {
            // return true (valid) if confirmation is not expired
            return confirmation != null && !confirmation.IsExpired;
        }

        #endregion
        #region Is not redeemed

        public const string FailedBecauseIRedeemed =
            "The email confirmation '{0}' was redeemed on {1}.";

        public static bool IsNotRedeemed(EmailConfirmation confirmation)
        {
            // return true (valid) if confirmation is not redeemed
            return confirmation != null && !confirmation.IsRedeemed;
        }

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
        #region Intent cannot be empty

        public const string FailedBecauseIntentWasEmpty =
            "An intent is required to redeem an email confirmation.";

        #endregion
        #region Intent must match

        public const string FailedBecauseIntentWasIncorrect =
            "The secret code '{0}' is not valid for email confirmation '{1}'.";

        public static bool IntentIsCorrect(EmailConfirmation confirmation, string intent)
        {
            // return true (valid) if confirmation intent matches
            return confirmation != null && confirmation.Intent == intent;
        }

        #endregion
    }
}
