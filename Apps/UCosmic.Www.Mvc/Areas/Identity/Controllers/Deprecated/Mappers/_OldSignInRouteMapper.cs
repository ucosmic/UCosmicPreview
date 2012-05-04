//using System.Web.Mvc;
//using System.Web.Routing;
//using UCosmic.Www.Mvc.Controllers;

//namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
//{
//    public static class OldSignInRouteMapper
//    {
//        private static readonly string Area = MVC.Identity.Name;
//        private static readonly string Controller = MVC.Identity.OldSignIn.Name;

//        public static void RegisterRoutes(AreaRegistrationContext context)
//        {
//            RootActionRouter.RegisterRoutes(typeof(OldSignInRouteMapper), context, Area, Controller);
//        }

//        // ReSharper disable UnusedMember.Global

//        public static class SignIn
//        {
//            public const string Route = "sign-in0";
//            private static readonly string Action = MVC.Identity.OldSignIn.ActionNames.SignIn;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class SignOut
//        {
//            public const string Route = "sign-out0";
//            private static readonly string Action = MVC.Identity.OldSignIn.ActionNames.SignOut;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class SignInAs
//        {
//            public const string Route = "sign-in/as";
//            private static readonly string Action = MVC.Identity.OldSignIn.ActionNames.SignInAs;
//            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
//            {
//                var defaults = new { area, controller, action = Action, };
//                var constraints = new
//                {
//                    httpMethod = new HttpMethodConstraint("GET", "POST"),
//                };
//                context.MapRoute(null, Route, defaults, constraints);
//            }
//        }

//        public static class UndoSignInAs
//        {
//            public const string Route = "sign-in/as/undo/then-go-to/{*returnUrl}";
//            private static readonly string Action = MVC.Identity.OldSignIn.ActionNames.UndoSignInAs;
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