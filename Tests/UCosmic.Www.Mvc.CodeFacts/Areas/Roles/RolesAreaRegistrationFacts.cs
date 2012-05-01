using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Roles
{
    // ReSharper disable UnusedMember.Global
    public class RolesAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_Roles_LowerCase()
            {
                var areaRegistration = new RolesAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("roles");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("Roles/{controller}/{action}/{id}", 
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["Roles_default"].ShouldBeNull();
            }
        }
    }
}
