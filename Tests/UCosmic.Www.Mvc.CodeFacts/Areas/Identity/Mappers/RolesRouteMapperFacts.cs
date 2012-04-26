using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    // ReSharper disable UnusedMember.Global
    public class RolesRouteMapperFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Identity.Name;

        [TestClass]
        public class Put
        {
            [TestMethod]
            public void OutBoundUrl_IsNotRouted()
            {
                var model = new RoleForm { EntityId = Guid.NewGuid() };
                Expression<Func<RolesController, ActionResult>> action =
                    controller => controller.Put(model);
                var url = RolesRouter.Put.Route.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldNotEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithPutAndPostMethods_IsRouted()
            {
                var model = new RoleForm { EntityId = Guid.NewGuid() };
                Expression<Func<RolesController, ActionResult>> action =
                    controller => controller.Put(model);
                var url = RolesRouter.Put.Route.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                url.WithMethod(HttpVerbs.Put).AndMethodArg("model", model).ShouldMapTo(action);
                url.WithMethod(HttpVerbs.Post).AndMethodArg("model", model).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonPutAndPostMethods_IsNotRouted()
            {
                var model = new RoleForm { EntityId = Guid.NewGuid() };
                var url = RolesRouter.Put.Route.ToAppRelativeUrl()
                    .Replace("{entityId}", model.EntityId.ToString());
                url.WithMethodsExcept(HttpVerbs.Put, HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                var model = new RoleForm { EntityId = Guid.NewGuid() };
                Expression<Func<RolesController, ActionResult>> action =
                    controller => controller.Put(model);
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }
    }
}
