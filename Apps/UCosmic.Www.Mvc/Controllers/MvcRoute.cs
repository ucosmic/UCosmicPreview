using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public abstract class MvcRoute : Route
    {
        protected MvcRoute(IRouteHandler routeHandler = null)
            : base(null, routeHandler)
        {
            RouteHandler = routeHandler ?? new MvcRouteHandler();
        }

        public int Order { get; set; }
        public IEnumerable<string> AlternateUrls { get; set; }
    }
}