using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public static class UpdateAffiliationRouter
    {
        private static readonly string Area = MVC.My.Name;
        private static readonly string Controller = MVC.My.UpdateAffiliation.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(UpdateAffiliationRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/affiliations/{establishmentId}/edit";
            private static readonly string Action = MVC.My.UpdateAffiliation.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    establishmentId = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Put
        {
            public const string Route = "my/affiliations/{establishmentId}";
            private static readonly string Action = MVC.My.UpdateAffiliation.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                    establishmentId = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}