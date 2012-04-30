using System;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ValidateRedeemTicketAttribute : ValidateConfirmEmailAttribute
    {
        public string Intent { get; private set; }

        private ValidateRedeemTicketAttribute(string paramName)
            : base(paramName)
        {
        }

        public ValidateRedeemTicketAttribute(string paramName, string intent)
            : this(paramName)
        {
            if (intent == null) throw new ArgumentNullException("intent");
            Intent = intent;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // when the base class has set a result, is retired or expired
            if (filterContext.Result != null) return;
            var tempData = filterContext.Controller.TempData;

            // when ticket or intent is invalid, crash
            if (EmailConfirmation.Ticket != tempData.EmailConfirmationTicket() ||
                EmailConfirmation.Intent != Intent)
            {
                HandleDenial(filterContext, ConfirmDeniedBecause.OtherCrash);
            }
        }

        protected override void ValidateRedemption(ActionExecutingContext filterContext)
        {
            if (EmailConfirmation.IsRedeemed) return;

            HandleDenial(filterContext, ConfirmDeniedBecause.OtherCrash);
        }

    }
}