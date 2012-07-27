using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.People
{
    // ReSharper disable UnusedMember.Global
    public class PeopleAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_People_LowerCase()
            {
                var areaRegistration = new PeopleAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("people");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("People/{controller}/{action}/{id}",
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["People_default"].ShouldBeNull();
            }
        }
    }
}
