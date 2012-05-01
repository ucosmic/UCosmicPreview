using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Passwords
{
    // ReSharper disable UnusedMember.Global
    public class PasswordsAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheAreaNameProperty
        {
            [TestMethod]
            public void Equals_Passwords_LowerCase()
            {
                var areaRegistration = new PasswordsAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("passwords");
            }
        }

        [TestClass]
        public class TheRegisterAreaMethod
        {
            [TestMethod]
            public void RegistersNoDefaultRoute()
            {
                RouteTable.Routes.Where(r => r is Route).Cast<Route>()
                    .SingleOrDefault(r => r.Url.Equals("Passwords/{controller}/{action}/{id}", 
                        StringComparison.OrdinalIgnoreCase))
                    .ShouldBeNull();
                RouteTable.Routes["Passwords_default"].ShouldBeNull();
            }
        }
    }
}
