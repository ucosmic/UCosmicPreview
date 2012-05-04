//using System.Web.Mvc;
//using System.Web.Routing;
//using UCosmic.Www.Mvc.Controllers;

//namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
//{
//    public static class OldSignUpRouteMapper
//    {
//        private static readonly string Area = MVC.Identity.Name;
//        private static readonly string Controller = MVC.Identity.OldSignUp.Name;

//        public static void RegisterRoutes(AreaRegistrationContext context)
//        {
//            RootActionRouter.RegisterRoutes(typeof(OldSignUpRouteMapper), context, Area, Controller);
//        }

//        // ReSharper disable UnusedMember.Global

//        public static class ValidateSendEmail
//        {
//            public const string Route = "sign-up0/validate";
//            private static readonly string Action = MVC.Identity.OldSignUp.ActionNames.ValidateSendEmail;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class SendEmail
//        {
//            public const string Route = "sign-up0";
//            private static readonly string Action = MVC.Identity.OldSignUp.ActionNames.SendEmail;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class ValidateConfirmEmail
//        {
//            public const string Route = "sign-up0/confirm-email/validate/{token}/{secretCode}";
//            private static readonly string Action = MVC.Identity.OldSignUp.ActionNames.ValidateConfirmEmail;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new
//                {
//                    area,
//                    controller,
//                    action = Action,
//                    secretCode = UrlParameter.Optional,
//                };
//                var constraints = new
//                {
//                    token = new NonEmptyGuidRouteConstraint(),
//                    httpMethod = new HttpMethodConstraint("POST")
//                };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class ConfirmEmail
//        {
//            public const string RouteForGet = "sign-up0/confirm-email/{token}/{secretCode}";
//            public const string RouteForPost = "sign-up/confirm-email";
//            private static readonly string Action = MVC.Identity.OldSignUp.ActionNames.ConfirmEmail;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                MapRoutesForGet(context, area, controller);
//                MapRoutesForPost(context, area, controller);
//            }
//            private static void MapRoutesForGet(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new
//                {
//                    area,
//                    controller,
//                    action = Action,
//                    secretCode = UrlParameter.Optional,
//                };
//                var constraints = new
//                {
//                    token = new NonEmptyGuidRouteConstraint(),
//                    httpMethod = new HttpMethodConstraint("GET"),
//                };
//                context.MapRoute(null, RouteForGet, defaults, constraints);
//            }
//            private static void MapRoutesForPost(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
//                context.MapRoute(null, RouteForPost, defaults, constraints);
//            }
//        }

//        public static class CreatePassword
//        {
//            public const string Route = "sign-up0/create-password";
//            private static readonly string Action = MVC.Identity.OldSignUp.ActionNames.CreatePassword;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class SignIn
//        {
//            public const string Route = "sign-up0/sign-in";
//            private static readonly string Action = MVC.Identity.OldSignUp.ActionNames.SignIn;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        // ReSharper restore UnusedMember.Global
//    }
//}