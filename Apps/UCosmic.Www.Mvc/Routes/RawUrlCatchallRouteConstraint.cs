using System.Web;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Routes
{
    public class RawUrlCatchallRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                var value = values[parameterName];
                value = value ?? "/";
                if (!value.ToString().StartsWith("/"))
                    value = string.Format("/{0}", value);
                values[parameterName] = value;
                return true;
            }
            return false;
        }
    }
}