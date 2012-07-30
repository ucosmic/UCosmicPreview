using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public static class NavigationRouterFacts
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class HorizontalTabs
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<NavigationController, ActionResult>> action =
                    controller => controller.HorizontalTabs();
                var url = new NavigationRouter.HorizontalTabsRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<NavigationController, ActionResult>> action =
                    controller => controller.HorizontalTabs();
                var url = new NavigationRouter.HorizontalTabsRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new NavigationRouter.HorizontalTabsRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }
    }
}
