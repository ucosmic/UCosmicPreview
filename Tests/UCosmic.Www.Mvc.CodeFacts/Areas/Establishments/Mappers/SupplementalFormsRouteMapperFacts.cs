using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.Establishments.Controllers;

namespace UCosmic.Www.Mvc.Areas.Establishments.Mappers
{
    public static class SupplementalFormsRouteMapperFacts
    {
        private static readonly string Area = MVC.Establishments.Name;

        [TestClass]
        public class FindPlaces
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                const double latitude = 1.1;
                const double longitude = -1.1;
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.FindPlaces(latitude, longitude);
                var url = SupplementalFormsRouteMapper.FindPlaces.Route.ToAppRelativeUrl()
                    .AddQueryString("?latitude={0}&longitude={1}", latitude, longitude);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                const double latitude = 1.1;
                const double longitude = -1.1;
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.FindPlaces(latitude, longitude);
                var url = SupplementalFormsRouteMapper.FindPlaces.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).AndMethodArg("latitude", latitude)
                    .AndMethodArg("longitude", longitude).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = SupplementalFormsRouteMapper.FindPlaces.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Locate_Get
        {
            [TestMethod]
            public void Maps2Urls_FirstWithReturnUrl_ThenWithout()
            {
                SupplementalFormsRouteMapper.Locate.RoutesForGet.ShouldNotBeNull();
                SupplementalFormsRouteMapper.Locate.RoutesForGet.Length.ShouldEqual(2);
                SupplementalFormsRouteMapper.Locate.RoutesForGet[0].ShouldContain("{*");
                SupplementalFormsRouteMapper.Locate.RoutesForGet[1].ShouldNotContain("{*");
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNullReturnUrl_IsRoutedTo_UrlWithoutCatchall()
            {
                var establishmentId = Guid.NewGuid();
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(establishmentId, null);
                var url = SupplementalFormsRouteMapper.Locate.RoutesForGet[1].ToAppRelativeUrl()
                    .Replace("{establishmentId}", establishmentId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNonNullReturnUrl_IsRoutedTo_UrlWithCatchall()
            {
                var establishmentId = Guid.NewGuid();
                const string attemptedUrl = "path/to/action";
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(establishmentId, attemptedUrl);
                var url = SupplementalFormsRouteMapper.Locate.RoutesForGet[0].ToAppRelativeUrl()
                    .Replace("{*returnUrl}", attemptedUrl)
                    .Replace("{establishmentId}", establishmentId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndNoCatchall_IsRoutedTo_ActionWithNullArg()
            {
                var establishmentId = Guid.NewGuid();
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(establishmentId, null);
                var url = SupplementalFormsRouteMapper.Locate.RoutesForGet[1].ToAppRelativeUrl()
                    .Replace("{establishmentId}", establishmentId.ToString());
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNoCatchall_IsNotRouted()
            {
                var url = SupplementalFormsRouteMapper.Locate.RoutesForGet[1].ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndCatchall_IsRoutedTo_ActionWithCatchallArg()
            {
                var establishmentId = Guid.NewGuid();
                const string attemptedUrl = "path/to/action";
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(establishmentId, attemptedUrl);
                var url = SupplementalFormsRouteMapper.Locate.RoutesForGet[0].ToAppRelativeUrl()
                    .Replace("{*returnUrl}", attemptedUrl)
                    .Replace("{establishmentId}", establishmentId.ToString());
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndCatchall_IsNotRouted()
            {
                var establishmentId = Guid.NewGuid();
                const string attemptedUrl = "path/to/action";
                var url = SupplementalFormsRouteMapper.Locate.RoutesForGet[0].ToAppRelativeUrl()
                    .Replace("{*returnUrl}", attemptedUrl)
                    .Replace("{establishmentId}", establishmentId.ToString());
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Locate_Post
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(null);
                var url = SupplementalFormsRouteMapper.Locate.RouteForPost.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithPostMethod_IsRouted()
            {
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(null);
                var url = SupplementalFormsRouteMapper.Locate.RouteForPost.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonPostMethod_IsNotRouted()
            {
                var url = SupplementalFormsRouteMapper.Locate.RouteForPost.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }
        }
    }
}
