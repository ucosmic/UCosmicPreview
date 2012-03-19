using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class SignOnRouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOn.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(SignOnRouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Begin
        {
            public const string Route = "sign-on";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Begin;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Saml2Post
        {
            public const string Route = "sign-on/saml/2/post";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Saml2Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Saml2Integrations
        {
            public const string Route = "sign-on/saml/2/providers";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Saml2Integrations;
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