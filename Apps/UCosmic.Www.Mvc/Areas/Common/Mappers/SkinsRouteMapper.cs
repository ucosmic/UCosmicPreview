using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class SkinsRouteMapper
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Skins.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(SkinsRouteMapper), context, Area, Controller);
        }

        public static class Change
        {
            public const string Route = "as/{skinContext}";
            private static readonly string Action = MVC.Common.Skins.ActionNames.Change;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Apply
        {
            public const string Route = "skins/apply/{skinFile}";
            private static readonly string Action = MVC.Common.Skins.ActionNames.Apply;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area, controller, action = Action,
                    skinFile = string.Empty,
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Logo
        {
            public const string Route = "skins/logo";
            private static readonly string Action = MVC.Common.Skins.ActionNames.Logo;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Sample
        {
            public static readonly string[] Routes = { "skins/sample/{content}", "skins" };
            private static readonly string Action = MVC.Common.Skins.ActionNames.Sample;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    content = new RequiredIfPresentRouteConstraint(),
                };
                context.MapRoutes(null, Routes, defaults, constraints);
            }
        }
    }
    // ReSharper restore UnusedMember.Global
}