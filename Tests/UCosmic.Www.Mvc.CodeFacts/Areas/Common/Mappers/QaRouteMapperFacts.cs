using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.Common.Controllers;

namespace UCosmic.Www.Mvc.Areas.Common.Mappers
{
    // ReSharper disable UnusedMember.Global
    public class QaRouteMapperFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class DeliverQaMail
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.DeliverQaMail();
                var url = QaRouteMapper.DeliverQaMail.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.DeliverQaMail();
                var url = QaRouteMapper.DeliverQaMail.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethodIsNotRouted()
            {
                var url = QaRouteMapper.DeliverQaMail.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.DeliverQaMail();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class ResetQaMail
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.ResetQaMail();
                var url = QaRouteMapper.ResetQaMail.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.ResetQaMail();
                var url = QaRouteMapper.ResetQaMail.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethodIsNotRouted()
            {
                var url = QaRouteMapper.ResetQaMail.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.ResetQaMail();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }
    }
}
