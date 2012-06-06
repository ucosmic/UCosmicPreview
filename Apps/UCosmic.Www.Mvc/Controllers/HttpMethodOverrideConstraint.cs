using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public class HttpMethodOverrideConstraint : HttpMethodConstraint
    {
        public HttpMethodOverrideConstraint(params string[] allowedMethods) : base(allowedMethods) { }

        protected override bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var methodOverride = httpContext.Request.Unvalidated().Form["X-HTTP-Method-Override"];
            if (methodOverride == null) return base.Match(httpContext, route, parameterName, values, routeDirection);
            return AllowedMethods.Any(m => string.Equals(m, httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
                && AllowedMethods.Any(m => string.Equals(m, methodOverride, StringComparison.OrdinalIgnoreCase));
        }
    }
}