using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public abstract class OrderedMvcRoute : MvcRoute
    {
        protected OrderedMvcRoute(IRouteHandler routeHandler = null)
            : base(routeHandler)
        {
        }

        public int Order { get; set; }
    }
}