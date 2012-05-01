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
    public class FeaturesRouteMapperFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class Releases
        {
            [TestMethod]
            public void Maps2Urls_SiteRoot_Features_OptionalVersion()
            {
                FeaturesRouteMapper.Releases.Routes.ShouldNotBeNull();
                FeaturesRouteMapper.Releases.Routes.Length.ShouldEqual(2);
                FeaturesRouteMapper.Releases.Routes[0].ShouldEqual(string.Empty);
            }

            [TestMethod]
            public void OutBoundUrl_OfAction_WithCurrentVersionArg_IsRoutedTo_SiteRoot()
            {
                const string currentVersion = FeaturesRouteMapper.Releases.CurrentVersion;
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.Releases(currentVersion);
                var url = FeaturesRouteMapper.Releases.Routes[0].ToAppRelativeUrl();

                url.ShouldEqual("~/");
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndCurrentVersion_IsRoutedTo_SiteRoot()
            {
                const string currentVersion = FeaturesRouteMapper.Releases.CurrentVersion;
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.Releases(currentVersion);
                var url = FeaturesRouteMapper.Releases.Routes[0].ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndCurrentVersion_IsNotRouted()
            {
                var url = FeaturesRouteMapper.Releases.Routes[0].ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void OutBoundUrl_OfAction_WithNullArg_IsRoutedTo_Features()
            {
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.Releases(null);
                var url = FeaturesRouteMapper.Releases.Routes[1]
                    .Replace("/{version}", string.Empty).ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNullVersion_IsNotRouted()
            {
                var url = FeaturesRouteMapper.Releases.Routes[1].ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void OutBoundUrl_OfAction_WithNonCurrentVersionArg_IsRoutedTo_Features()
            {
                const string otherVersion = "version";
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.Releases(otherVersion);
                var url = FeaturesRouteMapper.Releases.Routes[1].ToAppRelativeUrl();
                url = url.Replace("{version}", otherVersion);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndNonCurrentVersion_IsRoutedTo_Features()
            {
                const string otherVersion = "version";
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.Releases(otherVersion);
                var url = FeaturesRouteMapper.Releases.Routes[1].ToAppRelativeUrl();
                url = url.Replace("{version}", otherVersion);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNonCurrentVersion_IsNotRouted()
            {
                const string otherVersion = "version";
                var url = FeaturesRouteMapper.Releases.Routes[1].ToAppRelativeUrl();
                url = url.Replace("{version}", otherVersion);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class ReleasesNav
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.ReleasesNav();
                var url = FeaturesRouteMapper.ReleasesNav.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<FeaturesController, ActionResult>> action =
                    controller => controller.ReleasesNav();
                var url = FeaturesRouteMapper.ReleasesNav.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = FeaturesRouteMapper.ReleasesNav.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }
    }
}
