namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    public class ConfirmDeniedPage
    {
        public enum DeniedBecause
        {
            UserIsSignedUp = 0,
            MemberIsSignedUp = 1,
            ConfirmationIsExpired = 2,
            ConfirmationIsRedeemed = 3
        }

        public ConfirmDeniedPage(DeniedBecause reason)
        {
            Reason = reason;
        }

        public DeniedBecause Reason { get; private set; }
    }
}