using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class FeaturesRouteMapper
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Features.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(FeaturesRouteMapper), context, Area, Controller);
        }

        public static class ReleasesNav
        {
            public const string Route = "releases/nav";
            private static readonly string Action = MVC.Common.Features.ActionNames.ReleasesNav;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Releases
        {
            public const string CurrentVersion = "february-2012-preview-2";
            public static readonly string[] Routes = { string.Empty, "releases/{version}" };
            private static readonly string Action = MVC.Common.Features.ActionNames.Releases;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    version = CurrentVersion
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoutes(null, Routes, defaults, constraints);
            }
        }

        public static class Requirements
        {
            public const string Route = "features/{module}";
            private static readonly string Action = MVC.Common.Features.ActionNames.Requirements;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    module = UrlParameter.Optional
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

    }
    // ReSharper restore UnusedMember.Global
}