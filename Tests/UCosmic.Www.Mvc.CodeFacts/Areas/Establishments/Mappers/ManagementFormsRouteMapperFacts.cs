using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.Establishments.Controllers;
using UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms;

namespace UCosmic.Www.Mvc.Areas.Establishments.Mappers
{
    // ReSharper disable UnusedMember.Global
    public class ManagementFormsRouteMapperFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Establishments.Name;

        [TestClass]
        public class Browse
        {
            [TestMethod]
            public void Maps4Urls_FirstIsRestful()
            {
                ManagementFormsRouteMapper.Browse.Routes.ShouldNotBeNull();
                ManagementFormsRouteMapper.Browse.Routes.Length.ShouldEqual(4);
                ManagementFormsRouteMapper.Browse.Routes[0].ShouldEqual("establishments");
            }

            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                   controller => controller.Browse();
                var url = ManagementFormsRouteMapper.Browse.Routes[0].ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_Restful_WithGetMethod_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                   controller => controller.Browse();
                var url = ManagementFormsRouteMapper.Browse.Routes[0].ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_Restful_WithDeleteMethod_IsNotRouted()
            {
                var url = ManagementFormsRouteMapper.Browse.Routes[0].ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Delete).ShouldMapToNothing();
            }

            [TestMethod]
            public void InBoundUrl_Restful_WithHeadMethod_IsNotRouted()
            {
                var url = ManagementFormsRouteMapper.Browse.Routes[0].ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Head).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                   controller => controller.Browse();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Form
        {
            [TestMethod]
            public void OutBoundUrl_ForAdd_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(null);
                var url = ManagementFormsRouteMapper.Form.RouteForAdd.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_ForAdd_WithGetMethod_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(null);
                var url = ManagementFormsRouteMapper.Form.RouteForAdd.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_ForAdd_WithNonGetMethod_IsNotRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(null);
                var url = ManagementFormsRouteMapper.Form.RouteForAdd.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_ForAdd_AreNotRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(null);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod]
            public void OutBoundUrl_FoEdit_IsRouted()
            {
                var entityId = Guid.NewGuid();
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(entityId);
                var url = ManagementFormsRouteMapper.Form.RouteForEdit.ToAppRelativeUrl()
                    .Replace("{entityId}", entityId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_FoEdit_WithGetMethod_IsRouted()
            {
                var entityId = Guid.NewGuid();
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(entityId);
                var url = ManagementFormsRouteMapper.Form.RouteForEdit.ToAppRelativeUrl()
                    .Replace("{entityId}", entityId.ToString());
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_FoEdit_WithNonGetMethod_IsNotRouted()
            {
                var entityId = Guid.NewGuid();
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(entityId);
                var url = ManagementFormsRouteMapper.Form.RouteForEdit.ToAppRelativeUrl()
                    .Replace("{entityId}", entityId.ToString());
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_ForEdit_AreNotRouted()
            {
                var entityId = Guid.NewGuid();
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(entityId);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class Put
        {
            [TestMethod]
            public void OutBoundUrl_IsNotRouted()
            {
                var model = new EstablishmentForm { EntityId = Guid.NewGuid() };
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Put(model);
                var url = ManagementFormsRouteMapper.Put.Route.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldNotEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithPutAndPostMethods_IsRouted()
            {
                var model = new EstablishmentForm { EntityId = Guid.NewGuid() };
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Put(model);
                var url = ManagementFormsRouteMapper.Put.Route.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                url.WithMethod(HttpVerbs.Put).AndMethodArg("model", model).ShouldMapTo(action);
                url.WithMethod(HttpVerbs.Post).AndMethodArg("model", model).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonPutAndPostMethods_IsNotRouted()
            {
                var model = new EstablishmentForm { EntityId = Guid.NewGuid() };
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Put(model);
                var url = ManagementFormsRouteMapper.Put.Route.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                url.WithMethodsExcept(HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                var model = new EstablishmentForm { EntityId = Guid.NewGuid() };
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Put(model);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class NewName
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.NewName();
                var url = ManagementFormsRouteMapper.NewName.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.NewName();
                var url = ManagementFormsRouteMapper.NewName.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.NewName();
                var url = ManagementFormsRouteMapper.NewName.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.NewName();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }
    }
}
