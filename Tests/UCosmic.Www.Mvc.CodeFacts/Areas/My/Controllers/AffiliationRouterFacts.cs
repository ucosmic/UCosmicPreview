using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class AffiliationRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_AndEstablishmentId0_MapsToNothing()
            {
                const int establishmentId = 0;
                const string routeUrl = AffiliationRouter.Get.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithGet_AndPositiveEstablishmentId_MapsToGetAction()
            {
                const int establishmentId = 1;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Get(establishmentId);
                const string routeUrl = AffiliationRouter.Get.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGetPostOrPut_AndEstablishmentId0_MapsToNothing()
            {
                const int establishmentId = 0;
                const string routeUrl = AffiliationRouter.Get.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithNonGetPostOrPut_AndPositiveEstablishmentId_MapsToNothing()
            {
                const int establishmentId = 4;
                const string routeUrl = AffiliationRouter.Get.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_WithEstablishmentId0_MapsToUrl()
            {
                const int establishmentId = 0;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Get(establishmentId);

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldBeNull();
            }

            [TestMethod]
            public void Outbound_ForGetAction_AndPositiveEstablishmentId_MapsToUrl()
            {
                const int establishmentId = 5;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Get(establishmentId);
                const string routeUrl = AffiliationRouter.Get.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_ForGetAction_AndEstablishmentId0_MapToNothing()
            {
                const int establishmentId = 0;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Get(establishmentId);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

            [TestMethod]
            public void Defaults_ForGetAction_AndPositiveEstablishmentId_MapToNothing()
            {
                const int establishmentId = 7;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Get(establishmentId);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class ThePutRoute
        {
            [TestMethod]
            public void Inbound_WithPut_AndEstablishmentId0_MapsToNothing()
            {
                const int establishmentId = 0;
                const string routeUrl = AffiliationRouter.Put.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPut_AndPositiveEstablishmentId_MapsToPutAction()
            {
                const int establishmentId = 2;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Put(null);
                const string routeUrl = AffiliationRouter.Put.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithPost_AndEstablishmentId0_MapsToNothing()
            {
                const int establishmentId = 0;
                const string routeUrl = AffiliationRouter.Put.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPost_AndPositiveEstablishmentId_MapsToPutAction()
            {
                const int establishmentId = 3;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Put(null);
                const string routeUrl = AffiliationRouter.Put.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Outbound_ForPutAction_WithEstablishmentId0_MapsToNothing()
            {
                const int establishmentId = 0;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Put(null);
                const string establishmentIdParam = "establishmentId";

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty(establishmentIdParam, establishmentId).AppRelativeUrl().ShouldBeNull();
            }

            [TestMethod]
            public void Outbound_ForPutAction_AndPositiveEstablishmentId_MapsToUrl()
            {
                const int establishmentId = 6;
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Put(null);
                const string routeUrl = AffiliationRouter.Put.Route;
                const string establishmentIdParam = "establishmentId";
                var urlFormat = routeUrl.Replace(establishmentIdParam, "0");
                var url = string.Format(urlFormat, establishmentId).ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .HavingViewModelProperty(establishmentIdParam, establishmentId).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPutAction_MapToNothing()
            {
                Expression<Func<AffiliationController, ActionResult>> action =
                    controller => controller.Put(null);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }
    }
}
