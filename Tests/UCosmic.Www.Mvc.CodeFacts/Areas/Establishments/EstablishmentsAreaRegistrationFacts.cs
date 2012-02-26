using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EstablishmentsAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Establishments.Name;
        private static readonly string[] Controllers = new[]
        {
            MVC.Establishments.ManagementForms.Name,
            MVC.Establishments.SupplementalForms.Name,
        };


        [TestClass]
        public class RegisterArea_Method
        {
            [TestMethod]
            public void DefaultAreaUrls_AreNotMapped()
            {
                //Area.DefaultAreaRoutes().ShouldMapToNothing(); // ~/Establishments is mapped
                foreach (var controller in Controllers)
                    Area.DefaultAreaRoutes(controller).ShouldMapToNothing();
            }
        }

        [TestClass]
        public class AreaName_Property
        {
            [TestMethod]
            public void IsCommon()
            {
                var areaRegistration = new EstablishmentsAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("establishments");
            }
        }
    }
}
