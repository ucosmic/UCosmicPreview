using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    // ReSharper disable UnusedMember.Global
    public static class HealthRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class RunEstablishmentHierarchy
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentHierarchy();
                var url = new HealthRouter.RunEstablishmentHierarchyRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentHierarchy();
                var url = new HealthRouter.RunEstablishmentHierarchyRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new HealthRouter.RunEstablishmentHierarchyRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class RunInstitutionalAgreementHierarchy
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunInstitutionalAgreementHierarchy();
                var url = new HealthRouter.RunInstitutionalAgreementHierarchyRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunInstitutionalAgreementHierarchy();
                var url = new HealthRouter.RunInstitutionalAgreementHierarchyRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new HealthRouter.RunInstitutionalAgreementHierarchyRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class RunEstablishmentImport
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentImport();
                var url = new HealthRouter.RunEstablishmentImportRoute().Url.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentImport();
                var url = new HealthRouter.RunEstablishmentImportRoute().Url.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = new HealthRouter.RunEstablishmentImportRoute().Url.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }
        }
    }
}
