using System;
using System.Web.Mvc;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class EnforceHttpsAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            base.HandleNonHttpsRequest(filterContext);

            // RequireHttps does not work in AppHarbor: http://stackoverflow.com/a/8977247/304832
            var xForwardedProtoHeader = filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"];
            if (WebConfig.IsDeployedTo(DeployToTarget.Test) &&
                filterContext.Result is RedirectResult &&
                "https".Equals(xForwardedProtoHeader, StringComparison.InvariantCultureIgnoreCase))
                filterContext.Result = null;
        }
    }
}