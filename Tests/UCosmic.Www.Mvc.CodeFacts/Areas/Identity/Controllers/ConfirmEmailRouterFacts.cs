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
    public class ConfirmEmailRouterFacts
    // ReSharper restore UnusedMember.Global
    {
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
                OutBoundRoute.Of(Action(Guid.Empty)).InArea(MVC.Identity.Name)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldBeNull();
            }

            [TestMethod]
            public void Inbound_WithGet_AndGuidToken_AndNullSecretCode_MapsToAction()
            {
                var token = Guid.NewGuid();
                FormatRoute(token).WithMethod(HttpVerbs.Get)
                    .ShouldMapTo(Action(token));
            }

            [TestMethod]
            public void Outbound_WithGet_AndGuidToken_AndNullSecretCode_MapsToUrl()
            {
                var token = Guid.NewGuid();
                OutBoundRoute.Of(Action(token)).InArea(MVC.Identity.Name)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldEqual(FormatRoute(token));
            }

            [TestMethod]
            public void Inbound_WithGet_AndGuidToken_AndNonNullSecretCode_MapsToAction()
            {
                var token = Guid.NewGuid();
                const string secretCode = "secret";
                FormatRoute(token, secretCode).WithMethod(HttpVerbs.Get)
                    .ShouldMapTo(Action(token, secretCode));
            }

            [TestMethod]
            public void Outbound_WithGet_AndGuidToken_AndNonNullSecretCode_MapsToUrl()
            {
                var token = Guid.NewGuid();
                const string secretCode = "secret";
                OutBoundRoute.Of(Action(token, secretCode)).InArea(MVC.Identity.Name)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl()
                    .ShouldEqual(FormatRoute(token, secretCode));
            }

            [TestMethod]
            public void Inbound_WithNonGet_AndGuidToken_AndNonNullSecretCode_MapsToNothing()
            {
                var token = Guid.NewGuid();
                const string secretCode = "secret";
                FormatRoute(token, secretCode).WithMethodsExcept(HttpVerbs.Get)
                    .ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_WithPost_AndGuidToken_AndNonNullSecretCode_MapsToNothing()
            {
                var token = Guid.NewGuid();
                const string secretCode = "secret";
                OutBoundRoute.Of(Action(token, secretCode)).InArea(MVC.Identity.Name)
                    .WithMethod(HttpVerbs.Post).AppRelativeUrl()
                    .ShouldBeNull();
            }

            [TestMethod]
            public void Outbound_WithPut_AndGuidToken_AndNonNullSecretCode_MapsToNothing()
            {
                var token = Guid.NewGuid();
                const string secretCode = "secret";
                OutBoundRoute.Of(Action(token, secretCode))
                    .InArea(MVC.Identity.Name).WithMethod(HttpVerbs.Put)
                    .AppRelativeUrl()
                    .ShouldBeNull();
            }

            [TestMethod]
            public void Defaults_WithGet_AndGuidToken_AndNonNullSecretCode_MapToNothing()
            {
                var token = Guid.NewGuid();
                const string secretCode = "secret";
                Action(token, secretCode).DefaultAreaRoutes(MVC.Identity.Name)
                    .ShouldMapToNothing();
            }

            private const string Route = ConfirmEmailRouter.Get.Route;
            private const string TokenParam = "{token}";
            private const string SecretCodeParam = "{secretCode}";

            private static Expression<Func<ConfirmEmailController, ActionResult>> Action(Guid token, string secretCode = null)
            {
                Expression<Func<ConfirmEmailController, ActionResult>> action =
                    controller => controller.Get(token, secretCode);
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
                OutBoundRoute.Of(Action).InArea(MVC.Identity.Name)
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
                OutBoundRoute.Of(Action).InArea(MVC.Identity.Name).WithMethod(HttpVerbs.Post)
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
                OutBoundRoute.Of(Action).InArea(MVC.Identity.Name)
                    .WithMethod(HttpVerbs.Put)
                    .HavingViewModelProperty("token", Guid.NewGuid())
                    .AppRelativeUrl().ShouldBeNull();
            }

            [TestMethod]
            public void Defaults_WithPostAction_MapToNothing()
            {
                Action.DefaultAreaRoutes(MVC.Identity.Name).ShouldMapToNothing();
            }

            private const string Route = ConfirmEmailRouter.Post.Route;
            private const string TokenParam = "{token}";
            private static readonly Expression<Func<ConfirmEmailController, ActionResult>> 
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
        public class TheValidateSecretCodeRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToAction()
            {
                Url.WithMethod(HttpVerbs.Post).ShouldMapTo(Action);
            }

            [TestMethod]
            public void Outbound_WithPost_MapsToUrl()
            {
                OutBoundRoute.Of(Action).InArea(MVC.Identity.Name)
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
                OutBoundRoute.Of(Action).InArea(MVC.Identity.Name)
                    .WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldBeNull();
            }

            private static readonly Expression<Func<ConfirmEmailController, ActionResult>> 
                Action = controller => controller.ValidateSecretCode(null);

            private static readonly string Url =
                ConfirmEmailRouter.ValidateSecretCode.Route.ToAppRelativeUrl();
        }
    }
}
