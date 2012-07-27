using System;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ReturnUrlReferrerAttribute : ActionFilterAttribute
    {
        public ReturnUrlReferrerAttribute(string fallback)
        {
            if (string.IsNullOrWhiteSpace(fallback))
                throw new ArgumentException("Cannot be null or whitespace.", "fallback");
            Fallback = fallback;
        }

        public string Fallback { get; private set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var model = filterContext.Controller.ViewData.Model as IReturnUrl;
            if (model == null) return;

            var returnUrl = filterContext.HttpContext.Request.UrlReferrer != null
                ? filterContext.HttpContext.Request.UrlReferrer.ToString()
                : Fallback;

            if (!returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                && !returnUrl.StartsWith("https://"))
                returnUrl = string.Format("/{0}", returnUrl);

            model.ReturnUrl = returnUrl;
        }
    }
}