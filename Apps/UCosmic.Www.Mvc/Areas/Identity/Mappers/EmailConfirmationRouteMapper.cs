using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class EmailConfirmationRouteMapper
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.EmailConfirmation.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(EmailConfirmationRouteMapper), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class ConfirmForPasswordReset
        {
            public static readonly string[] Routes = new[]
            {
                "confirm-password-reset/t-{token}.html", 
                "confirm-password-reset/t-{token}/{secretCode}",
                "confirm-password-reset.html",
            };
            private static readonly string Action = 
                MVC.Identity.EmailConfirmation.ActionNames.ConfirmForPasswordReset;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var defaultsWithSecret = new
                {
                    area, controller, action = Action, 
                    secretCode = UrlParameter.Optional,
                };
                var getConstraints = new
                {
                    token = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                var postConstraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                };
                context.MapRoute(null, Routes[0], defaults, getConstraints);
                context.MapRoute(null, Routes[1], defaultsWithSecret, getConstraints);
                context.MapRoute(null, Routes[2], defaults, postConstraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}