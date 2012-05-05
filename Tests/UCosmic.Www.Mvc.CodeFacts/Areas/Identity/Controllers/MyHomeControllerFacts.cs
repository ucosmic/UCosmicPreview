using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class MyHomeControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_Authorize()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(MyHomeController), typeof(AuthorizeAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<AuthorizeAttribute>();
            }
        }

        [TestClass]
        public class TheGetMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<MyHomeController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<MyHomeController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<MyHomeController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<MyHomeController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingMyHome()
            {
                Expression<Func<MyHomeController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<MyHomeController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("my-home");
            }

            [TestMethod]
            public void ThrowsNullReferenceException_WhenUserIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<GetMyEmailAddressByNumberQuery>()))
                    .Returns(null as EmailAddress);
                NullReferenceException exception = null;

                try
                {
                    controller.Get();
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ForUser_ByName()
            {
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = userName,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery =
                    query => query.Name == userName;
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);

                controller.Get();

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(userByNameQuery)),
                        Times.Once());
            }

            [TestMethod]
            public void Returns404_WheUser_CannotBeFound()
            {
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = userName,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery =
                    query => query.Name == userName;
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);

                var result = controller.Get();

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsView_WhenUser_IsFound()
            {
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = userName,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery =
                    query => query.Name == userName;
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(userByNameQuery)))
                    .Returns(new User { Person = new Person() });

                var result = controller.Get();

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<MyHomeInfo>();
            }
        }

        private class ScenarioOptions
        {
            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
            internal string PrincipalIdentityName { get; set; }
        }

        private static MyHomeController CreateController(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();

            scenarioOptions.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            var services = new MyHomeServices(scenarioOptions.MockQueryProcessor.Object);

            var controller = new MyHomeController(services);

            var builder = ReuseMock.TestControllerBuilder();

            builder.HttpContext.User = null;
            if (!string.IsNullOrWhiteSpace(scenarioOptions.PrincipalIdentityName))
            {
                builder.HttpContext.User = scenarioOptions.PrincipalIdentityName.AsPrincipal();
            }

            builder.InitializeController(controller);

            return controller;
        }
    }
}
