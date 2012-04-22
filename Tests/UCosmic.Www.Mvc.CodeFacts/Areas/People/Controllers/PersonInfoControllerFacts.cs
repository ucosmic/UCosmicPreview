using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class PersonInfoControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_Authorize()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(PersonInfoController), typeof(AuthorizeAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<AuthorizeAttribute>();
            }
        }

        [TestClass]
        public class TheByEmailMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<PersonInfoController, ActionResult>> method = m => m.ByEmail(null);

                var attributes = method.GetAttributes<PersonInfoController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGetPersonByEmail()
            {
                const string email = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PersonQueryBasedOn(email))))
                    .Returns(null as Person);

                controller.ByEmail(email);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PersonQueryBasedOn(email))),
                        Times.Once());
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenPersonIsNotFound()
            {
                const string email = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PersonQueryBasedOn(email))))
                    .Returns(null as Person);

                var result = controller.ByEmail(email);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithPersonInfoModelData_WhenPersonIsFound()
            {
                const string email = "test@test.test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PersonQueryBasedOn(email))))
                    .Returns(new Person
                    {
                        Emails = new[]
                        {
                            new EmailAddress
                            {
                                IsDefault = true,
                                Value = email,
                            }
                        }
                    });

                var result = controller.ByEmail(email);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel>();
                var model = (PersonInfoModel) result.Data;
                model.DefaultEmail.ShouldEqual(email);
            }

            private static Expression<Func<GetPersonByEmailQuery, bool>> PersonQueryBasedOn(string email)
            {
                Expression<Func<GetPersonByEmailQuery, bool>> personQueryBasedOn = q =>
                    q.Email == email
                ;
                return personQueryBasedOn;
            }
        }

        [TestClass]
        public class TheByGuidMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<PersonInfoController, ActionResult>> method = m => m.ByGuid(Guid.Empty);

                var attributes = method.GetAttributes<PersonInfoController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGetPersonByGuid()
            {
                var guid = Guid.NewGuid();
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PersonQueryBasedOn(guid))))
                    .Returns(null as Person);

                controller.ByGuid(guid);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PersonQueryBasedOn(guid))),
                        Times.Once());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenGuidIsEmpty()
            {
                var guid = Guid.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.ByGuid(guid);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PersonQueryBasedOn(guid))),
                        Times.Never());
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenGuidIsEmpty()
            {
                var guid = Guid.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.ByGuid(guid);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenPersonIsNotFound()
            {
                var guid = Guid.NewGuid();
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PersonQueryBasedOn(guid))))
                    .Returns(null as Person);

                var result = controller.ByGuid(guid);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithPersonInfoModelData_WhenPersonIsFound()
            {
                var guid = Guid.NewGuid();
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PersonQueryBasedOn(guid))))
                    .Returns(new Person
                    {
                        EntityId = guid,
                    });

                var result = controller.ByGuid(guid);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel>();
                var model = (PersonInfoModel)result.Data;
                model.EntityId.ShouldEqual(guid);
            }

            private static Expression<Func<GetPersonByGuidQuery, bool>> PersonQueryBasedOn(Guid guid)
            {
                Expression<Func<GetPersonByGuidQuery, bool>> personQueryBasedOn = q =>
                    q.Guid == guid
                ;
                return personQueryBasedOn;
            }
        }

        private class ScenarioOptions
        {
            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
        }

        private static PersonInfoController CreateController(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();

            scenarioOptions.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            var services = new PersonInfoServices(scenarioOptions.MockQueryProcessor.Object);

            var controller = new PersonInfoController(services);

            return controller;
        }
    }
}
