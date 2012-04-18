using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web.Mvc;
using FluentValidation.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses;
using UCosmic.Www.Mvc.Areas.My.Services;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class EmailAddressesControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_Authorize()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(EmailAddressesController), typeof(AuthorizeAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<AuthorizeAttribute>();
            }
        }

        [TestClass]
        public class TheChangeSpellingGetMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(1);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(1);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingChangeSpelling()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(1);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("change-spelling");
            }

            [TestMethod]
            public void IsDecoratedWith_ReturnUrlReferrer_UsingMyProfile()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(1);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, ReturnUrlReferrerAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Fallback.ShouldEqual(SelfRouteMapper.Me.OutboundRoute);
            }

            [TestMethod]
            public void ThrowsNullReferenceException_WhenUserIsNull()
            {
                const int number = 2;
                var scenarioOptions = new ScenarioOptions();
                var controller = CreateEmailAddressesController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<GetEmailAddressByUserNameAndNumberQuery>()))
                    .Returns(null as EmailAddress);
                NullReferenceException exception = null;

                try
                {
                    controller.ChangeSpelling(number);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ForEmailAddress_ByUserNameAndNumber()
            {
                const int number = 2;
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userName,
                };
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressByUserNameAndNumberQuery =
                    query => query.UserName == userName && query.Number == number;
                var controller = CreateEmailAddressesController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumberQuery)))
                    .Returns(new EmailAddress());

                controller.ChangeSpelling(number);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(emailAddressByUserNameAndNumberQuery)),
                        Times.Once());
            }

            [TestMethod]
            public void Returns404_WhenEmailAddress_CannotBeFound()
            {
                const int number = 2;
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userName,
                };
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressByUserNameAndNumberQuery =
                    query => query.UserName == userName && query.Number == number;
                var controller = CreateEmailAddressesController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumberQuery)))
                    .Returns(null as EmailAddress);

                var result = controller.ChangeSpelling(number);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsView_WhenEmailAddress_IsFound()
            {
                const int number = 2;
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userName,
                };
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressByUserNameAndNumberQuery =
                    query => query.UserName == userName && query.Number == number;
                var controller = CreateEmailAddressesController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumberQuery)))
                    .Returns(new EmailAddress());

                var result = controller.ChangeSpelling(number);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ChangeSpellingForm>();
            }
        }

        [TestClass]
        public class TheChangeSpellingPutMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPut()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(null);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, HttpPutAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_UnitOfWork()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(null);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, UnitOfWorkAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(1);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingChangeSpelling()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ChangeSpelling(1);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("change-spelling");
            }

            [TestMethod]
            public void Returns404_WhenModel_IsNull()
            {
                var controller = CreateEmailAddressesController();

                var result = controller.ChangeSpelling(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Returns404_WhenPersonUserName_IsNotEqualTo_UserIdentityName()
            {
                const string userIdentityName = "user@domain.tld";
                const string personUserName = "user@domain2.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userIdentityName,
                };
                var model = new ChangeSpellingForm
                {
                    PersonUserName = personUserName,
                };
                var controller = CreateEmailAddressesController(scenarioOptions);

                var result = controller.ChangeSpelling(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsView_WhenModelState_IsInvalid()
            {
                const string userIdentityName = "user@domain.tld";
                const string personUserName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userIdentityName,
                };
                var model = new ChangeSpellingForm
                {
                    PersonUserName = personUserName,
                };
                var controller = CreateEmailAddressesController(scenarioOptions);
                controller.ModelState.AddModelError("error", "message");

                var result = controller.ChangeSpelling(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ChangeSpellingForm>();
                viewResult.Model.ShouldEqual(model);
            }

            [TestMethod]
            public void ExecutesCommand_WhenAction_IsValid()
            {
                const int number = 2;
                const string personUserName = "user@domain.tld";
                const string newValue = "User@domain.tld";
                const string userIdentityName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userIdentityName,
                };
                var model = new ChangeSpellingForm
                {
                    Number = number,
                    PersonUserName = personUserName,
                    Value = newValue,
                    ReturnUrl = "https://www.site.com/"
                };
                var controller = CreateEmailAddressesController(scenarioOptions);
                Expression<Func<ChangeEmailAddressSpellingCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.Number == number &&
                    command.UserName == personUserName &&
                    command.NewValue == newValue
                ;
                scenarioOptions.MockChangeSpelling.Setup(m => m.Handle(It.Is(commandDerivedFromModel)));

                controller.ChangeSpelling(model);

                scenarioOptions.MockChangeSpelling.Verify(m =>
                    m.Handle(It.Is(commandDerivedFromModel)),
                        Times.Once());
            }

            [TestMethod]
            public void FlashesSuccessMessage_WhenCommand_ChangedState()
            {
                const int number = 2;
                const string personUserName = "user@domain.tld";
                const string newValue = "User@domain.tld";
                const string userIdentityName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userIdentityName,
                };
                var model = new ChangeSpellingForm
                {
                    Number = number,
                    PersonUserName = personUserName,
                    Value = newValue,
                    ReturnUrl = "https://www.site.com/"
                };
                var controller = CreateEmailAddressesController(scenarioOptions);
                Expression<Func<ChangeEmailAddressSpellingCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.Number == number &&
                    command.UserName == personUserName &&
                    command.NewValue == newValue
                ;
                scenarioOptions.MockChangeSpelling.Setup(m => m.Handle(It.Is(commandDerivedFromModel)))
                    .Callback((ChangeEmailAddressSpellingCommand command) => command.ChangedState = true);

                controller.ChangeSpelling(model);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(string.Format(EmailAddressesController.ChangeSpellingSuccessMessageFormat, model.Value));
            }

            [TestMethod]
            public void FlashesNoChangesMessage_WhenCommand_ChangedState()
            {
                const int number = 2;
                const string personUserName = "user@domain.tld";
                const string newValue = "User@domain.tld";
                const string userIdentityName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userIdentityName,
                };
                var model = new ChangeSpellingForm
                {
                    Number = number,
                    PersonUserName = personUserName,
                    Value = newValue,
                    ReturnUrl = "https://www.site.com/"
                };
                var controller = CreateEmailAddressesController(scenarioOptions);
                Expression<Func<ChangeEmailAddressSpellingCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.Number == number &&
                    command.UserName == personUserName &&
                    command.NewValue == newValue
                ;
                scenarioOptions.MockChangeSpelling.Setup(m => m.Handle(It.Is(commandDerivedFromModel)));

                controller.ChangeSpelling(model);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(EmailAddressesController.ChangeSpellingNoChangesMessage);
            }

            [TestMethod]
            public void ReturnsRedirect_ToModelReturnUrl_AfterCommandIsExecuted()
            {
                const int number = 2;
                const string personUserName = "user@domain.tld";
                const string newValue = "User@domain.tld";
                const string userIdentityName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    UserIdentityName = userIdentityName,
                };
                var model = new ChangeSpellingForm
                {
                    Number = number,
                    PersonUserName = personUserName,
                    Value = newValue,
                    ReturnUrl = "https://www.site.com/"
                };
                var controller = CreateEmailAddressesController(scenarioOptions);
                Expression<Func<ChangeEmailAddressSpellingCommand, bool>> commandDerivedFromModel =
                    command =>
                    command.Number == number &&
                    command.UserName == personUserName &&
                    command.NewValue == newValue
                ;
                scenarioOptions.MockChangeSpelling.Setup(m => m.Handle(It.Is(commandDerivedFromModel)));

                var result = controller.ChangeSpelling(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectResult>();
                var redirectResult = (RedirectResult) result;
                redirectResult.Url.ShouldEqual(model.ReturnUrl);
                redirectResult.Permanent.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheValidateChangeSpellingPostMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ValidateChangeSpelling(null);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OutputCache()
            {
                Expression<Func<EmailAddressesController, ActionResult>> method = m => m.ValidateChangeSpelling(null);

                var attributes = method.GetAttributes<EmailAddressesController, ActionResult, OutputCacheAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ViewModelArgument_IsDecoratedWith_CustomizeValidator_ForValueProperty()
            {
                Expression<Func<EmailAddressesController, JsonResult>> methodExpression = m => m.ValidateChangeSpelling(null);
                var methodCallExpression = (MethodCallExpression)methodExpression.Body;
                var methodInfo = methodCallExpression.Method;
                var methodArg = methodInfo.GetParameters().Single();
                var attributes = methodArg.GetCustomAttributes(typeof(CustomizeValidatorAttribute), false);

                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].ShouldBeType<CustomizeValidatorAttribute>();
                var customizeValidator = (CustomizeValidatorAttribute) attributes[0];
                customizeValidator.Properties.ShouldEqual(ChangeSpellingForm.ValuePropertyName);
            }

            [TestMethod]
            public void ReturnsTrue_WhenModelStateIsValid()
            {
                var model = new ChangeSpellingForm();
                var controller = CreateEmailAddressesController();

                var result = controller.ValidateChangeSpelling(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(true);
            }

            [TestMethod]
            public void ReturnsErrorMessage_WhenModelStateIsInvalid_ForValueProperty()
            {
                const string errorMessage = "Here is your error message.";
                var model = new ChangeSpellingForm();
                var controller = CreateEmailAddressesController();
                controller.ModelState.AddModelError(ChangeSpellingForm.ValuePropertyName, errorMessage);

                var result = controller.ValidateChangeSpelling(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(errorMessage);
            }
        }

        private class ScenarioOptions
        {
            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
            internal Mock<IHandleCommands<ChangeEmailAddressSpellingCommand>> MockChangeSpelling { get; set; }
            internal string UserIdentityName { get; set; }
        }

        private static EmailAddressesController CreateEmailAddressesController(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();

            scenarioOptions.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            scenarioOptions.MockChangeSpelling = new Mock<IHandleCommands<ChangeEmailAddressSpellingCommand>>(MockBehavior.Strict);

            var services = new EmailAddressesServices(scenarioOptions.MockQueryProcessor.Object, scenarioOptions.MockChangeSpelling.Object);

            var controller = new EmailAddressesController(services);

            var builder = ReuseMock.TestControllerBuilder();

            builder.HttpContext.User = null;
            if (!string.IsNullOrWhiteSpace(scenarioOptions.UserIdentityName))
            {
                var identity = new Mock<IIdentity>();
                identity.Setup(p => p.Name).Returns(scenarioOptions.UserIdentityName);
                var principal = new Mock<IPrincipal>();
                principal.Setup(p => p.Identity).Returns(identity.Object);
                builder.HttpContext.User = principal.Object;
            }

            builder.InitializeController(controller);

            return controller;
        }
    }
}
