using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ResetPasswordRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string AreaName = MVC.Identity.Name;

        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_AndEmptyToken_MapsToNothing()
            {
                FormatRoute(Guid.Empty).WithMethod(HttpVerbs.Get)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithGet_AndEmptyToken_MapsToNothing()
            {
                OutBoundRoute.Of(Action(Guid.Empty)).InArea(AreaName)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldBeNull();
            }

            [TestMethod]
            public void Inbound_WithGet_AndGuidToken_MapsToAction()
            {
                var token = Guid.NewGuid();
                FormatRoute(token).WithMethod(HttpVerbs.Get)
                    .ShouldMapTo(Action(token));
            }

            [TestMethod]
            public void Outbound_WithGet_AndGuidToken_MapsToUrl()
            {
                var token = Guid.NewGuid();
                OutBoundRoute.Of(Action(token)).InArea(AreaName)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldEqual(FormatRoute(token));
            }

            [TestMethod]
            public void Inbound_WithNonGetOrPost_AndGuidToken_MapsToNothing()
            {
                var token = Guid.NewGuid();
                FormatRoute(token).WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithPut_AndGuidToken_AndNonNullSecretCode_MapsToNothing()
            {
                var token = Guid.NewGuid();
                OutBoundRoute.Of(Action(token)).InArea(AreaName)
                    .WithMethod(HttpVerbs.Put).AppRelativeUrl()
                    .ShouldBeNull();
            }

            private const string Route = ResetPasswordRouter.Get.Route;
            private const string TokenParam = "{token}";

            private static Expression<Func<ResetPasswordController, ActionResult>> Action(Guid token)
            {
                Expression<Func<ResetPasswordController, ActionResult>> action =
                    controller => controller.Get(token);
                return action;
            }

            private static string FormatRoute(Guid token)
            {
                var parameters = new Dictionary<string, string>
                {
                    { TokenParam, token.ToString() },
                };

                return Route.FormatTemplate(parameters)
                    .ToAppRelativeUrl();
            }
        }

        [TestClass]
        public class ThePostRoute
        {
            [TestMethod]
            public void Inbound_WithPost_AndEmptyToken_MapsToNothing()
            {
                FormatRoute(Guid.Empty).WithMethod(HttpVerbs.Post)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithPost_AndEmptyToken_MapsToNothing()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Post).AppRelativeUrl()
                    .ShouldBeNull();
            }

            [TestMethod]
            public void Inbound_WithPost_AndGuidToken_MapsToAction()
            {
                FormatRoute(Guid.NewGuid()).WithMethod(HttpVerbs.Post)
                    .ShouldMapTo(Action);
            }

            [TestMethod]
            public void Outbound_WithPost_AndGuidToken_MapsToUrl()
            {
                var token = Guid.NewGuid();
                OutBoundRoute.Of(Action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty("token", token)
                    .AppRelativeUrl().ShouldEqual(FormatRoute(token));
            }

            [TestMethod]
            public void Inbound_WithNonGetOrPost_AndGuidToken_MapsToNothing()
            {
                FormatRoute(Guid.NewGuid()).WithMethodsExcept(HttpVerbs.Post, HttpVerbs.Get)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithDelete_AndEmptyToken_MapsToNothing()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Delete)
                    .HavingViewModelProperty("token", Guid.NewGuid())
                    .AppRelativeUrl().ShouldBeNull();
            }

            private const string Route = ResetPasswordRouter.Post.Route;
            private const string TokenParam = "{token}";
            private static readonly Expression<Func<ResetPasswordController, ActionResult>>
                Action = controller => controller.Post(null);

            private static string FormatRoute(Guid? token)
            {
                var parameters = new Dictionary<string, string>();

                if (token.HasValue)
                    parameters.Add(TokenParam, token.Value.ToString());

                return Route.FormatTemplate(parameters)
                    .ToAppRelativeUrl();
            }
        }

        [TestClass]
        public class TheValidatePasswordConfirmationRoute
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

            private static readonly Expression<Func<ResetPasswordController, ActionResult>>
                Action = controller => controller.ValidatePasswordConfirmation(null);

            private static readonly string Url =
                ResetPasswordRouter.ValidatePasswordConfirmation.Route.ToAppRelativeUrl();
        }
    }
}
