using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public static class PersonInfoRouter
    {
        private static readonly string Area = MVC.People.Name;
        private static readonly string Controller = MVC.People.PersonInfo.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(PersonInfoRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class ByEmail
        {
            public const string Route = "people/by-email";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.ByEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ByGuid
        {
            public const string Route = "people/by-guid";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.ByGuid;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class WithEmail
        {
            public const string Route = "people/with-email";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.WithEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class WithFirstName
        {
            public const string Route = "people/with-first-name";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.WithFirstName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class WithLastName
        {
            public const string Route = "people/with-last-name";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.WithLastName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}