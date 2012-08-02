using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public static class MyHomeRouterFacts
    {
        private static readonly string AreaName = MVC.Identity.Name;

        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Defines_Fallback_Url()
            {
                var route = new MyHomeRouter.GetRoute();
                route.Url.ShouldEqual("my/home");
                route.AlternateUrls.ShouldNotBeNull();
                route.AlternateUrls.Count().ShouldEqual(1);
                route.AlternateUrls.Single().ShouldEqual("my");
            }

            [TestMethod]
            public void Inbound_WithGet_MapsToGetAction()
            {
                Expression<Func<MyHomeController, ActionResult>> action =
                    controller => controller.Get();
                var url = MyHomeRouter.GetRoute.UrlConstant.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGet_MapsToNothing()
            {
                var url = MyHomeRouter.GetRoute.UrlConstant.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_MapsToUrl()
            {
                Expression<Func<MyHomeController, ActionResult>> action =
                    controller => controller.Get();
                var url = MyHomeRouter.GetRoute.UrlConstant.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldEqual(url);
            }
        }
    }
}
