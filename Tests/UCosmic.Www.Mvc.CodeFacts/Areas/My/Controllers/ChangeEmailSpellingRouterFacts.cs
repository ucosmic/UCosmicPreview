using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ChangeEmailSpellingRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.Get.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithGet_AndPositiveNumber_MapsToGetAction()
            {
                const int number = 1;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Get(number);
                const string routeUrl = ChangeEmailSpellingRouter.Get.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGetPostOrPut_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.Get.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithNonGetPostOrPut_AndPositiveNumber_MapsToNothing()
            {
                const int number = 4;
                const string routeUrl = ChangeEmailSpellingRouter.Get.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_WithNumber0_MapsToUrl()
            {
                const int number = 0;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Get(number);

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldBeNull();
            }

            [TestMethod]
            public void Outbound_ForGetAction_AndPositiveNumber_MapsToUrl()
            {
                const int number = 5;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Get(number);
                const string routeUrl = ChangeEmailSpellingRouter.Get.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_ForGetAction_AndNumber0_MapToNothing()
            {
                const int number = 0;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Get(number);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

            [TestMethod]
            public void Defaults_ForGetAction_AndPositiveNumber_MapToNothing()
            {
                const int number = 7;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Get(number);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class ThePutRoute
        {
            [TestMethod]
            public void Inbound_WithPut_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.Put.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPut_AndPositiveNumber_MapsToPutAction()
            {
                const int number = 2;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Put(null);
                const string routeUrl = ChangeEmailSpellingRouter.Put.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithPost_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.Put.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPost_AndPositiveNumber_MapsToPutAction()
            {
                const int number = 3;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Put(null);
                const string routeUrl = ChangeEmailSpellingRouter.Put.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Outbound_ForPutAction_WithNumber0_MapsToNothing()
            {
                const int number = 0;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Put(null);
                const string numberParam = "number";

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty(numberParam, number).AppRelativeUrl().ShouldBeNull();
            }

            [TestMethod]
            public void Outbound_ForPutAction_AndPositiveNumber_MapsToUrl()
            {
                const int number = 6;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Put(null);
                const string routeUrl = ChangeEmailSpellingRouter.Put.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty(numberParam, number).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPutAction_MapToNothing()
            {
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.Put(null);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class TheValidateValueRoute
        {
            [TestMethod]
            public void Inbound_WithGet_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithGet_AndPositiveNumber_MapsToNothing()
            {
                const int number = 1;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPut_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPut_AndPositiveNumber_MapsToNothing()
            {
                const int number = 2;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPost_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPost_AndPositiveNumber_MapsToPostAction()
            {
                const int number = 3;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.ValidateValue(null);
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_AndNumber0_MapsToNothing()
            {
                const int number = 0;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithNonPost_AndPositiveNumber_MapsToNothing()
            {
                const int number = 4;
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_WithNumber0_MapsToNothing()
            {
                const int number = 0;
                const string numberParam = "number";
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.ValidateValue(null);

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty(numberParam, number).AppRelativeUrl().ShouldBeNull();
            }

            [TestMethod]
            public void Outbound_ForPostAction_AndPositiveNumber_MapsToUrl()
            {
                const int number = 6;
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.ValidateValue(null);
                const string routeUrl = ChangeEmailSpellingRouter.ValidateValue.Route;
                const string numberParam = "number";
                var urlFormat = routeUrl.Replace(numberParam, "0");
                var url = string.Format(urlFormat, number).ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty(numberParam, number).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPostAction_MapToNothing()
            {
                Expression<Func<ChangeEmailSpellingController, ActionResult>> action =
                    controller => controller.ValidateValue(null);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }
    }
}
