using System.Web.Mvc;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public class TryAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.User.Identity.Name))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "area", MVC.Common.Name },
                    { "controller", MVC.Common.Errors.Name },
                    { "action", MVC.Common.Errors.ActionNames.NotAuthorized },
                    { "url", filterContext.HttpContext.Request.RawUrl },
                });
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}