using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class PersonNameControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_Authorize()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(PersonNameController), typeof(AuthorizeAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<AuthorizeAttribute>();
            }
        }

        [TestClass]
        public class TheGenerateDisplayNameMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<PersonNameController, ActionResult>> method = m => m.GenerateDisplayName(null);

                var attributes = method.GetAttributes<PersonNameController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OutputCache_UsingAllParams()
            {
                Expression<Func<PersonNameController, ActionResult>> method = m => m.GenerateDisplayName(null);

                var attributes = method.GetAttributes<PersonNameController, ActionResult, OutputCacheAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].VaryByParam.ShouldEqual("*");
            }

            [TestMethod]
            public void IsDecoratedWith_OutputCache_UsingServerLocation()
            {
                Expression<Func<PersonNameController, ActionResult>> method = m => m.GenerateDisplayName(null);

                var attributes = method.GetAttributes<PersonNameController, ActionResult, OutputCacheAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Location.ShouldEqual(OutputCacheLocation.Server);
            }

            [TestMethod]
            public void ExecutesQuery_ToGenerateDisplayName()
            {
                var model = new GenerateDisplayNameForm
                {
                    Salutation = "Mr",
                    FirstName = "Adam",
                    MiddleName = "B",
                    LastName = "West",
                    Suffix = "Sr.",
                };
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                Expression<Func<GenerateDisplayNameQuery, bool>> generateDisplayNameBasedOnModel = q =>
                    q.Salutation == model.Salutation &&
                    q.FirstName == model.FirstName &&
                    q.MiddleName == model.MiddleName &&
                    q.LastName == model.LastName &&
                    q.Suffix == model.Suffix
                ;
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(generateDisplayNameBasedOnModel)))
                    .Returns("derived display name");

                controller.GenerateDisplayName(model);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(generateDisplayNameBasedOnModel)),
                        Times.Once());
            }

            [TestMethod]
            public void ReturnsJson_WithGeneratedDisplayName()
            {
                const string generatedDisplayName = "generated display name";
                var model = new GenerateDisplayNameForm
                {
                    Salutation = "Mr",
                    FirstName = "Adam",
                    MiddleName = "B",
                    LastName = "West",
                    Suffix = "Sr.",
                };
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                Expression<Func<GenerateDisplayNameQuery, bool>> generateDisplayNameBasedOnModel = q =>
                    q.Salutation == model.Salutation &&
                    q.FirstName == model.FirstName &&
                    q.MiddleName == model.MiddleName &&
                    q.LastName == model.LastName &&
                    q.Suffix == model.Suffix
                ;
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(generateDisplayNameBasedOnModel)))
                    .Returns(generatedDisplayName);

                var result = controller.GenerateDisplayName(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldEqual(generatedDisplayName);
            }
        }

        [TestClass]
        public class TheAutoCompleteSalutationsMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<PersonNameController, ActionResult>> method = m => m.AutoCompleteSalutations(null);

                var attributes = method.GetAttributes<PersonNameController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OutputCache_UsingAllParams()
            {
                Expression<Func<PersonNameController, ActionResult>> method = m => m.AutoCompleteSalutations(null);

                var attributes = method.GetAttributes<PersonNameController, ActionResult, OutputCacheAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].VaryByParam.ShouldEqual("*");
            }

            [TestMethod]
            public void IsDecoratedWith_OutputCache_UsingServerLocation()
            {
                Expression<Func<PersonNameController, ActionResult>> method = m => m.AutoCompleteSalutations(null);

                var attributes = method.GetAttributes<PersonNameController, ActionResult, OutputCacheAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Location.ShouldEqual(OutputCacheLocation.Server);
            }

            [TestMethod]
            public void ExecutesQuery_ToFindDistinctSalutations()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<FindDistinctSalutationsQuery>()))
                    .Returns(null as string[]);

                controller.AutoCompleteSalutations(null);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.IsAny<FindDistinctSalutationsQuery>()),
                        Times.Once());
            }

            [TestMethod]
            public void ReturnsJson_WithIEnumerableData_AllowingGet()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<FindDistinctSalutationsQuery>()))
                    .Returns(null as string[]);

                var result = controller.AutoCompleteSalutations(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldImplement<IEnumerable<object>>();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.AllowGet);
            }

            [TestMethod]
            public void ReturnsJson_WithIEnumerable_IncludingNullValueLabel_AsTopResult()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<FindDistinctSalutationsQuery>()))
                    .Returns(null as string[]);

                var result = controller.AutoCompleteSalutations(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldImplement<IEnumerable<object>>();
                var enumerable = (IEnumerable<object>)result.Data;
                dynamic item = enumerable.First();
                string value = item.value;
                string label = item.label;
                value.ShouldEqual(string.Empty);
                label.ShouldEqual(PersonNameController.SalutationAndSuffixNullValueLabel);
            }

            [TestMethod]
            public void ReturnsJson_WithIEnumerable_IncludingAllDefaultValues()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<FindDistinctSalutationsQuery>()))
                    .Returns(null as string[]);

                var result = controller.AutoCompleteSalutations(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldImplement<IEnumerable<object>>();
                var enumerable = (IEnumerable<object>)result.Data;
                foreach (var anonymous in enumerable)
                {
                    dynamic item = anonymous;
                    string value = item.value;
                    string label = item.label;
                    if (label == PersonNameController.SalutationAndSuffixNullValueLabel) continue;
                    PersonNameController.DefaultSalutationValues.ShouldContain(value);
                    PersonNameController.DefaultSalutationValues.ShouldContain(label);
                }
            }

            [TestMethod]
            public void ReturnsJson_WithIEnumerable_IncludingNonDefaultValues()
            {
                var databaseValues = new[] { "S1", "S2" };
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<FindDistinctSalutationsQuery>()))
                    .Returns(databaseValues);

                var result = controller.AutoCompleteSalutations(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldImplement<IEnumerable<object>>();
                var enumerable = (IEnumerable<object>)result.Data;
                foreach (var anonymous in enumerable)
                {
                    dynamic item = anonymous;
                    string value = item.value;
                    string label = item.label;
                    if (label == PersonNameController.SalutationAndSuffixNullValueLabel) continue;
                    if (PersonNameController.DefaultSalutationValues.Contains(value)) continue;
                    databaseValues.ShouldContain(value);
                    databaseValues.ShouldContain(label);
                }
            }
        }

        private class ScenarioOptions
        {
            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
        }

        private static PersonNameController CreateController(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();

            scenarioOptions.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            var services = new PersonNameServices(scenarioOptions.MockQueryProcessor.Object);

            var controller = new PersonNameController(services);

            return controller;
        }
    }
}
