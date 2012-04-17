using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class SignUpRouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignUp.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(SignUpRouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class ValidateSendEmail
        {
            public const string Route = "sign-up/validate";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.ValidateSendEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class SendEmail
        {
            public const string Route = "sign-up";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.SendEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateConfirmEmail
        {
            public const string Route = "sign-up/confirm-email/validate/{token}/{secretCode}";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.ValidateConfirmEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    secretCode = UrlParameter.Optional,
                };
                var constraints = new
                {
                    token = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("POST")
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ConfirmEmail
        {
            public const string RouteForGet = "sign-up/confirm-email/{token}/{secretCode}";
            public const string RouteForPost = "sign-up/confirm-email";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.ConfirmEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                MapRoutesForGet(context, area, controller);
                MapRoutesForPost(context, area, controller);
            }
            private static void MapRoutesForGet(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    secretCode = UrlParameter.Optional,
                };
                var constraints = new
                {
                    token = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, RouteForGet, defaults, constraints);
            }
            private static void MapRoutesForPost(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, RouteForPost, defaults, constraints);
            }
        }

        public static class CreatePassword
        {
            public const string Route = "sign-up/create-password";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.CreatePassword;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class SignIn
        {
            public const string Route = "sign-up/sign-in";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.SignIn;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}