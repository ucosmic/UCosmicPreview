using System;
using System.Web.Mvc;
using System.Web.Routing;
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

            // when intent does not match, crash
            if (EmailConfirmation.Intent != Intent)
            {
                HandleDenial(filterContext, ConfirmDeniedBecause.OtherCrash);
            }

            // when the confirmation is not redeemed or the ticket has been lost, redirect
            var ticket = filterContext.Controller.TempData.EmailConfirmationTicket();
            if (!EmailConfirmation.IsRedeemed || EmailConfirmation.Ticket != ticket)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        area = MVC.Identity.Name,
                        controller = MVC.Identity.ConfirmEmail.Name,
                        action = MVC.Identity.ConfirmEmail.ActionNames.Get,
                        token = EmailConfirmation.Token,
                    }));
            }

        }
    }
}