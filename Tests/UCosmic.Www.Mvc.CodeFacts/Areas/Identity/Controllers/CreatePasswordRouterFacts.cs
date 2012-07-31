using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public static class CreatePasswordRouterFacts
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

            private static readonly string Route = new CreatePasswordRouter.GetRoute().Url;
            private const string TokenParam = "{token}";
            private const string SecretCodeParam = "{secretCode}";

            private static Expression<Func<CreatePasswordController, ActionResult>> Action(Guid token)
            {
                Expression<Func<CreatePasswordController, ActionResult>> action =
                    controller => controller.Get(token);
                return action;
            }

            private static string FormatRoute(Guid token, string secretCode = null)
            {
                var parameters = new Dictionary<string, string>
                {
                    { TokenParam, token.ToString() },
                    { SecretCodeParam, secretCode },
                };

                return Route.FormatTemplate(parameters)
                    .ToAppRelativeUrl().WithoutTrailingSlash();
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
            public void Outbound_WithPut_AndEmptyToken_MapsToNothing()
            {
                OutBoundRoute.Of(Action).InArea(AreaName)
                    .WithMethod(HttpVerbs.Put)
                    .HavingViewModelProperty("token", Guid.NewGuid())
                    .AppRelativeUrl().ShouldBeNull();
            }

            private static readonly string Route = new CreatePasswordRouter.PostRoute().Url;
            private const string TokenParam = "{token}";
            private static readonly Expression<Func<CreatePasswordController, ActionResult>>
                Action = controller => controller.Post(null);

            private static string FormatRoute(Guid? token)
            {
                var parameters = new Dictionary<string, string>();

                if (token.HasValue)
                    parameters.Add(TokenParam, token.Value.ToString());

                return Route.FormatTemplate(parameters)
                    .ToAppRelativeUrl().WithoutTrailingSlash();
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

            private static readonly Expression<Func<CreatePasswordController, ActionResult>>
                Action = controller => controller.ValidatePasswordConfirmation(null);

            private static readonly string Url =
                new CreatePasswordRouter.ValidatePasswordConfirmationRoute().Url.ToAppRelativeUrl();
        }
    }
}
