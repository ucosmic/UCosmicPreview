using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class UpdateNameRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheGetRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToGetAction()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Get();
                var url = UpdateNameRouter.Get.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonGet_MapsToNothing()
            {
                var url = UpdateNameRouter.Get.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForGetAction_MapsToUrl()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Get();
                var url = UpdateNameRouter.Get.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Get).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_ForGetAction_MapToNothing()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Get();

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class ThePutRoute
        {
            [TestMethod]
            public void Inbound_WithPut_MapsToPutAction()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Put(null);
                var url = UpdateNameRouter.Put.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithPost_MapsToPutAction()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Put(null);
                var url = UpdateNameRouter.Put.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPutOrPost_MapsToNothing()
            {
                var url = UpdateNameRouter.Put.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPutAction_WithPutMethod_MapsToUrl()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Put(null);
                var url = UpdateNameRouter.Put.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Put)
                    .AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Outbound_ForPutAction_WithPostMethod_MapsToUrl()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Put(null);
                var url = UpdateNameRouter.Put.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPutAction_MapToNothing()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.Put(null);
                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }

        [TestClass]
        public class TheGenerateDisplayNameRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToNothing()
            {
                var url = UpdateNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPut_MapsToNothing()
            {
                var url = UpdateNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.GenerateDisplayName(null);
                var url = UpdateNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = UpdateNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.GenerateDisplayName(null);
                var url = UpdateNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.My.Name).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPostAction_MapToNothing()
            {
                Expression<Func<UpdateNameController, ActionResult>> action =
                    controller => controller.GenerateDisplayName(null);

                action.DefaultAreaRoutes(MVC.My.Name).ShouldMapToNothing();
            }

        }
    }
}
