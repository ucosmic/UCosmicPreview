using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ProfileRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToGetAction()
            {
                Expression<Func<ProfileController, ActionResult>> action =
                    controller => controller.Get();
                var url = ProfileRouter.Get.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGet_MapsToNothing()
            {
                var url = ProfileRouter.Get.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_MapsToUrl()
            {
                Expression<Func<ProfileController, ActionResult>> action =
                    controller => controller.Get();
                var url = ProfileRouter.Get.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_ForGetAction_MapToNothing()
            {
                Expression<Func<ProfileController, ActionResult>> action =
                    controller => controller.Get();

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }
    }
}
