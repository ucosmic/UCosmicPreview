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
    public class SkinsRouteMapperFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class Change
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                const string skinContext = "skin-context";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Change(skinContext, null);
                var url = SkinsRouteMapper.Change.Route.ToAppRelativeUrl()
                    .Replace("{skinContext}", skinContext);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                const string skinContext = "skin-context";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Change(skinContext, null);
                var url = SkinsRouteMapper.Change.Route.ToAppRelativeUrl()
                    .Replace("{skinContext}", skinContext);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethodIsNotRouted()
            {
                const string skinContext = "skin-context";
                var url = SkinsRouteMapper.Change.Route.ToAppRelativeUrl()
                    .Replace("{skinContext}", skinContext);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNullCatchall()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Change("skin-context", null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Sample
        {
            [TestMethod]
            public void Maps2Urls_FirstWithParam_ThenWithout()
            {
                SkinsRouteMapper.Sample.Routes.ShouldNotBeNull();
                SkinsRouteMapper.Sample.Routes.Length.ShouldEqual(2);
                SkinsRouteMapper.Sample.Routes[0].ShouldContain("/{");
                SkinsRouteMapper.Sample.Routes[1].ShouldNotContain("/{");
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNullArg_IsRoutedTo_UrlWithoutParam()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(null);
                var url = SkinsRouteMapper.Sample.Routes[1].ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithEmptyArg_IsRoutedTo_UrlWithoutParam()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(string.Empty);
                var url = SkinsRouteMapper.Sample.Routes[1].ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNonEmptyArg_IsRoutedTo_UrlWithParam()
            {
                const string content = "sample-content";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(content);
                var url = SkinsRouteMapper.Sample.Routes[0].ToAppRelativeUrl()
                    .Replace("{content}", content);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndNoParam_IsRoutedTo_ActionWithNullArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(null);
                var url = SkinsRouteMapper.Sample.Routes[1].ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNoParam_IsNotRouted()
            {
                var url = SkinsRouteMapper.Sample.Routes[1].ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndNonEmptyParam_IsRoutedTo_ActionWithNonEmptyArg()
            {
                const string content = "sample-content";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(content);
                var url = SkinsRouteMapper.Sample.Routes[0].ToAppRelativeUrl()
                    .Replace("{content}", content);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNonEmptyParam_IsNotRouted()
            {
                const string content = "sample-content";
                var url = SkinsRouteMapper.Sample.Routes[0].ToAppRelativeUrl()
                    .Replace("{content}", content);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNullArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithEmptyArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(string.Empty);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNonEmptyArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample("sample-content");
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Apply
        {
            [TestMethod]
            public void OutBoundUrl_OfActionWithWithNullArg_IsRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply(null);
                var url = SkinsRouteMapper.Apply.Route.ToAppRelativeUrl()
                    .Replace("/{skinFile}", string.Empty);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithWithNonNullArg_IsRouted()
            {
                const string skinFile = "skin-file";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply(skinFile);
                var url = SkinsRouteMapper.Apply.Route.ToAppRelativeUrl()
                    .Replace("{skinFile}", skinFile);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndEmptyArg_IsRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply(string.Empty);
                var url = SkinsRouteMapper.Apply.Route.ToAppRelativeUrl()
                    .Replace("/{skinFile}", string.Empty);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNonNullArg_IsNotRouted()
            {
                var url = SkinsRouteMapper.Apply.Route.ToAppRelativeUrl()
                    .Replace("/{skinFile}", string.Empty);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNullArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply(null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted_ForActionWithNonNullArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply("skin-file");
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Logo
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Logo();
                var url = SkinsRouteMapper.Logo.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Logo();
                var url = SkinsRouteMapper.Logo.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = SkinsRouteMapper.Logo.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Logo();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }
    }
}
