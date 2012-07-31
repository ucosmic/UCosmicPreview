using System;
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
                var route1 = new MyHomeRouter.GetRoute();
                var route2 = new MyHomeRouter.GetMyRoute();
                route1.Url.ShouldEqual("my/home");
                route2.Url.ShouldEqual("my");
            }

            [TestMethod]
            public void Inbound_WithGet_MapsToGetAction()
            {
                Expression<Func<MyHomeController, ActionResult>> action =
                    controller => controller.Get();
                var url = MyHomeRouter.GetRoute.MyHomeUrl.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGet_MapsToNothing()
            {
                var url = MyHomeRouter.GetRoute.MyHomeUrl.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_MapsToUrl()
            {
                Expression<Func<MyHomeController, ActionResult>> action =
                    controller => controller.Get();
                var url = MyHomeRouter.GetRoute.MyHomeUrl.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldEqual(url);
            }
        }
    }
}
