using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    // ReSharper disable UnusedMember.Global
    public static class ErrorsRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class NotFound
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.NotFound();
                var url = new ErrorsRouter.NotFoundRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.NotFound();
                var url = new ErrorsRouter.NotFoundRoute().Url.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }
        }

        [TestClass]
        public class FileUploadTooLarge
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.FileUploadTooLarge(null);
                var url = new ErrorsRouter.FileUploadTooLargeRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.FileUploadTooLarge(null);
                var url = new ErrorsRouter.FileUploadTooLargeRoute().Url.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }
        }

        [TestClass]
        public class NotAuthorized
        {
            [TestMethod]
            public void Maps2Urls_FirstWithCatchall_ThenWithout()
            {
                new ErrorsRouter.NotAuthorizedRoute();
                new ErrorsRouter.NotAuthorizedRoute403();
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNullArg_IsRoutedTo_UrlWithoutCatchall()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(null);
                var url = new ErrorsRouter.NotAuthorizedRoute403().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithCatchallArg_IsRoutedTo_UrlWithCatchall()
            {
                const string attemptedUrl = "path/to/action";
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(attemptedUrl);
                var url = new ErrorsRouter.NotAuthorizedRoute().Url.ToAppRelativeUrl()
                    .Replace("{*url}", attemptedUrl);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_AndNoCatchall_IsRoutedTo_ActionWithNullArg()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(null);
                var url = new ErrorsRouter.NotAuthorizedRoute403().Url.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_AndCatchall_IsRoutedTo_ActionWithCatchallArg()
            {
                const string attemptedUrl = "path/to/action";
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(attemptedUrl);
                var url = new ErrorsRouter.NotAuthorizedRoute().Url.ToAppRelativeUrl()
                    .Replace("{*url}", attemptedUrl);
                url.WithAnyMethod().ShouldMapTo(action);
            }
        }

        [TestClass]
        public class BadRequest
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.BadRequest();
                var url = new ErrorsRouter.BadRequestRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.BadRequest();
                var url = new ErrorsRouter.BadRequestRoute().Url.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }
        }

        [TestClass]
        public class Unexpected
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Unexpected();
                var url = new ErrorsRouter.UnexpectedRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Unexpected();
                var url = new ErrorsRouter.UnexpectedRoute().Url.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }
        }

        [TestClass]
        public class Throw
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Throw();
                var url = new ErrorsRouter.ThrowRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Throw();
                var url = new ErrorsRouter.ThrowRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new ErrorsRouter.ThrowRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class LogAjaxError
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.LogAjaxError(null);
                var url = new ErrorsRouter.LogAjaxErrorRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.LogAjaxError(null);
                var url = new ErrorsRouter.LogAjaxErrorRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new ErrorsRouter.LogAjaxErrorRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class NotFoundByHackerSniff
        {
            [TestMethod]
            public void InBoundUrls_AreRoutedTo_NotFoud()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.NotFound();
                var routeUrls = ErrorsRouter.NotFoundByHackerSniffRoute.OtherUrls;

                const string catchallParam = "*catchall";
                const string catchallValue = "any";
                const string prefixParam = "prefix";
                const string prefixValue = "any";
                foreach (var routeUrl in routeUrls)
                {
                    if (routeUrl.Contains(catchallParam))
                    {
                        var urlFormat = routeUrl.Replace(catchallParam, "0").ToAppRelativeUrl();
                        var urlWithCatchall = string.Format(urlFormat, catchallValue);
                        urlWithCatchall.WithAnyMethod().ShouldMapTo(action);

                        var urlWithTrailingSlash = string.Format(urlFormat, string.Empty);
                        urlWithTrailingSlash.WithAnyMethod().ShouldMapTo(action);

                        var urlWithoutTrailingSlash = urlWithTrailingSlash.WithoutTrailingSlash();
                        urlWithoutTrailingSlash.WithAnyMethod().ShouldMapTo(action);
                    }
                    else if (routeUrl.Contains(prefixParam))
                    {
                        var urlFormat = routeUrl.Replace(prefixParam, "0").ToAppRelativeUrl();
                        var url = string.Format(urlFormat, prefixValue);
                        url.WithAnyMethod().ShouldMapTo(action);
                    }
                    else
                    {
                        var url = routeUrl.ToAppRelativeUrl();
                        url.WithAnyMethod().ShouldMapTo(action);
                    }
                }
            }
        }
    }
}
