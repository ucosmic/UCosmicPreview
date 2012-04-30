using System;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateConfirmEmailAttribute : ActionFilterAttribute
    {
        public string TokenParamName { get; private set; }

        public ValidateConfirmEmailAttribute(string tokenParamName)
        {
            if (tokenParamName == null) throw new ArgumentNullException("tokenParamName");
            TokenParamName = tokenParamName;
        }

        public IProcessQueries QueryProcessor { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var token = GetToken(filterContext);

            // get the confirmation
            EmailConfirmation confirmation;
            var found = ValidateEmailConfirmation.TokenMatchesEntity
                (token, QueryProcessor, out confirmation);

            ConfirmDeniedModel model = null;
            RouteValueDictionary routeValues = null;

            // make sure the token matches a confirmation
            if (token == Guid.Empty || !found)
                filterContext.Result = new HttpNotFoundResult();

            // make sure the token is not expired
            else if (confirmation.IsExpired)
                model = new ConfirmDeniedModel(
                    ConfirmDeniedBecause.IsExpired, confirmation.Intent);


            // make sure the token is not retired
            else if (confirmation.IsRetired)
                model = new ConfirmDeniedModel(
                    ConfirmDeniedBecause.IsRetired, confirmation.Intent);

            // make sure the token is not redeemed
            else if (confirmation.IsRedeemed)
                routeValues = GetRedeemedRouteValues
                    (confirmation.Token, confirmation.Intent);

            // set the result if necessary
            if (model != null)
                filterContext.Result = new PartialViewResult
                {
                    ViewName = MVC.Identity.ConfirmEmail.Views._denied,
                    ViewData = new ViewDataDictionary(model),
                };

            else if (routeValues != null)
                filterContext.Result = new RedirectToRouteResult(routeValues);
        }

        protected internal Guid GetToken(ActionExecutingContext filterContext)
        {
            Guid token;

            // verify the constructor arguments
            if (!filterContext.ActionParameters.ContainsKey(TokenParamName))
                throw new InvalidOperationException(String.Format(
                    "The action method does not have a '{0}' parameter.",
                        TokenParamName));

            // get the token value
            var tokenValue = filterContext.ActionParameters[TokenParamName];
            if (tokenValue is Guid)
                token = (Guid)tokenValue;

            else if (tokenValue is IModelEmailConfirmation)
                token = ((IModelEmailConfirmation)tokenValue).Token;

            else
                throw new InvalidOperationException(String.Format(
                    "Unable to locate the a Guid token for action method parameter '{0}'.",
                        TokenParamName));

            return token;
        }

        internal static RouteValueDictionary GetRedeemedRouteValues(Guid token, string intent)
        {
            switch (intent)
            {
                case EmailConfirmationIntent.PasswordReset:
                    return new RouteValueDictionary(new
                    {
                        area = MVC.Passwords.Name,
                        controller = MVC.Passwords.ResetPassword.Name,
                        action = MVC.Passwords.ResetPassword.ActionNames.Get,
                        token,
                    });
                default:
                    throw new NotSupportedException(String.Format(
                        "The email confirmation intent '{0}' is not supported.",
                        intent));
            }
        }
    }
}