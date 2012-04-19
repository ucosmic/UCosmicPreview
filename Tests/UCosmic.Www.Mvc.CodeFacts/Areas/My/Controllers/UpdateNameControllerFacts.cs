using System;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class UpdateNameControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_Authorize()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(UpdateNameController), typeof(AuthorizeAttribute));

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
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_NullLayoutOnChildAction()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, NullLayoutOnChildActionAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingUpdateName()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("update-name");
            }

            [TestMethod]
            public void ThrowsNullReferenceException_WhenUserIsNull()
            {
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<GetEmailAddressByUserNameAndNumberQuery>()))
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
                    UserIdentityName = userName,
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
                    UserIdentityName = userName,
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
            public void ReturnsPartialView_WhenUser_IsFound()
            {
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userName,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery =
                    query => query.Name == userName;
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(userByNameQuery)))
                    .Returns(new User { Person = new Person() });

                var result = controller.Get();

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialViewResult = (PartialViewResult)result;
                partialViewResult.Model.ShouldNotBeNull();
                partialViewResult.Model.ShouldBeType<UpdateNameForm>();
            }
        }

        [TestClass]
        public class ThePutMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPut()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, HttpPutAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_UnitOfWork()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, UnitOfWorkAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingUpdateName()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("update-name");
            }

            [TestMethod]
            public void Returns404_WhenModel_IsNull()
            {
                var controller = CreateController();

                var result = controller.Put(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsPartialView_WhenModelState_IsInvalid()
            {
                var scenarioOptions = new ScenarioOptions();
                var model = new UpdateNameForm();
                var controller = CreateController(scenarioOptions);
                controller.ModelState.AddModelError("error", "message");

                var result = controller.Put(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialViewResult = (PartialViewResult)result;
                partialViewResult.Model.ShouldNotBeNull();
                partialViewResult.Model.ShouldBeType<UpdateNameForm>();
                partialViewResult.Model.ShouldEqual(model);
            }

            [TestMethod]
            public void SetsCommandPrincipalProperty()
            {
                const string principalIdentityName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = principalIdentityName,
                };
                var model = new UpdateNameForm
                {
                    DisplayName = "test",
                    IsDisplayNameDerived = false,
                    FirstName = "first",
                    LastName = "last",
                };
                var controller = CreateController(scenarioOptions);
                Expression<Func<UpdateNameCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.DisplayName == model.DisplayName &&
                    command.IsDisplayNameDerived == model.IsDisplayNameDerived &&
                    command.Salutation == model.Salutation &&
                    command.FirstName == model.FirstName &&
                    command.MiddleName == model.MiddleName &&
                    command.LastName == model.LastName &&
                    command.Suffix == model.Suffix
                ;
                UpdateNameCommand executedCommand = null;
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(It.Is(commandDerivedFromModel)))
                    .Callback((UpdateNameCommand command) => executedCommand = command);

                controller.Put(model);

                executedCommand.Principal.ShouldEqual(scenarioOptions.MockPrincipal.Object);
                executedCommand.Principal.Identity.Name.ShouldEqual(principalIdentityName);
            }

            [TestMethod]
            public void ExecutesCommand_WhenAction_IsValid()
            {
                var scenarioOptions = new ScenarioOptions();
                var model = new UpdateNameForm
                {
                    DisplayName = "test",
                    IsDisplayNameDerived = false,
                    FirstName = "first",
                    LastName = "last",
                };
                var controller = CreateController(scenarioOptions);
                Expression<Func<UpdateNameCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.DisplayName == model.DisplayName &&
                    command.IsDisplayNameDerived == model.IsDisplayNameDerived &&
                    command.Salutation == model.Salutation &&
                    command.FirstName == model.FirstName &&
                    command.MiddleName == model.MiddleName &&
                    command.LastName == model.LastName &&
                    command.Suffix == model.Suffix
                ;
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(It.Is(commandDerivedFromModel)));

                controller.Put(model);

                scenarioOptions.MockCommandHandler.Verify(m =>
                    m.Handle(It.Is(commandDerivedFromModel)),
                        Times.Once());
            }

            [TestMethod]
            public void FlashesSuccessMessage_WhenCommand_ChangedState()
            {
                var scenarioOptions = new ScenarioOptions();
                var model = new UpdateNameForm
                {
                    DisplayName = "test",
                    IsDisplayNameDerived = false,
                    FirstName = "first",
                    LastName = "last",
                };
                var controller = CreateController(scenarioOptions);
                Expression<Func<UpdateNameCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.DisplayName == model.DisplayName &&
                    command.IsDisplayNameDerived == model.IsDisplayNameDerived &&
                    command.Salutation == model.Salutation &&
                    command.FirstName == model.FirstName &&
                    command.MiddleName == model.MiddleName &&
                    command.LastName == model.LastName &&
                    command.Suffix == model.Suffix
                ;
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(It.Is(commandDerivedFromModel)))
                    .Callback((UpdateNameCommand command) => command.ChangeCount = 1);

                controller.Put(model);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(UpdateNameController.SuccessMessage);
            }

            [TestMethod]
            public void FlashesNoChangesMessage_WhenCommand_ChangedState()
            {
                var scenarioOptions = new ScenarioOptions();
                var model = new UpdateNameForm
                {
                    DisplayName = "test",
                    IsDisplayNameDerived = false,
                    FirstName = "first",
                    LastName = "last",
                };
                var controller = CreateController(scenarioOptions);
                Expression<Func<UpdateNameCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.DisplayName == model.DisplayName &&
                    command.IsDisplayNameDerived == model.IsDisplayNameDerived &&
                    command.Salutation == model.Salutation &&
                    command.FirstName == model.FirstName &&
                    command.MiddleName == model.MiddleName &&
                    command.LastName == model.LastName &&
                    command.Suffix == model.Suffix
                ;
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(It.Is(commandDerivedFromModel)));

                controller.Put(model);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(UpdateNameController.NoChangesMessage);
            }

            [TestMethod]
            public void ReturnsRedirect_ToStaticReturnUrl_AfterCommandIsExecuted()
            {
                var scenarioOptions = new ScenarioOptions();
                var model = new UpdateNameForm
                {
                    DisplayName = "test",
                    IsDisplayNameDerived = false,
                    FirstName = "first",
                    LastName = "last",
                };
                var controller = CreateController(scenarioOptions);
                Expression<Func<UpdateNameCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.DisplayName == model.DisplayName &&
                    command.IsDisplayNameDerived == model.IsDisplayNameDerived &&
                    command.Salutation == model.Salutation &&
                    command.FirstName == model.FirstName &&
                    command.MiddleName == model.MiddleName &&
                    command.LastName == model.LastName &&
                    command.Suffix == model.Suffix
                ;
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(It.Is(commandDerivedFromModel)));

                var result = controller.Put(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectResult>();
                var redirectResult = (RedirectResult)result;
                redirectResult.Url.ShouldEqual(UpdateNameForm.ReturnUrl);
                redirectResult.Permanent.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheGenerateDisplayNameMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.GenerateDisplayName(null, null, null, null, null);

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OutputCache()
            {
                Expression<Func<UpdateNameController, ActionResult>> method = m => m.GenerateDisplayName(null, null, null, null, null);

                var attributes = method.GetAttributes<UpdateNameController, ActionResult, OutputCacheAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGenerateDisplayName()
            {
                const string salutation = "Mr";
                const string firstName = "Adam";
                const string middleName = "B";
                const string lastName = "West";
                const string suffix = "Sr.";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                Expression<Func<GenerateDisplayNameQuery, bool>> generateDisplayNameBasedOnArgs = q =>
                    q.Salutation == salutation &&
                    q.FirstName == firstName &&
                    q.MiddleName == middleName &&
                    q.LastName == lastName &&
                    q.Suffix == suffix
                ;
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(generateDisplayNameBasedOnArgs)))
                    .Returns("derived display name");

                controller.GenerateDisplayName(salutation, firstName, middleName, lastName, suffix);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(generateDisplayNameBasedOnArgs)), 
                        Times.Once());
            }

            [TestMethod]
            public void ReturnsJson_WithGeneratedDisplayName()
            {
                const string generatedDisplayName = "generated display name";
                const string salutation = "Mr";
                const string firstName = "Adam";
                const string middleName = "B";
                const string lastName = "West";
                const string suffix = "Sr.";
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateController(scenarioOptions);
                Expression<Func<GenerateDisplayNameQuery, bool>> generateDisplayNameBasedOnArgs = q =>
                    q.Salutation == salutation &&
                    q.FirstName == firstName &&
                    q.MiddleName == middleName &&
                    q.LastName == lastName &&
                    q.Suffix == suffix
                ;
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(generateDisplayNameBasedOnArgs)))
                    .Returns(generatedDisplayName);

                var result = controller.GenerateDisplayName(salutation, firstName, middleName, lastName, suffix);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.Data.ShouldEqual(generatedDisplayName);
            }
        }

        private class ScenarioOptions
        {
            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
            internal Mock<IPrincipal> MockPrincipal { get; set; }
            internal Mock<IHandleCommands<UpdateNameCommand>> MockCommandHandler { get; set; }
            internal string UserIdentityName { get; set; }
        }

        private static UpdateNameController CreateController(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();

            scenarioOptions.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            scenarioOptions.MockCommandHandler = new Mock<IHandleCommands<UpdateNameCommand>>(MockBehavior.Strict);

            var services = new UpdateNameServices(scenarioOptions.MockQueryProcessor.Object, scenarioOptions.MockCommandHandler.Object);

            var controller = new UpdateNameController(services);

            var builder = ReuseMock.TestControllerBuilder();

            builder.HttpContext.User = null;
            if (!string.IsNullOrWhiteSpace(scenarioOptions.UserIdentityName))
            {
                var identity = new Mock<IIdentity>();
                identity.Setup(p => p.Name).Returns(scenarioOptions.UserIdentityName);
                scenarioOptions.MockPrincipal = new Mock<IPrincipal>();
                scenarioOptions.MockPrincipal.Setup(p => p.Identity).Returns(identity.Object);
                builder.HttpContext.User = scenarioOptions.MockPrincipal.Object;
            }

            builder.InitializeController(controller);

            return controller;
        }
    }
}
