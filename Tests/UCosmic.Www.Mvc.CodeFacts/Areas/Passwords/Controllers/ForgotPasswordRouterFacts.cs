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
        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToGetAction()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.Get();
                var url = ForgotPasswordRouter.Get.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGetOrPost_MapsToNothing()
            {
                var url = ForgotPasswordRouter.Get.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_MapsToUrl()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.Get();
                var url = ForgotPasswordRouter.Get.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.Passwords.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_ForGetAction_MapToNothing()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.Get();

                action.DefaultAreaRoutes(MVC.Passwords.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class ThePostRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToAction()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.Post(null);
                var url = ForgotPasswordRouter.Post.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.Post(null);
                var url = ForgotPasswordRouter.Post.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.Passwords.Name).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPostAction_MapToNothing()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.Post(null);

                action.DefaultAreaRoutes(MVC.Passwords.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class TheValidateEmailAddressRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.ValidateEmailAddress(null);
                var url = ForgotPasswordRouter.ValidateEmailAddress.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = ForgotPasswordRouter.ValidateEmailAddress.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.ValidateEmailAddress(null);
                var url = ForgotPasswordRouter.ValidateEmailAddress.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.Passwords.Name).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPostAction_MapToNothing()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> action =
                    controller => controller.ValidateEmailAddress(null);

                action.DefaultAreaRoutes(MVC.Passwords.Name).ShouldMapToNothing();
            }

        }
    }
}
