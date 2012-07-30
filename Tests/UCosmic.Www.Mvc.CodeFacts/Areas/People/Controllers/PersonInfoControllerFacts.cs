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
    public static class PersonInfoControllerFacts
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
            public void ExecutesNoQuery_WhenEmailArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.ByEmail(null);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PersonQueryBasedOn(null))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenEmailArgIsEmptyString()
            {
                var email = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.ByEmail(email);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PersonQueryBasedOn(email))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenEmailArgIsWhiteSpace()
            {
                const string email = " \t";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.ByEmail(email);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PersonQueryBasedOn(email))),
                        Times.Never());
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenEmailArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.ByEmail(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenEmailArgIsEmptyString()
            {
                var email = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.ByEmail(email);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenEmailArgIsWhiteSpace()
            {
                const string email = "\t ";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.ByEmail(email);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
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

        [TestClass]
        public class TheWithEmailMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<PersonInfoController, ActionResult>> method = m => m.WithEmail(null, StringMatchStrategy.Equals);

                var attributes = method.GetAttributes<PersonInfoController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGetPeopleWithEmail()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(term))))
                    .Returns(new Person[] {});

                controller.WithEmail(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(term))),
                        Times.Once());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithEmail(null);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(null))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsEmptyString()
            {
                var term = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithEmail(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(term))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsWhiteSpace()
            {
                const string term = " \t";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithEmail(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(term))),
                        Times.Never());
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithEmail(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsEmptyString()
            {
                var term = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithEmail(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsWhiteSpace()
            {
                const string term = "\t ";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithEmail(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithEmptyData_WhenPeopleAreNotFound()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(term))))
                    .Returns(new Person[] {});

                var result = controller.WithEmail(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel[]>();
                var models = (PersonInfoModel[]) result.Data;
                models.Length.ShouldEqual(0);
            }

            [TestMethod]
            public void ReturnsJson_WithPersonInfoModelArrayData_WhenPeopleAreFound()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithEmailQueryBasedOn(term))))
                    .Returns(new[]
                    {
                        new Person(),
                        new Person(),
                        new Person(),
                    });

                var result = controller.WithEmail(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel[]>();
                var models = (PersonInfoModel[])result.Data;
                models.Length.ShouldEqual(3);
            }

            private static Expression<Func<FindPeopleWithEmailQuery, bool>> PeopleWithEmailQueryBasedOn(string term)
            {
                Expression<Func<FindPeopleWithEmailQuery, bool>> personQueryBasedOn = q =>
                    q.Term == term
                ;
                return personQueryBasedOn;
            }
        }

        [TestClass]
        public class TheWithFirstNameMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<PersonInfoController, ActionResult>> method = m => m.WithFirstName(null, StringMatchStrategy.Equals);

                var attributes = method.GetAttributes<PersonInfoController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGetPeopleWithFirstName()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(term))))
                    .Returns(new Person[] { });

                controller.WithFirstName(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(term))),
                        Times.Once());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithFirstName(null);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(null))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsEmptyString()
            {
                var term = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithFirstName(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(term))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsWhiteSpace()
            {
                const string term = " \t";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithFirstName(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(term))),
                        Times.Never());
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithFirstName(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsEmptyString()
            {
                var term = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithFirstName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsWhiteSpace()
            {
                const string term = "\t ";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithFirstName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithEmptyData_WhenPeopleAreNotFound()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(term))))
                    .Returns(new Person[] { });

                var result = controller.WithFirstName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel[]>();
                var models = (PersonInfoModel[])result.Data;
                models.Length.ShouldEqual(0);
            }

            [TestMethod]
            public void ReturnsJson_WithPersonInfoModelArrayData_WhenPeopleAreFound()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithFirstNameQueryBasedOn(term))))
                    .Returns(new[]
                    {
                        new Person(),
                        new Person(),
                        new Person(),
                    });

                var result = controller.WithFirstName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel[]>();
                var models = (PersonInfoModel[])result.Data;
                models.Length.ShouldEqual(3);
            }

            private static Expression<Func<FindPeopleWithFirstNameQuery, bool>> PeopleWithFirstNameQueryBasedOn(string term)
            {
                Expression<Func<FindPeopleWithFirstNameQuery, bool>> personQueryBasedOn = q =>
                    q.Term == term
                ;
                return personQueryBasedOn;
            }
        }

        [TestClass]
        public class TheWithLastNameMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<PersonInfoController, ActionResult>> method = m => m.WithLastName(null, StringMatchStrategy.Equals);

                var attributes = method.GetAttributes<PersonInfoController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGetPeopleWithLastName()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(term))))
                    .Returns(new Person[] { });

                controller.WithLastName(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(term))),
                        Times.Once());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithLastName(null);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(null))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsEmptyString()
            {
                var term = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithLastName(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(term))),
                        Times.Never());
            }

            [TestMethod]
            public void ExecutesNoQuery_WhenTermArgIsWhiteSpace()
            {
                const string term = " \t";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                controller.WithLastName(term);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(term))),
                        Times.Never());
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithLastName(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsEmptyString()
            {
                var term = string.Empty;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithLastName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithNullData_WhenTermArgIsWhiteSpace()
            {
                const string term = "\t ";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);

                var result = controller.WithLastName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsJson_WithEmptyData_WhenPeopleAreNotFound()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(term))))
                    .Returns(new Person[] { });

                var result = controller.WithLastName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel[]>();
                var models = (PersonInfoModel[])result.Data;
                models.Length.ShouldEqual(0);
            }

            [TestMethod]
            public void ReturnsJson_WithPersonInfoModelArrayData_WhenPeopleAreFound()
            {
                const string term = "test";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(PeopleWithLastNameQueryBasedOn(term))))
                    .Returns(new[]
                    {
                        new Person(),
                        new Person(),
                        new Person(),
                    });

                var result = controller.WithLastName(term);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldNotBeNull();
                result.Data.ShouldBeType<PersonInfoModel[]>();
                var models = (PersonInfoModel[])result.Data;
                models.Length.ShouldEqual(3);
            }

            private static Expression<Func<FindPeopleWithLastNameQuery, bool>> PeopleWithLastNameQueryBasedOn(string term)
            {
                Expression<Func<FindPeopleWithLastNameQuery, bool>> personQueryBasedOn = q =>
                    q.Term == term
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
