using System;
using System.Web;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Mappers
{
    public class NonEmptyGuidRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route,
            string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                var guid = values[parameterName] as Guid?;
                if (!guid.HasValue)
                {
                    var stringValue = values[parameterName] as string;
                    if (!string.IsNullOrWhiteSpace(stringValue))
                    {
                        Guid parsedGuid;
                        Guid.TryParse(stringValue, out parsedGuid);
                        guid = parsedGuid;
                    }
                }
                return (guid.HasValue && guid.Value != Guid.Empty);
            }
            return false;
        }
    }
}