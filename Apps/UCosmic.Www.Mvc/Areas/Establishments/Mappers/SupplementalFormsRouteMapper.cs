using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Establishments.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class SupplementalFormsRouteMapper
    {
        private static readonly string Area = MVC.Establishments.Name;
        private static readonly string Controller = MVC.Establishments.SupplementalForms.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(SupplementalFormsRouteMapper), context, Area, Controller);
        }

        public static class FindPlaces
        {
            public const string Route = "establishments/new/location/places";
            private static readonly string Action = MVC.Establishments.SupplementalForms.ActionNames.FindPlaces;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Locate
        {
            public static readonly string[] RoutesForGet = new[]
            {
                "establishments/{establishmentId}/locate/then-return-to/{*returnUrl}", 
                "establishments/{establishmentId}/locate"
            };

            public const string RouteForPost = "establishments/locate";
            private static readonly string Action = MVC.Establishments.SupplementalForms.ActionNames.Locate;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraintsForGetWithReturnUrl = new
                {
                    establishmentId = new NonEmptyGuidRouteConstraint(),
                    returnUrl = new RequiredIfPresentRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, RoutesForGet[0], defaults, constraintsForGetWithReturnUrl);

                var constraintsForGetWithoutReturnUrl = new
                {
                    establishmentId = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, RoutesForGet[1], defaults, constraintsForGetWithoutReturnUrl);

                var constraintsForPost = new
                {
                    httpMethod = new HttpMethodConstraint("POST")
                };
                context.MapRoute(null, RouteForPost, defaults, constraintsForPost);
            }
        }
    }
    // ReSharper restore UnusedMember.Global
}