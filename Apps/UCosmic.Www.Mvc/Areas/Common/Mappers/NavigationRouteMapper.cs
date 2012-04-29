using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class NavigationRouteMapper
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Navigation.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(NavigationRouteMapper), context, Area, Controller);
        }

        public static class HorizontalTabs
        {
            public const string Route = "navigation/horizontal-tabs.partial.html";
            private static readonly string Action = MVC.Common.Navigation.ActionNames.HorizontalTabs;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

    }
    // ReSharper restore UnusedMember.Global
}