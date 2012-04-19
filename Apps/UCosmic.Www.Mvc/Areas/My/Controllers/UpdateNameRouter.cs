using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public static class UpdateNameRouter
    {
        private static readonly string Area = MVC.My.Name;
        private static readonly string Controller = MVC.My.UpdateName.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(UpdateNameRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/name/edit";
            private static readonly string Action = MVC.My.UpdateName.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Put
        {
            public const string Route = "my/name";
            private static readonly string Action = MVC.My.UpdateName.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST", "PUT"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }
    }
}