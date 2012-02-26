using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class ErrorsRouteMapper
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Errors.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(ErrorsRouteMapper), context, Area, Controller);
        }

        public static class NotFound
        {
            public const string Route = "errors/404.html";
            private static readonly string Action = MVC.Common.Errors.ActionNames.NotFound;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class NotFoundByHackerSniff
        {
            // Since UCosmic uses ELMAH, hacker sniffing requests which generate 404's cause
            // emails to be sent out to administrators. By setting up inbound URL routes to
            // some common and frequent URL attack patterns, we can bypass the 404 exception
            // and route users to the 404 page without triggering mail from ELMAH.

            private static readonly string Action = MVC.Common.Errors.ActionNames.NotFound;
            public static readonly string[] Routes = new[]
            {
                "admin/{*catchall}",
                "mysql/{*catchall}",
                "phpMyAdmin/{*catchall}",
                "scripts/setup.php",
                "{prefix}/scripts/setup.php",
                "user/soapCaller.bs",
                "cgi-bin/{*catchall}",
                "jmx-console/{*catchall}",
                "cn/{*catchall}",
                "pp/{*catchall}",
                "appserv/{*catchall}",
                "manager/{*catchall}",
                "crossdomain.xml",
            };
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };

                // prevent ZmEu & script kiddies from logging Elmah 404 errors
                foreach (var route in Routes) context.MapRoute(null, route, defaults);
            }
        }

        public static class FileUploadTooLarge
        {
            public const string Route = "errors/file-upload-too-large.html";
            private static readonly string Action = MVC.Common.Errors.ActionNames.FileUploadTooLarge;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class NotAuthorized
        {
            public static readonly string[] Routes = { "errors/not-authorized-for/{*url}", "errors/403" };
            private static readonly string Action = MVC.Common.Errors.ActionNames.NotAuthorized;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { url = new RequiredIfPresentConstraint() };
                context.MapRoute(null, Routes[0], defaults, constraints);
                context.MapRoute(null, Routes[1], defaults);
            }
        }

        public static class BadRequest
        {
            public const string Route = "errors/400.html";
            private static readonly string Action = MVC.Common.Errors.ActionNames.BadRequest;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class Unexpected
        {
            public const string Route = "errors/unexpected.html";
            private static readonly string Action = MVC.Common.Errors.ActionNames.Unexpected;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class Throw
        {
            public const string Route = "errors/throw.html";
            private static readonly string Action = MVC.Common.Errors.ActionNames.Throw;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class LogAjaxError
        {
            public const string Route = "errors/log-ajax-error.json";
            private static readonly string Action = MVC.Common.Errors.ActionNames.LogAjaxError;
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