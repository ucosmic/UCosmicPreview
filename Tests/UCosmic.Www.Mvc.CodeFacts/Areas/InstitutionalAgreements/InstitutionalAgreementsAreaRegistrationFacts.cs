using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public static class InstitutionalAgreementsAreaRegistrationFacts
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_InstitutionalAgreements_PascalCase()
            {
                var areaRegistration = new InstitutionalAgreementsAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("InstitutionalAgreements");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("InstitutionalAgreements/{controller}/{action}/{id}",
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["InstitutionalAgreements_default"].ShouldBeNull();
            }
        }
    }
}
