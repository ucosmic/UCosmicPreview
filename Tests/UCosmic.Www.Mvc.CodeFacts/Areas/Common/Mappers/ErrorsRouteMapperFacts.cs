using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.Common.Controllers;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public class ErrorsRouteMapperFacts
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
                var url = ErrorsRouteMapper.NotFound.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.NotFound();
                var url = ErrorsRouteMapper.NotFound.Route.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.NotFound();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = ErrorsRouteMapper.FileUploadTooLarge.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.FileUploadTooLarge(null);
                var url = ErrorsRouteMapper.FileUploadTooLarge.Route.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.FileUploadTooLarge(null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class NotAuthorized
        {
            [TestMethod]
            public void Maps2Urls_FirstWithCatchall_ThenWithout()
            {
                ErrorsRouteMapper.NotAuthorized.Routes.ShouldNotBeNull();
                ErrorsRouteMapper.NotAuthorized.Routes.Length.ShouldEqual(2);
                ErrorsRouteMapper.NotAuthorized.Routes[0].ShouldContain("{*");
                ErrorsRouteMapper.NotAuthorized.Routes[1].ShouldNotContain("{*");
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNullArg_IsRoutedTo_UrlWithoutCatchall()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(null);
                var url = ErrorsRouteMapper.NotAuthorized.Routes[1].ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithCatchallArg_IsRoutedTo_UrlWithCatchall()
            {
                const string attemptedUrl = "path/to/action";
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(attemptedUrl);
                var url = ErrorsRouteMapper.NotAuthorized.Routes[0].ToAppRelativeUrl()
                    .Replace("{*url}", attemptedUrl);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_AndNoCatchall_IsRoutedTo_ActionWithNullArg()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(null);
                var url = ErrorsRouteMapper.NotAuthorized.Routes[1].ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_AndCatchall_IsRoutedTo_ActionWithCatchallArg()
            {
                const string attemptedUrl = "path/to/action";
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(attemptedUrl);
                var url = ErrorsRouteMapper.NotAuthorized.Routes[0].ToAppRelativeUrl()
                    .Replace("{*url}", attemptedUrl);
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNullArg()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNonNullArg()
            {
                const string attemptedUrl = "path/to/action";
                Expression<Func<ErrorsController, ActionResult>> action =
                   controller => controller.NotAuthorized(attemptedUrl);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = ErrorsRouteMapper.BadRequest.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.BadRequest();
                var url = ErrorsRouteMapper.BadRequest.Route.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.BadRequest();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = ErrorsRouteMapper.Unexpected.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithAnyMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Unexpected();
                var url = ErrorsRouteMapper.Unexpected.Route.ToAppRelativeUrl();
                url.WithAnyMethod().ShouldMapTo(action);
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Unexpected();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = ErrorsRouteMapper.Throw.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Throw();
                var url = ErrorsRouteMapper.Throw.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = ErrorsRouteMapper.Throw.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.Throw();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = ErrorsRouteMapper.LogAjaxError.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.LogAjaxError(null);
                var url = ErrorsRouteMapper.LogAjaxError.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = ErrorsRouteMapper.LogAjaxError.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ErrorsController, ActionResult>> action =
                    controller => controller.LogAjaxError(null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var routeUrls = ErrorsRouteMapper.NotFoundByHackerSniff.Routes;

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
