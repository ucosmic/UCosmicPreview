using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class HealthRouteMapper
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Health.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(HealthRouteMapper), context, Area, Controller);
        }

        public static class SampleCachedPage
        {
            public const string Route = "health/sample-cached-page";
            private static readonly string Action = MVC.Common.Health.ActionNames.SampleCachedPage;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class RunEstablishmentHierarchy
        {
            public const string Route = "health/run-establishment-hierarchy";
            private static readonly string Action = MVC.Common.Health.ActionNames.RunEstablishmentHierarchy;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class RunInstitutionalAgreementHierarchy
        {
            public const string Route = "health/run-institutional-agreement-hierarchy";
            private static readonly string Action = MVC.Common.Health.ActionNames.RunInstitutionalAgreementHierarchy;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class RunEstablishmentImport
        {
            public const string Route = "health/run-establishment-import";
            private static readonly string Action = MVC.Common.Health.ActionNames.RunEstablishmentImport;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

    }
    // ReSharper restore UnusedMember.Global
}