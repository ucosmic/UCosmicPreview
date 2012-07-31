using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Establishments.Controllers
{
    public static class SupplementalFormsRouterFacts
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
                var url = new SupplementalFormsRouter.FindPlacesRoute().Url.ToAppRelativeUrl()
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
                var url = new SupplementalFormsRouter.FindPlacesRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).AndMethodArg("latitude", latitude)
                    .AndMethodArg("longitude", longitude).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new SupplementalFormsRouter.FindPlacesRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class LocateGet
        {
            [TestMethod]
            public void Maps2Urls_FirstWithReturnUrl_ThenWithout()
            {
                new SupplementalFormsRouter.LocateGetRoute();
                new SupplementalFormsRouter.LocateGetWithoutReturnUrlRoute();
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNullReturnUrl_IsRoutedTo_UrlWithoutCatchall()
            {
                var establishmentId = Guid.NewGuid();
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(establishmentId, null);
                var url = new SupplementalFormsRouter.LocateGetWithoutReturnUrlRoute().Url.ToAppRelativeUrl()
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
                var url = new SupplementalFormsRouter.LocateGetRoute().Url.ToAppRelativeUrl()
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
                var url = new SupplementalFormsRouter.LocateGetWithoutReturnUrlRoute().Url.ToAppRelativeUrl()
                    .Replace("{establishmentId}", establishmentId.ToString());
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNoCatchall_IsNotRouted()
            {
                var url = new SupplementalFormsRouter.LocateGetWithoutReturnUrlRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndCatchall_IsRoutedTo_ActionWithCatchallArg()
            {
                var establishmentId = Guid.NewGuid();
                const string attemptedUrl = "path/to/action";
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(establishmentId, attemptedUrl);
                var url = new SupplementalFormsRouter.LocateGetRoute().Url.ToAppRelativeUrl()
                    .Replace("{*returnUrl}", attemptedUrl)
                    .Replace("{establishmentId}", establishmentId.ToString());
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndCatchall_IsNotRouted()
            {
                var establishmentId = Guid.NewGuid();
                const string attemptedUrl = "path/to/action";
                var url = new SupplementalFormsRouter.LocateGetRoute().Url.ToAppRelativeUrl()
                    .Replace("{*returnUrl}", attemptedUrl)
                    .Replace("{establishmentId}", establishmentId.ToString());
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class LocatePost
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(null);
                var url = new SupplementalFormsRouter.LocatePostRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithPostMethod_IsRouted()
            {
                Expression<Func<SupplementalFormsController, ActionResult>> action =
                   controller => controller.Locate(null);
                var url = new SupplementalFormsRouter.LocatePostRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonPostMethod_IsNotRouted()
            {
                var url = new SupplementalFormsRouter.LocatePostRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }
        }
    }
}
