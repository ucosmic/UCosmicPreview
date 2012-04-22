using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public static class UpdateEmailValueRouter
    {
        private static readonly string Area = MVC.My.Name;
        private static readonly string Controller = MVC.My.UpdateEmailValue.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(UpdateEmailValueRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/emails/{number}/change-spelling";
            private static readonly string Action = MVC.My.UpdateEmailValue.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Put
        {
            public const string Route = "my/emails/{number}";
            private static readonly string Action = MVC.My.UpdateEmailValue.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateValue
        {
            public const string Route = "my/emails/{number}/change-spelling/validate";
            private static readonly string Action = MVC.My.UpdateEmailValue.ActionNames.ValidateValue;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}