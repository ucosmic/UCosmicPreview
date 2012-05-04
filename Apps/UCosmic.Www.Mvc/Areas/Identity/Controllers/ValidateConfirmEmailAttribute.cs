using System;
using System.Web.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateConfirmEmailAttribute : ActionFilterAttribute
    {
        public string ParamName { get; private set; }

        public ValidateConfirmEmailAttribute(string paramName)
        {
            if (paramName == null) throw new ArgumentNullException("paramName");
            ParamName = paramName;
        }

        public IProcessQueries QueryProcessor { get; set; }

        protected EmailConfirmation EmailConfirmation { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!ValidateToken(filterContext)) return;

            // make sure the token is not expired
            if (!ValidateExpiration(filterContext)) return;

            // make sure the token is not retired
            ValidateRetirement(filterContext);
        }

        protected internal Guid GetToken(ActionExecutingContext filterContext)
        {
            Guid token;

            // verify the constructor arguments
            if (!filterContext.ActionParameters.ContainsKey(ParamName))
                throw new InvalidOperationException(String.Format(
                    "The action method does not have a '{0}' parameter.",
                        ParamName));

            // get the token value
            var tokenValue = filterContext.ActionParameters[ParamName];
            if (tokenValue is Guid)
                token = (Guid)tokenValue;

            else if (tokenValue is IModelConfirmAndRedeem)
                token = ((IModelConfirmAndRedeem)tokenValue).Token;

            else
                throw new InvalidOperationException(String.Format(
                    "The action method parameter '{0}' must either be a Guid " + 
                    "or implement the '{1}' interface.",
                        ParamName, typeof(IModelConfirmAndRedeem).FullName));

            return token;
        }

        private bool ValidateToken(ActionExecutingContext filterContext)
        {
            // get the token
            var token = GetToken(filterContext);

            // get the confirmation
            EmailConfirmation catchEntity;
            var found = ValidateEmailConfirmation.TokenMatchesEntity
                (token, QueryProcessor, out catchEntity);
            EmailConfirmation = catchEntity;

            // valid when matches an entity
            if (token != Guid.Empty && found) return true;

            filterContext.Result = new HttpNotFoundResult();
            return false;
        }

        private bool ValidateExpiration(ActionExecutingContext filterContext)
        {
            if (!EmailConfirmation.IsExpired) return true;

            HandleDenial(filterContext, ConfirmDeniedBecause.IsExpired);
            return false;
        }

        private void ValidateRetirement(ActionExecutingContext filterContext)
        {
            if (!EmailConfirmation.IsRetired) return;

            HandleDenial(filterContext, ConfirmDeniedBecause.IsRetired);
        }

        protected void HandleDenial(ActionExecutingContext filterContext, ConfirmDeniedBecause reason)
        {
            var model = new ConfirmDeniedModel(reason, EmailConfirmation.Intent);
            filterContext.Result = new PartialViewResult
            {
                ViewName = MVC.Identity.Shared.Views._confirm_email_denied,
                ViewData = new ViewDataDictionary(model),
            };
        }
    }
}