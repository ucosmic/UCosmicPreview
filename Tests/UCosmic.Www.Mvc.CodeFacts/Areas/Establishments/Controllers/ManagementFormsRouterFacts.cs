using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms;

namespace UCosmic.Www.Mvc.Areas.Establishments.Controllers
{
    public static class ManagementFormsRouterFacts
    {
        private static readonly string Area = MVC.Establishments.Name;

        [TestClass]
        public class Browse
        {
            [TestMethod]
            public void Maps4Urls_FirstIsRestful()
            {
                var route = new ManagementFormsRouter.BrowseRoute();
                route.Url.ShouldEqual("establishments");
                route.AlternateUrls.ShouldNotBeNull();
                route.AlternateUrls.Count().ShouldEqual(3);
                route.AlternateUrls.First().ShouldEqual("establishments/manage");
                route.AlternateUrls.Skip(1).First().ShouldEqual("establishments/manage/browse");
                route.AlternateUrls.Last().ShouldEqual("establishments/manage/browse.html");
            }

            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                   controller => controller.Browse();
                var url = new ManagementFormsRouter.BrowseRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_Restful_WithGetMethod_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                   controller => controller.Browse();
                var url = new ManagementFormsRouter.BrowseRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_Restful_WithDeleteMethod_IsNotRouted()
            {
                var url = new ManagementFormsRouter.BrowseRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Delete).ShouldMapToNothing();
            }

            [TestMethod]
            public void InBoundUrl_Restful_WithHeadMethod_IsNotRouted()
            {
                var url = new ManagementFormsRouter.BrowseRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Head).ShouldMapToNothing();
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
                var url = new ManagementFormsRouter.FormAddRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_ForAdd_WithGetMethod_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(null);
                var url = new ManagementFormsRouter.FormAddRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_ForAdd_WithNonGetMethod_IsNotRouted()
            {
                var url = new ManagementFormsRouter.FormAddRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void OutBoundUrl_FoEdit_IsRouted()
            {
                var entityId = Guid.NewGuid();
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(entityId);
                var url = new ManagementFormsRouter.FormEditRoute().Url.ToAppRelativeUrl()
                    .Replace("{entityId}", entityId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_FoEdit_WithGetMethod_IsRouted()
            {
                var entityId = Guid.NewGuid();
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Form(entityId);
                var url = new ManagementFormsRouter.FormEditRoute().Url.ToAppRelativeUrl()
                    .Replace("{entityId}", entityId.ToString());
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_FoEdit_WithNonGetMethod_IsNotRouted()
            {
                var entityId = Guid.NewGuid();
                var url = new ManagementFormsRouter.FormEditRoute().Url.ToAppRelativeUrl()
                    .Replace("{entityId}", entityId.ToString());
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
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
                var url = new ManagementFormsRouter.PutRoute().Url.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldNotEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithPutAndPostMethods_IsRouted()
            {
                var model = new EstablishmentForm { EntityId = Guid.NewGuid() };
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.Put(model);
                var url = new ManagementFormsRouter.PutRoute().Url.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                url.WithMethod(HttpVerbs.Put).AndMethodArg("model", model).ShouldMapTo(action);
                url.WithMethod(HttpVerbs.Post).AndMethodArg("model", model).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonPutAndPostMethods_IsNotRouted()
            {
                var model = new EstablishmentForm { EntityId = Guid.NewGuid() };
                var url = new ManagementFormsRouter.PutRoute().Url.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                url.WithMethodsExcept(HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
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
                var url = new ManagementFormsRouter.NewNameRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<ManagementFormsController, ActionResult>> action =
                    controller => controller.NewName();
                var url = new ManagementFormsRouter.NewNameRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new ManagementFormsRouter.NewNameRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }
    }
}
