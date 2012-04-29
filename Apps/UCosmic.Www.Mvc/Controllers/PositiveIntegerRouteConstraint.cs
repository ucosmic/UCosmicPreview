using System.Web;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public class PositiveIntegerRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route,
            string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName) && values[parameterName] != null)
            {
                var integer = 0;
                int.TryParse(values[parameterName].ToString(), out integer);
                return integer > 0;
            }
            return false;
        }
    }
}