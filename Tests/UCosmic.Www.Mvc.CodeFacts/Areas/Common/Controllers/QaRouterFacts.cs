using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public static class QaRouterFacts
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
                var url = new QaRouter.DeliverQaMailRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.DeliverQaMail();
                var url = new QaRouter.DeliverQaMailRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethodIsNotRouted()
            {
                var url = new QaRouter.DeliverQaMailRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
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
                var url = new QaRouter.ResetQaMailRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<QaController, ActionResult>> action =
                   controller => controller.ResetQaMail();
                var url = new QaRouter.ResetQaMailRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethodIsNotRouted()
            {
                var url = new QaRouter.ResetQaMailRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }
    }
}
