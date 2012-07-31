using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public static class UpdatePasswordRouterFacts
    {
        private static readonly string AreaName = MVC.Identity.Name;

        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToAction()
            {
                FormatRoute().WithMethod(HttpVerbs.Get)
                    .ShouldMapTo(Action());
            }

            [TestMethod]
            public void Outbound_WithGet_MapsToUrl()
            {
                OutBoundRoute.Of(Action()).InArea(AreaName)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldEqual(FormatRoute());
            }

            private static readonly string Route = new UpdatePasswordRouter.GetRoute().Url;

            private static Expression<Func<UpdatePasswordController, ActionResult>> Action()
            {
                Expression<Func<UpdatePasswordController, ActionResult>> action =
                    controller => controller.Get();
                return action;
            }

            private static string FormatRoute()
            {
                var route = Route.ToAppRelativeUrl().WithoutTrailingSlash();
                return route;
            }
        }

        [TestClass]
        public class ThePostRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToAction()
            {
                FormatRoute().WithMethod(HttpVerbs.Post)
                    .ShouldMapTo(Action);
            }

            [TestMethod]
            public void Outbound_WithPost_MapsToUrl()
            {
                OutBoundRoute.Of(Action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(FormatRoute());
            }

            [TestMethod]
            public void Inbound_WithNonGetOrPost_MapsToNothing()
            {
                FormatRoute().WithMethodsExcept(HttpVerbs.Post, HttpVerbs.Get)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithPut_MapsToNothing()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Put)
                    .AppRelativeUrl().ShouldBeNull();
            }

            private static readonly string Route = new UpdatePasswordRouter.PostRoute().Url;
            private static readonly Expression<Func<UpdatePasswordController, ActionResult>>
                Action = controller => controller.Post(null);

            private static string FormatRoute()
            {
                return Route.ToAppRelativeUrl().WithoutTrailingSlash();
            }
        }

        [TestClass]
        public class TheValidateCurrentPasswordRoute
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
                    .WithMethod(HttpVerbs.Post).AppRelativeUrl().ShouldEqual(Url);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                Url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithGet_MapsToNothing()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldBeNull();
            }

            private static readonly Expression<Func<UpdatePasswordController, ActionResult>>
                Action = controller => controller.ValidateCurrentPassword(null);

            private static readonly string Url =
                new UpdatePasswordRouter.ValidateCurrentPasswordRoute().Url.ToAppRelativeUrl();
        }

        [TestClass]
        public class TheValidateNewPasswordConfirmationRoute
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
                    .WithMethod(HttpVerbs.Post).AppRelativeUrl().ShouldEqual(Url);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                Url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithGet_MapsToNothing()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldBeNull();
            }

            private static readonly Expression<Func<UpdatePasswordController, ActionResult>>
                Action = controller => controller.ValidateNewPasswordConfirmation(null);

            private static readonly string Url =
                new UpdatePasswordRouter.ValidateNewPasswordConfirmationRoute().Url.ToAppRelativeUrl();
        }
    }
}
