using System;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class ErrorsController : Controller
    {
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

        [EnforceHttps]
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
            var exceptionLogger = DependencyInjector.Current.GetService<ILogExceptions>();
            exceptionLogger.LogException(model);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}
