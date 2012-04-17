using System.Web;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Routes
{
    public class RequiredIfPresentRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                var value = values[parameterName];
                return value != null;
            }
            return true;
        }
    }
}