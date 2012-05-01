using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ForgotPasswordRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string AreaName = MVC.Passwords.Name;

        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToGetAction()
            {
                Url.WithMethod(HttpVerbs.Get).ShouldMapTo(Action);
            }

            [TestMethod]
            public void Inbound_WithNonGetOrPost_MapsToNothing()
            {
                Url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithGet_MapsToUrl()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldEqual(Url);
            }

            private static readonly string Url = ForgotPasswordRouter.Get.Route.ToAppRelativeUrl();

            private static readonly Expression<Func<ForgotPasswordController, ActionResult>>
                Action = controller => controller.Get();
        }

        [TestClass]
        public class ThePostRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToAction()
            {
                Url.WithMethod(HttpVerbs.Post).ShouldMapTo(Action);
            }

            [TestMethod]
            public void Outbound_WithPost_MapsToUrl()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Post).AppRelativeUrl()
                    .ShouldEqual(Url);
            }

            private static readonly string Url = ForgotPasswordRouter.Post.Route.ToAppRelativeUrl();

            private static readonly Expression<Func<ForgotPasswordController, ActionResult>>
                Action = controller => controller.Post(null);
        }

        [TestClass]
        public class TheValidateEmailAddressRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Url.WithMethod(HttpVerbs.Post).ShouldMapTo(Action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                Url.WithMethodsExcept(HttpVerbs.Post)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithPost_MapsToUrl()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Post).AppRelativeUrl()
                    .ShouldEqual(Url);
            }

            private static readonly string Url = ForgotPasswordRouter.ValidateEmailAddress.Route
                .ToAppRelativeUrl();

            private static readonly Expression<Func<ForgotPasswordController, ActionResult>>
                Action = controller => controller.ValidateEmailAddress(null);
        }
    }
}
