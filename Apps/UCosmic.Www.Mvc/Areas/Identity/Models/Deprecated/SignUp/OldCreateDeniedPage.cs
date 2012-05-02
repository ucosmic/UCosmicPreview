
namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    public class OldCreateDeniedPage
    {
        public enum DeniedBecause
        {
            UserIsSignedUp = 0,
            MemberIsSignedUp = 1,
            ConfirmationIsExpired = 2,
            ConfirmationDoesNotExist = 3,
        }

        public OldCreateDeniedPage(DeniedBecause reason)
        {
            Reason = reason;
        }

        public DeniedBecause Reason { get; private set; }
    }
}