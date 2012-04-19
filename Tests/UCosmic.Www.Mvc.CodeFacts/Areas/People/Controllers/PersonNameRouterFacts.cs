using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class PersonNameRouterFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheGenerateDisplayNameRoute
        {
            [TestMethod]
            public void Inbound_WithGet_MapsToNothing()
            {
                var url = PersonNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Get).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPut_MapsToNothing()
            {
                var url = PersonNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Put).ShouldMapToNothing();
            }

            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<PersonNameController, ActionResult>> action =
                    controller => controller.GenerateDisplayName(null);
                var url = PersonNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = PersonNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<PersonNameController, ActionResult>> action =
                    controller => controller.GenerateDisplayName(null);
                var url = PersonNameRouter.GenerateDisplayName.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(MVC.People.Name).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }

            [TestMethod]
            public void Defaults_WithPostAction_MapToNothing()
            {
                Expression<Func<PersonNameController, ActionResult>> action =
                    controller => controller.GenerateDisplayName(null);

                action.DefaultAreaRoutes(MVC.People.Name).ShouldMapToNothing();
            }

        }
    }
}
