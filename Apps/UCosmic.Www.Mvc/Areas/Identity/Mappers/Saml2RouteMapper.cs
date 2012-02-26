using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class Saml2RouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.Saml2.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(Saml2RouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class ComponentSpaceSignOn
        {
            public const string RouteForGet = "sign-on/saml/2";
            public const string RouteForPost = "saml/2/sign-on/post";
            private static readonly string Action = MVC.Identity.Saml2.ActionNames.ComponentSpaceSignOn;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraintsForGet = new { httpMethod = new HttpMethodConstraint("GET") };
                var constraintsForPost = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, RouteForGet, defaults, constraintsForGet);
                context.MapRoute(null, RouteForPost, defaults, constraintsForPost);
            }
        }

        public static class SignOnSuccess
        {
            public const string Route = "sign-on/saml2/success";
            private static readonly string Action = MVC.Identity.Saml2.ActionNames.SignOnSuccess;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class SignOff
        {
            public const string Route = "sign-off/saml2";
            private static readonly string Action = MVC.Identity.Saml2.ActionNames.SignOff;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class SignOffSuccess
        {
            public const string Route = "sign-off/saml2/success";
            private static readonly string Action = MVC.Identity.Saml2.ActionNames.SignOffSuccess;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Metadata
        {
            public const string Route = "shibboleth";
            private static readonly string Action = MVC.Identity.Saml2.ActionNames.Metadata;
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