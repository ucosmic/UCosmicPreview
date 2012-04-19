using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public static class PersonNameRouter
    {
        private static readonly string Area = MVC.People.Name;
        private static readonly string Controller = MVC.People.PersonName.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(PersonNameRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class GenerateDisplayName
        {
            public const string Route = "people/generate-display-name";
            private static readonly string Action = MVC.People.PersonName.ActionNames.GenerateDisplayName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class AutoCompleteSalutations
        {
            public const string Route = "people/salutations";
            private static readonly string Action = MVC.People.PersonName.ActionNames.AutoCompleteSalutations;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}