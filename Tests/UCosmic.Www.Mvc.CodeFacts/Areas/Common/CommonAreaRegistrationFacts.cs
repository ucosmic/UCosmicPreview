using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Common
{
    // ReSharper disable UnusedMember.Global
    public class CommonAreaRegistrationFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string[] Controllers = new[]
        {
            MVC.Common.Errors.Name,
            MVC.Common.Features.Name,
            MVC.Common.Health.Name,
            MVC.Common.Navigation.Name,
            MVC.Common.Qa.Name,
            MVC.Common.Skins.Name,
        };

        //[TestClass]
        //public class RegisterArea_Method
        //{
        //    [TestMethod]
        //    public void DefaultAreaUrls_AreNotMapped()
        //    {
        //        Area.DefaultAreaRoutes().ShouldMapToNothing();
        //        foreach (var controller in Controllers)
        //            Area.DefaultAreaRoutes(controller).ShouldMapToNothing();
        //    }
        //}

        [TestClass]
        public class AreaName_Property
        {
            [TestMethod]
            public void IsCommon()
            {
                var areaRegistration = new CommonAreaRegistration();
                areaRegistration.AreaName.ShouldEqual("common");
            }
        }
    }
}
