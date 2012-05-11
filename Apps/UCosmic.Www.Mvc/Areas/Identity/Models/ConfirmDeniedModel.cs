using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class ConfirmDeniedModel
    {
        public ConfirmDeniedModel(ConfirmDeniedBecause reason, EmailConfirmationIntent intent)
        {
            Reason = reason;
            Intent = intent;
        }

        public ConfirmDeniedBecause Reason { get; private set; }
        public EmailConfirmationIntent Intent { get; private set; }
    }

    public enum ConfirmDeniedBecause
    {
        IsExpired = 2,
        IsRetired = 3,
        OtherCrash = 4,
    }
}