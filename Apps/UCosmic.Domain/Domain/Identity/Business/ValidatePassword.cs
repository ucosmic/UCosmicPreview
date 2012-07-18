namespace UCosmic.Domain.Identity
{
    public static class ValidatePassword
    {
        #region Password cannot be empty

        public const string FailedBecausePasswordWasEmpty =
            "Password is required.";

        #endregion
        #region Password must be between 6 and 100 characters long

        public static string FailedBecausePasswordWasTooShort(int minimumLength)
        {
            return string.Format("Password must be at least {0} characters long.", minimumLength);
        }

        #endregion
        #region Password confirmation cannot be empty

        public const string FailedBecausePasswordConfirmationWasEmpty =
            "Password confirmation is required.";

        #endregion
        #region Password confirmation must match

        public const string FailedBecausePasswordConfirmationDidNotEqualPassword =
            "Password and confirmation do not match.";

        #endregion
    }
}
