using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class QaRouteMapper
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Qa.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            if (!WebConfig.IsDeployedToCloud)
                DefaultRouter.RegisterRoutes(typeof(QaRouteMapper), context, Area, Controller);
        }

        public static class DeliverQaMail
        {
            public const string Route = "qa/deliver-mail";
            private static readonly string Action = MVC.Common.Qa.ActionNames.DeliverQaMail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ResetQaMail
        {
            public const string Route = "qa/reset-mail";
            private static readonly string Action = MVC.Common.Qa.ActionNames.ResetQaMail;
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