using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class PasswordRouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.Password.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(PasswordRouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class ChangePassword
        {
            public const string Route = "me/change-password.html";
            private static readonly string Action = MVC.Identity.Password.ActionNames.ChangePassword;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ForgotPassword
        {
            public const string Route = "i-forgot-my-password";
            private static readonly string Action = MVC.Identity.Password.ActionNames.ForgotPassword;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ResetPassword
        {
            public const string Route = "reset-password";
            private static readonly string Action = MVC.Identity.Password.ActionNames.ResetPassword;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}