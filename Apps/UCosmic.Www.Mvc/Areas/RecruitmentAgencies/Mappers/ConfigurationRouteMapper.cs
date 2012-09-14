using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies.Mappers
{
    public class ConfigurationRouteMapper
    {
        private static readonly string Area = MVC.RecruitmentAgencies.Name;
        private static readonly string Controller = MVC.RecruitmentAgencies.Configuration.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ConfigurationRouteMapper), context, Area, Controller);
        }

        public static class Browse
        {
            public const string Route = "Recruitment-Agencies";
            private static readonly string Action = MVC.RecruitmentAgencies.Configuration.ActionNames.Configure;

            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new {area, controller, action = Action};
                var constraints = new {httpMethod = new HttpMethodConstraint("GET")};
                context.MapRoute(null, Route, defaults, constraints);
            }
        }
    }
}