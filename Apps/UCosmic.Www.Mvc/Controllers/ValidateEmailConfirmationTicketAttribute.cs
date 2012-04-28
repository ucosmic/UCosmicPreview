using System;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateEmailConfirmationTicketAttribute : ActionFilterAttribute
    {
        public const string TicketPropertyName = "Ticket";
        public const string IntentPropertyName = "Intent";

        public string Intent { get; private set; }

        public IProcessQueries QueryProcessor { get; set; }

        public ValidateEmailConfirmationTicketAttribute(string intent)
        {
            if (intent == null) throw new ArgumentNullException("intent");
            Intent = intent;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            const string tokenKey = ConfirmEmailForm.TokenPropertyName;
            var routeValues = filterContext.RouteData.Values;
            var tempData = filterContext.Controller.TempData;

            // get the token value from the route
            var token = Guid.Empty;
            if (routeValues.ContainsKey(tokenKey) && routeValues[tokenKey] != null)
                Guid.TryParse(routeValues[tokenKey].ToString(), out token);

            // get the token from the action parameters
            if (token == Guid.Empty && filterContext.ActionParameters != null)
                foreach (var actionParameter in filterContext.ActionParameters)
                {
                    dynamic parameterValue = actionParameter.Value;
                    if (parameterValue is Guid && actionParameter.Key.Equals(tokenKey, StringComparison.OrdinalIgnoreCase))
                        token = (Guid)actionParameter.Value;

                    else if (actionParameter.Key == "model")
                        token = (Guid)parameterValue.Token;

                    if (token != Guid.Empty) break;
                }

            // get the confirmation
            var confirmation = QueryProcessor.Execute(
                new Domain.Email.GetEmailConfirmationQuery(token));

            // make sure the ticket is valid
            if (confirmation != null &&
                confirmation.Ticket != tempData.EmailConfirmationTicket())
                AddModelError(filterContext, TicketPropertyName);

            else if (token == Guid.Empty || confirmation == null)
                AddModelError(filterContext, tokenKey);

            // make sure the intent matches
            else if (confirmation.Intent != Intent)
                AddModelError(filterContext, IntentPropertyName);
        }

        private static void AddModelError(ControllerContext controllerContext, string key)
        {
            controllerContext.Controller.ViewData.ModelState.AddModelError(key, string.Empty);
        }
    }
}