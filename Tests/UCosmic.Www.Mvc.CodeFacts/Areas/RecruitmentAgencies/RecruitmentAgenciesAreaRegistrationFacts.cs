using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies
{
    // ReSharper disable UnusedMember.Global
    public class RecruitmentAgenciesAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_RecruitmentAgencies_PascalCase()
            {
                var areaRegistration = new RecruitmentAgenciesAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("RecruitmentAgencies");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("RecruitmentAgencies/{controller}/{action}/{id}", 
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["RecruitmentAgencies_default"].ShouldBeNull();
            }
        }
    }
}
