using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Establishments.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class ManagementFormsRouteMapper
    {
        private static readonly string Area = MVC.Establishments.Name;
        private static readonly string Controller = MVC.Establishments.ManagementForms.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            if (!WebConfig.IsDeployedToCloud)
                DefaultRouteMapper.RegisterRoutes(typeof(ManagementFormsRouteMapper), context, Area, Controller);
        }

        public static class Browse
        {
            public static readonly string[] Routes = { "establishments", "establishments/manage", "establishments/manage/browse", "establishments/manage/browse.html" };
            private static readonly string Action = MVC.Establishments.ManagementForms.ActionNames.Browse;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoutes(null, Routes, defaults, constraints);
            }
        }

        public static class Form
        {
            public const string RouteForEdit = "establishments/{entityId}/edit";
            public const string RouteForAdd = "establishments/new";
            private static readonly string Action = MVC.Establishments.ManagementForms.ActionNames.Form;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraintsForEdit = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    entityId = new NonEmptyGuidConstraint(),
                };
                context.MapRoute(null, RouteForEdit, defaults, constraintsForEdit);

                var constraintsForAdd = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, RouteForAdd, defaults, constraintsForAdd);
            }
        }

        public static class Put
        {
            public const string Route = "establishments/{entityId}";
            private static readonly string Action = MVC.Establishments.ManagementForms.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("PUT", "POST"),
                    entityId = new NonEmptyGuidConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class NewName
        {
            public const string Route = "establishments/new/name";
            private static readonly string Action = MVC.Establishments.ManagementForms.ActionNames.NewName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }
    }
    // ReSharper restore UnusedMember.Global
}