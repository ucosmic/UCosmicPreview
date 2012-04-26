
namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    public class CreateDeniedPage
    {
        public enum DeniedBecause
        {
            UserIsSignedUp = 0,
            MemberIsSignedUp = 1,
            ConfirmationIsExpired = 2,
            ConfirmationDoesNotExist = 3,
        }

        public CreateDeniedPage(DeniedBecause reason)
        {
            Reason = reason;
        }

        public DeniedBecause Reason { get; private set; }
    }
}