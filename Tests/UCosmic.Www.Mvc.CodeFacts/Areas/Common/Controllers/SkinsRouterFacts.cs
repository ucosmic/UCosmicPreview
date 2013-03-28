using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public static class SkinsRouterFacts
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
                var url = new SkinsRouter.ChangeRoute().Url.ToAppRelativeUrl()
                    .Replace("{*skinContext}", skinContext);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                const string skinContext = "skin-context";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Change(skinContext, null);
                var url = new SkinsRouter.ChangeRoute().Url.ToAppRelativeUrl()
                    .Replace("{*skinContext}", skinContext);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethodIsNotRouted()
            {
                const string skinContext = "skin-context";
                var url = new SkinsRouter.ChangeRoute().Url.ToAppRelativeUrl()
                    .Replace("{skinContext}", skinContext);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Sample
        {
            [TestMethod]
            public void Maps2Urls_FirstWithParam_ThenWithout()
            {
                var route = new SkinsRouter.SampleRoute();
                route.Url.ShouldEqual("skins/sample/{content}");
                route.AlternateUrls.ShouldNotBeNull();
                route.AlternateUrls.Count().ShouldEqual(1);
                route.AlternateUrls.Single().ShouldEqual("skins");
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNullArg_IsRoutedTo_UrlWithoutParam()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(null);
                var url = new SkinsRouter.SampleRoute().AlternateUrls.Single().ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithEmptyArg_IsRoutedTo_UrlWithoutParam()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(string.Empty);
                var url = new SkinsRouter.SampleRoute().AlternateUrls.Single().ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithNonEmptyArg_IsRoutedTo_UrlWithParam()
            {
                const string content = "sample-content";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(content);
                var url = new SkinsRouter.SampleRoute().Url.ToAppRelativeUrl()
                    .Replace("{content}", content);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndNoParam_IsRoutedTo_ActionWithNullArg()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(null);
                var url = new SkinsRouter.SampleRoute().AlternateUrls.Single().ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNoParam_IsNotRouted()
            {
                var url = new SkinsRouter.SampleRoute().AlternateUrls.Single().ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndNonEmptyParam_IsRoutedTo_ActionWithNonEmptyArg()
            {
                const string content = "sample-content";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Sample(content);
                var url = new SkinsRouter.SampleRoute().Url.ToAppRelativeUrl()
                    .Replace("{content}", content);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNonEmptyParam_IsNotRouted()
            {
                const string content = "sample-content";
                var url = new SkinsRouter.SampleRoute().Url.ToAppRelativeUrl()
                    .Replace("{content}", content);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
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
                var url = new SkinsRouter.ApplyRoute().Url.ToAppRelativeUrl()
                    .Replace("/{skinFile}", string.Empty);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void OutBoundUrl_OfActionWithWithNonNullArg_IsRouted()
            {
                const string skinFile = "skin-file";
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply(skinFile);
                var url = new SkinsRouter.ApplyRoute().Url.ToAppRelativeUrl()
                    .Replace("{skinFile}", skinFile);
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_AndEmptyArg_IsRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Apply(string.Empty);
                var url = new SkinsRouter.ApplyRoute().Url.ToAppRelativeUrl()
                    .Replace("/{skinFile}", string.Empty);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_AndNonNullArg_IsNotRouted()
            {
                var url = new SkinsRouter.ApplyRoute().Url.ToAppRelativeUrl()
                    .Replace("/{skinFile}", string.Empty);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
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
                var url = new SkinsRouter.LogoRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<SkinsController, ActionResult>> action =
                   controller => controller.Logo();
                var url = new SkinsRouter.LogoRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new SkinsRouter.LogoRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }
    }
}
