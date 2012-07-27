using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    // ReSharper disable UnusedMember.Global
    public class IdentityAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_Identity_LowerCase()
            {
                var areaRegistration = new IdentityAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("identity");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("Identity/{controller}/{action}/{id}",
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["Identity_default"].ShouldBeNull();
            }
        }
    }
}
