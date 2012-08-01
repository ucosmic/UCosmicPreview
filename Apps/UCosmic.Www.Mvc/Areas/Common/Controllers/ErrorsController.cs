using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class ErrorsController : Controller
    {
        private readonly ILogExceptions _exceptionLogger;

        public ErrorsController(ILogExceptions exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        [ActionName("not-found")]
        public virtual ActionResult NotFound()
        {
            // do not return 404 for missing css files
            if (Request.RawUrl.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
            {
                Response.StatusCode = 404;
                return new EmptyResult();
            }
            return View();
        }

        [ActionName("file-upload-too-large")]
        public virtual ActionResult FileUploadTooLarge(string path)
        {
            // When app's httpRuntime maxRequestLength is GREATER THAN server's requestLimits maxAllowedContentLength,
            // server will generate an HTTP 404.13 header and the path parameter will be null.
            // Otherwise, when app's httpRuntime maxRequestLength is LESS THAN server's requestLimits maxAllowedContentLength,
            // Application_Error will kick in and redirect with a path parameter.
            ViewBag.Path = path;
            if (string.IsNullOrWhiteSpace(path) && Request.UrlReferrer != null)
            {
                ViewBag.Path = Request.UrlReferrer.AbsolutePath;
            }
            return View();
        }

        [ActionName("not-authorized")]
        public virtual ActionResult NotAuthorized(string url = null)
        {
            if (!string.IsNullOrEmpty(url))
            {
                ViewBag.Url = url;
            }
            else
            {
                Response.StatusCode = 403;
            }
            return View();
        }

        [ActionName("bad-request")]
        public virtual ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }

        [ActionName("unexpected")]
        public virtual ActionResult Unexpected()
        {
            return View(MVC.Shared.Views.Error);
        }

        [ActionName("throw")]
        public virtual ActionResult Throw()
        {
            var ex = new Exception("This is a test exception thrown on purpose from the web server.");
            throw ex;
        }

        [ActionName("log-ajax-error")]
        public virtual JsonResult LogAjaxError(JQueryAjaxException model)
        {
            _exceptionLogger.LogException(model);
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }

    public static class ErrorsRouter
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Errors.Name;

        public class NotFoundRoute : MvcRoute
        {
            public NotFoundRoute()
            {
                Url = "errors/404.html";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.NotFound,
                });
            }
        }

        public class NotFoundByHackerSniffRoute : NotFoundRoute
        {
            // Since UCosmic uses ELMAH, hacker sniffing requests which generate 404's cause
            // emails to be sent out to administrators. By setting up inbound URL routes to
            // some common and frequent URL attack patterns, we can bypass the 404 exception
            // and route users to the 404 page without triggering mail from ELMAH.

            public static readonly string[] OtherUrls = new[]
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

            public NotFoundByHackerSniffRoute()
            {
                Url = OtherUrls.Last();
                foreach (var otherUrl in OtherUrls.Take(OtherUrls.Length - 1))
                {
                    RouteTable.Routes.Add(new NotFoundRoute { Url = otherUrl });
                }
            }
        }

        public class FileUploadTooLargeRoute : MvcRoute
        {
            public FileUploadTooLargeRoute()
            {
                Url = "errors/file-upload-too-large.html";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.FileUploadTooLarge,
                });
            }
        }

        public class NotAuthorizedRoute : MvcRoute
        {
            public NotAuthorizedRoute()
            {
                Url = "errors/not-authorized-for/{*url}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.NotAuthorized,
                });
                Constraints = new RouteValueDictionary(new
                {
                    url = new RequiredIfPresentRouteConstraint(),
                });
            }
        }

        public class NotAuthorized403Route : NotAuthorizedRoute
        {
            public NotAuthorized403Route()
            {
                Url = "errors/403";
            }
        }

        public class BadRequestRoute : MvcRoute
        {
            public BadRequestRoute()
            {
                Url = "errors/400.html";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.BadRequest,
                });
            }
        }

        public class UnexpectedRoute:Route
        {
            public UnexpectedRoute()
                : base("errors/unexpected.html", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.Unexpected,
                });
            }
        }

        public class ThrowRoute:Route
        {
            public ThrowRoute()
                : base("errors/throw.html", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.Throw,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class LogAjaxErrorRoute : MvcRoute
        {
            public LogAjaxErrorRoute()
            {
                Url = "errors/log-ajax-error.json";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Errors.ActionNames.LogAjaxError,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"), // todo: this should be POST
                });
            }
        }
    }
}
