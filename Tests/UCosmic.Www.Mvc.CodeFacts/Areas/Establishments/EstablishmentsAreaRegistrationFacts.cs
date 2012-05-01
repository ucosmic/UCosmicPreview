using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EstablishmentsAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_Establishments_LowerCase()
            {
                var areaRegistration = new EstablishmentsAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("establishments");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("Establishments/{controller}/{action}/{id}",
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["Establishments_default"].ShouldBeNull();
            }
        }
    }
}
