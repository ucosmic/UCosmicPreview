using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    // ReSharper disable UnusedMember.Global
    public static class ActivityListRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string AreaName = MVC.Activities.Name;
        private static readonly string ControllerName = MVC.Activities.ActivityList.Name;

        [TestClass]
        public class TheShortListRouteConstructor
        {
            [TestMethod]
            public void ConfiguresDefaults()
            {
                var route = new ActivityListRouter.ShortListRoute();
                route.DataTokens.ShouldContain(new KeyValuePair<string, object>("area", AreaName));
                route.Defaults.ShouldContain(new KeyValuePair<string, object>("controller", ControllerName));
                route.Defaults.ShouldContain(new KeyValuePair<string, object>("action",
                    MVC.Activities.ActivityList.ActionNames.ShortList));
            }

            [TestMethod]
            public void ConfiguresConstraints()
            {
                var route = new ActivityListRouter.ShortListRoute();
                route.Constraints.ShouldNotBeNull();
                route.Constraints.Count.ShouldEqual(1);
                route.Constraints.Single().Key.ShouldEqual("httpMethod");
                route.Constraints.Single().Value.ShouldBeType<HttpMethodConstraint>();
                var httpMethodConstraint = (HttpMethodConstraint)route.Constraints.Single().Value;
                httpMethodConstraint.AllowedMethods.Count.ShouldEqual(1);
                httpMethodConstraint.AllowedMethods.ShouldContain("GET");
            }
        }

        [TestClass]
        public class ThePageRouteConstructor
        {
            [TestMethod]
            public void ConfiguresDefaults()
            {
                var route = new ActivityListRouter.PageRoute();
                route.DataTokens.ShouldContain(new KeyValuePair<string, object>("area", AreaName));
                route.Defaults.ShouldContain(new KeyValuePair<string, object>("controller", ControllerName));
                route.Defaults.ShouldContain(new KeyValuePair<string, object>("action",
                    MVC.Activities.ActivityList.ActionNames.Page));
            }

            [TestMethod]
            public void ConfiguresConstraints()
            {
                var route = new ActivityListRouter.ShortListRoute();
                route.Constraints.ShouldNotBeNull();
                route.Constraints.Count.ShouldEqual(1);
                route.Constraints.Single().Key.ShouldEqual("httpMethod");
                route.Constraints.Single().Value.ShouldBeType<HttpMethodConstraint>();
                var httpMethodConstraint = (HttpMethodConstraint)route.Constraints.Single().Value;
                httpMethodConstraint.AllowedMethods.Count.ShouldEqual(1);
                httpMethodConstraint.AllowedMethods.ShouldContain("GET");
            }
        }
    }
}
