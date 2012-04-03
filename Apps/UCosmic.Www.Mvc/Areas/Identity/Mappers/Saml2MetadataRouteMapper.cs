using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class Saml2MetadataRouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.Saml2Metadata.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            if (!WebConfig.IsDeployedToCloud)
                DefaultRouteMapper.RegisterRoutes(typeof(Saml2MetadataRouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Index
        {
            public const string Route = "sign-on/saml/2/metadata";
            private static readonly string Action = MVC.Identity.Saml2Metadata.ActionNames.Index;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Development
        {
            public const string Route = "sign-on/saml/2/metadata/development";
            private static readonly string Action = MVC.Identity.Saml2Metadata.ActionNames.Development;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global

    }
}