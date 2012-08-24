using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Areas.Activities;
using UCosmic.Www.Mvc.Areas.Common;
using UCosmic.Www.Mvc.Areas.Establishments;
using UCosmic.Www.Mvc.Areas.Identity;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements;
using UCosmic.Www.Mvc.Areas.People;
using UCosmic.Www.Mvc.Areas.RecruitmentAgencies;
using UCosmic.Www.Mvc.Areas.Roles;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc
{
    [TestClass]
    public static class GlobalAsaxFacts
    {
        [AssemblyInitialize]
        public static void RegisterAllRoutes(TestContext testContext)
        {
            // register routes once when the test suite begins
            RouteTable.Routes.Clear();
            RouteRegistration.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            MvcApplication.RegisterRoutes(RouteTable.Routes);
            AutoMapperRegistration.RegisterProfiles();
        }

        [TestClass]
        public class TheRegisterGlobalFiltersMethod
        {
            [TestMethod]
            public void Registers_ElmahHandleErrorAttribute()
            {
                var filters = new GlobalFilterCollection();
                MvcApplication.RegisterGlobalFilters(filters);

                filters.ShouldNotBeNull();
                filters.Count.ShouldBeInRange(1, int.MaxValue);

                var expectedFilter = filters.SingleOrDefault(filter =>
                    typeof(ElmahHandleErrorAttribute) == filter.Instance.GetType());
                expectedFilter.ShouldNotBeNull();
            }

            [TestMethod]
            public void Registers_EnforceHttpsAttribute()
            {
                var filters = new GlobalFilterCollection();
                MvcApplication.RegisterGlobalFilters(filters);

                filters.ShouldNotBeNull();
                filters.Count.ShouldBeInRange(1, int.MaxValue);

                var expectedFilter = filters.SingleOrDefault(filter =>
                    typeof(EnforceHttpsAttribute) == filter.Instance.GetType());
                expectedFilter.ShouldNotBeNull();
            }
        }

        [TestClass]
        public class TheRegisterRoutesMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("{controller}/{action}/{id}",
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["Default"].ShouldBeNull();
            }
        }

        private static class AreaRegistration
        {
            private static void RegisterArea(System.Web.Mvc.AreaRegistration area, RouteCollection routes)
            {
                var context = new AreaRegistrationContext(area.AreaName, routes);
                context.Namespaces.Add(area.GetType().Namespace);
                area.RegisterArea(context);
            }

            public static void RegisterAllAreas()
            {
                new System.Web.Mvc.AreaRegistration[]
                {
                    new ActivitiesAreaRegistration(),
                    new CommonAreaRegistration(),
                    new EstablishmentsAreaRegistration(),
                    new IdentityAreaRegistration(),
                    new InstitutionalAgreementsAreaRegistration(),
                    new PeopleAreaRegistration(),
                    new RecruitmentAgenciesAreaRegistration(),
                    new RolesAreaRegistration(),

                }.ToList().ForEach(area => RegisterArea(area, RouteTable.Routes));
            }
        }
    }
}
