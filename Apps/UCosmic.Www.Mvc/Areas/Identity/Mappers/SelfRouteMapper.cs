using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class SelfRouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.Self.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(SelfRouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Me
        {
            public const string OutboundRoute = "my/profile";
            public const string AlternateRoute = "my";
            private static readonly string Action = MVC.Identity.Self.ActionNames.Me;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var outboundConstraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                var inboundConstraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, OutboundRoute, defaults, outboundConstraints);
                context.MapRoute(null, AlternateRoute, defaults, inboundConstraints);
            }
        }

        public static class EditAffiliation
        {
            public const string RouteForGet = "me/affiliations/{entityId}/edit.html";
            public const string RouteForPost = "me/affiliations/edit.html";
            private static readonly string Action = MVC.Identity.Self.ActionNames.EditAffiliation;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraintsForGet = new
                {
                        entityId = new NonEmptyGuidRouteConstraint(),
                        httpMethod = new HttpMethodConstraint("GET"),
                };
                var constraintsForPost = new
                {
                    httpMethod = new HttpMethodConstraint("POST")
                };
                context.MapRoute(null, RouteForGet, defaults, constraintsForGet);
                context.MapRoute(null, RouteForPost, defaults, constraintsForPost);
            }
        }

        public static class AutoCompleteNameSalutations
        {
            public const string Route = "people/autocomplete/name-salutations.json";
            private static readonly string Action = MVC.Identity.Self.ActionNames.AutoCompleteNameSalutations;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class AutoCompleteNameSuffixes
        {
            public const string Route = "people/autocomplete/name-suffixes.json";
            private static readonly string Action = MVC.Identity.Self.ActionNames.AutoCompleteNameSuffixes;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class AutoCompletePersonName
        {
            public const string Route = "people/autocomplete/name.json";
            private static readonly string Action = MVC.Identity.Self.ActionNames.AutoCompletePersonName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class PersonInfoById
        {
            public const string Route = "people/info-by-id.json";
            private static readonly string Action = MVC.Identity.Self.ActionNames.PersonInfoById;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class PersonInfoByEmail
        {
            public const string Route = "people/info-by-email.json";
            private static readonly string Action = MVC.Identity.Self.ActionNames.PersonInfoByEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class DeriveDisplayName
        {
            public const string Route = "people/derive-display-name.json";
            private static readonly string Action = MVC.Identity.Self.ActionNames.DeriveDisplayName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}