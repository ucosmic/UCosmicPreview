namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class ConfirmDeniedModel
    {
        public ConfirmDeniedModel(ConfirmDeniedBecause reason, string intent)
        {
            Reason = reason;
            Intent = intent;
        }

        public ConfirmDeniedBecause Reason { get; private set; }
        public string Intent { get; private set; }
    }

    public enum ConfirmDeniedBecause
    {
        //UserIsSignedUp = 0,
        //MemberIsSignedUp = 1,
        ConfirmationIsExpired = 2,
        //ConfirmationIsRedeemed = 3
    }
}