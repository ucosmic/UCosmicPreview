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
    public class HealthRouteMapperFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;

        [TestClass]
        public class SampleCachedPage
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.SampleCachedPage();
                var url = HealthRouteMapper.SampleCachedPage.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.SampleCachedPage();
                var url = HealthRouteMapper.SampleCachedPage.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = HealthRouteMapper.SampleCachedPage.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.SampleCachedPage();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class RunEstablishmentHierarchy
        {
            [TestMethod]
            public void OutBoundUrl_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentHierarchy();
                var url = HealthRouteMapper.RunEstablishmentHierarchy.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentHierarchy();
                var url = HealthRouteMapper.RunEstablishmentHierarchy.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = HealthRouteMapper.RunEstablishmentHierarchy.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentHierarchy();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = HealthRouteMapper.RunInstitutionalAgreementHierarchy.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunInstitutionalAgreementHierarchy();
                var url = HealthRouteMapper.RunInstitutionalAgreementHierarchy.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = HealthRouteMapper.RunInstitutionalAgreementHierarchy.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunInstitutionalAgreementHierarchy();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
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
                var url = HealthRouteMapper.RunEstablishmentImport.Route.ToAppRelativeUrl();
                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void InBoundUrl_WithGetMethod_IsRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentImport();
                var url = HealthRouteMapper.RunEstablishmentImport.Route.ToAppRelativeUrl();
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            }

            [TestMethod]
            public void InBoundUrl_WithNonGetMethod_IsNotRouted()
            {
                var url = HealthRouteMapper.RunEstablishmentImport.Route.ToAppRelativeUrl();
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void DefaultAreaUrls_AreNotRouted()
            {
                Expression<Func<HealthController, ActionResult>> action =
                    controller => controller.RunEstablishmentImport();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }
        }
    }
}
