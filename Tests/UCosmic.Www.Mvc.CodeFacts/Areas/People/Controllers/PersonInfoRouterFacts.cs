using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public static class PersonInfoRouterFacts
    {
        private static readonly string AreaName = MVC.People.Name;

        [TestClass]
        public class TheByEmailRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.ByEmail(null);
                var url = new PersonInfoRouter.ByEmailRoute().Url.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = new PersonInfoRouter.ByEmailRoute().Url.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.ByEmail(null);
                var url = new PersonInfoRouter.ByEmailRoute().Url.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }
        }

        [TestClass]
        public class TheByGuidRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.ByGuid();
                var url = new PersonInfoRouter.ByGuidRoute().Url.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = new PersonInfoRouter.ByGuidRoute().Url.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.ByGuid();
                var url = new PersonInfoRouter.ByGuidRoute().Url.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }
        }

        [TestClass]
        public class TheWithEmailRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.WithEmail();
                var url = new PersonInfoRouter.WithEmailRoute().Url.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = new PersonInfoRouter.WithEmailRoute().Url.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.WithEmail();
                var url = new PersonInfoRouter.WithEmailRoute().Url.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }
        }

        [TestClass]
        public class TheWithFirstNameRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.WithFirstName();
                var url = new PersonInfoRouter.WithFirstNameRoute().Url.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = new PersonInfoRouter.WithFirstNameRoute().Url.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.WithFirstName();
                var url = new PersonInfoRouter.WithFirstNameRoute().Url.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }
        }

        [TestClass]
        public class TheWithLastNameRoute
        {
            [TestMethod]
            public void Inbound_WithPost_MapsToPostAction()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.WithLastName();
                var url = new PersonInfoRouter.WithLastNameRoute().Url.ToAppRelativeUrl();

                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
            }

            [TestMethod]
            public void Inbound_WithNonPost_MapsToNothing()
            {
                var url = new PersonInfoRouter.WithLastNameRoute().Url.ToAppRelativeUrl();

                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
            }

            [TestMethod]
            public void Outbound_ForPostAction_MapsToUrl()
            {
                Expression<Func<PersonInfoController, ActionResult>> action =
                    controller => controller.WithLastName();
                var url = new PersonInfoRouter.WithLastNameRoute().Url.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(AreaName).WithMethod(HttpVerbs.Post)
                    .AppRelativeUrl().ShouldEqual(url);
            }
        }
    }
}
