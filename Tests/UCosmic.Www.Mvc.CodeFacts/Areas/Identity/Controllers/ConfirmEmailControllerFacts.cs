using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentValidation.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_EnforceHttps()
            {
                var attribute = Attribute.GetCustomAttribute(
                    typeof(ConfirmEmailController),
                    typeof(EnforceHttpsAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<EnforceHttpsAttribute>();
            }
        }

        [TestClass]
        public class TheGetMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Get(Guid.Empty, null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Get(Guid.Empty, null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingConfirmEmail()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Get(Guid.Empty, null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("confirm-email");
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateConfirmEmail_UsingToken()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Get(Guid.Empty, null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, ValidateConfirmEmailAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].ParamName.ShouldEqual("token");
            }

            [TestMethod]
            public void ExecutesQuery_ForConfirmation()
            {
                var token = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(token))))
                    .Returns(null as EmailConfirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                controller.Get(token, null);

                queryProcessor.Verify(m => m.Execute(
                    It.Is(ConfirmationQueryBasedOn(token))),
                        Times.Once());
            }

            [TestMethod]
            public void Returns404_WhenConfirmation_CannotBeFound()
            {
                var token = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(token))))
                    .Returns(null as EmailConfirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(token, null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsPartialView_WhenConfirmation_IsFound()
            {
                var confirmation = new EmailConfirmation();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(confirmation.Token, null);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
            }

            [TestMethod]
            public void SetsModelProperty_SecretCode_ToMethodArgValue()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "very secret",
                };
                const string secretCode = "not as secret";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(confirmation.Token, secretCode);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)result;
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmEmailForm>();
                var model = (ConfirmEmailForm)partialView.Model;
                model.SecretCode.ShouldEqual(secretCode);
                model.SecretCode.ShouldNotEqual(confirmation.SecretCode);
            }

            [TestMethod]
            public void SetsModelProperty_IsUrlConfirmation_ToFalse_WhenMethodArgIsNull()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "very secret",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(confirmation.Token, null);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)result;
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmEmailForm>();
                var model = (ConfirmEmailForm)partialView.Model;
                model.SecretCode.ShouldBeNull();
                model.SecretCode.ShouldNotEqual(confirmation.SecretCode);
                model.IsUrlConfirmation.ShouldBeFalse();
            }

            [TestMethod]
            public void SetsModelProperty_IsUrlConfirmation_ToFalse_WhenMethodArgIsEmptyString()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "very secret",
                };
                var secretCode = String.Empty;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(confirmation.Token, secretCode);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)result;
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmEmailForm>();
                var model = (ConfirmEmailForm)partialView.Model;
                model.SecretCode.ShouldEqual(secretCode);
                model.SecretCode.ShouldNotEqual(confirmation.SecretCode);
                model.IsUrlConfirmation.ShouldBeFalse();
            }

            [TestMethod]
            public void SetsModelProperty_IsUrlConfirmation_ToFalse_WhenMethodArgIsWhiteSpace()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "very secret",
                };
                const string secretCode = " ";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(confirmation.Token, secretCode);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)result;
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmEmailForm>();
                var model = (ConfirmEmailForm)partialView.Model;
                model.SecretCode.ShouldEqual(secretCode);
                model.SecretCode.ShouldNotEqual(confirmation.SecretCode);
                model.IsUrlConfirmation.ShouldBeFalse();
            }

            [TestMethod]
            public void SetsModelProperty_IsUrlConfirmation_ToTrue_WhenMethodArgIsNotNullOrWhiteSpace()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "very secret",
                };
                const string secretCode = "not as secret";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ConfirmEmailServices(queryProcessor.Object, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Get(confirmation.Token, secretCode);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)result;
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmEmailForm>();
                var model = (ConfirmEmailForm)partialView.Model;
                model.SecretCode.ShouldEqual(secretCode);
                model.SecretCode.ShouldNotEqual(confirmation.SecretCode);
                model.IsUrlConfirmation.ShouldBeTrue();
            }
        }

        [TestClass]
        public class ThePostMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_UnitOfWork()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, UnitOfWorkAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateAntiForgeryToken()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, ValidateAntiForgeryTokenAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingUpdateAffiliation()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("confirm-email");
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateConfirmEmail_UsingModel()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, ValidateConfirmEmailAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].ParamName.ShouldEqual("model");
            }

            [TestMethod]
            public void Returns404_WhenModel_IsNull()
            {
                var services = new ConfirmEmailServices(null, null);
                var controller = new ConfirmEmailController(services);

                var result = controller.Post(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsPartialView_WhenModelState_IsInvalid()
            {
                var form = new ConfirmEmailForm
                {
                    Intent = "wrong",
                    SecretCode = "wrong",
                };
                var services = new ConfirmEmailServices(null, null);
                var controller = new ConfirmEmailController(services);
                controller.ModelState.AddModelError("error", String.Empty);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)result;
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmEmailForm>();
                var model = (ConfirmEmailForm)partialView.Model;
                model.ShouldEqual(form);
                model.Intent.ShouldEqual(form.Intent);
                model.SecretCode.ShouldEqual(form.SecretCode);
            }

            [TestMethod]
            public void ExecutesCommand_WhenAction_IsValid()
            {
                var form = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    Intent = EmailConfirmationIntent.PasswordReset,
                    SecretCode = "secret",
                };
                var commandHandler = new Mock<IHandleCommands<RedeemEmailConfirmationCommand>>(MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ConfirmationCommandBasedOn(form))));
                var services = new ConfirmEmailServices(null, commandHandler.Object);
                var controller = new ConfirmEmailController(services);

                controller.Post(form);

                commandHandler.Verify(m =>
                    m.Handle(It.Is(ConfirmationCommandBasedOn(form))),
                        Times.Once());
            }

            [TestMethod]
            public void SetsEmailConfirmationTicket_InTempData_UsingCommandValue()
            {
                var form = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    Intent = EmailConfirmationIntent.PasswordReset,
                    SecretCode = "secret",
                };
                var commandHandler = new Mock<IHandleCommands<RedeemEmailConfirmationCommand>>(MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ConfirmationCommandBasedOn(form))))
                    .Callback((RedeemEmailConfirmationCommand command) =>
                        command.Ticket = TwoFiftySixLengthString1);
                var services = new ConfirmEmailServices(null, commandHandler.Object);
                var controller = new ConfirmEmailController(services);

                controller.Post(form);

                controller.TempData.ShouldNotBeNull();
                var ticket = controller.TempData.EmailConfirmationTicket(false);
                ticket.ShouldNotBeNull();
                ticket.ShouldEqual(TwoFiftySixLengthString1);
            }

            [TestMethod]
            public void FlashesSuccessMessage_ForResetPassword_WhenIntentMatches()
            {
                var form = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    Intent = EmailConfirmationIntent.PasswordReset,
                    SecretCode = "secret",
                };
                var commandHandler = new Mock<IHandleCommands<RedeemEmailConfirmationCommand>>(MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ConfirmationCommandBasedOn(form))));
                var services = new ConfirmEmailServices(null, commandHandler.Object);
                var controller = new ConfirmEmailController(services);

                controller.Post(form);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(ConfirmEmailController.SuccessMessageForIntent
                    [EmailConfirmationIntent.PasswordReset]);
            }

            [TestMethod]
            public void FlashesSuccessMessage_ForSignUp_WhenIntentMatches()
            {
                var form = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    Intent = EmailConfirmationIntent.SignUp,
                    SecretCode = "secret",
                };
                var commandHandler = new Mock<IHandleCommands<RedeemEmailConfirmationCommand>>(MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ConfirmationCommandBasedOn(form))));
                var services = new ConfirmEmailServices(null, commandHandler.Object);
                var controller = new ConfirmEmailController(services);

                controller.Post(form);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(ConfirmEmailController.SuccessMessageForIntent
                    [EmailConfirmationIntent.SignUp]);
            }

            [TestMethod]
            public void ReturnsRedirect_ToResetPassword_WhenIntentMatches()
            {
                var form = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    Intent = EmailConfirmationIntent.PasswordReset,
                    SecretCode = "secret",
                };
                var commandHandler = new Mock<IHandleCommands<RedeemEmailConfirmationCommand>>(MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ConfirmationCommandBasedOn(form))));
                var services = new ConfirmEmailServices(null, commandHandler.Object);
                var controller = new ConfirmEmailController(services);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.Permanent.ShouldBeFalse();
                routeResult.RouteValues["area"].ShouldEqual(MVC.Passwords.Name);
                routeResult.RouteValues["controller"].ShouldEqual(MVC.Passwords.ResetPassword.Name);
                routeResult.RouteValues["action"].ShouldEqual(MVC.Passwords.ResetPassword.ActionNames.Get);
                routeResult.RouteValues["token"].ShouldEqual(form.Token);
            }

            [TestMethod]
            public void ReturnsRedirect_ToCreatePassword_WhenIntentMatches()
            {
                var form = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    Intent = EmailConfirmationIntent.SignUp,
                    SecretCode = "secret",
                };
                var commandHandler = new Mock<IHandleCommands<RedeemEmailConfirmationCommand>>(MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ConfirmationCommandBasedOn(form))));
                var services = new ConfirmEmailServices(null, commandHandler.Object);
                var controller = new ConfirmEmailController(services);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.Permanent.ShouldBeFalse();
                routeResult.RouteValues["area"].ShouldEqual(MVC.Passwords.Name);
                routeResult.RouteValues["controller"].ShouldEqual(MVC.Passwords.CreatePassword.Name);
                routeResult.RouteValues["action"].ShouldEqual(MVC.Passwords.CreatePassword.ActionNames.Get);
                routeResult.RouteValues["token"].ShouldEqual(form.Token);
            }
        }

        [TestClass]
        public class TheValidateSecretCodeMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<ConfirmEmailController, ActionResult>> method = m => m.ValidateSecretCode(null);

                var attributes = method.GetAttributes<ConfirmEmailController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void ViewModelArgument_IsDecoratedWith_CustomizeValidator_ForSecretCodeProperty()
            {
                Expression<Func<ConfirmEmailController, JsonResult>> methodExpression = m => m.ValidateSecretCode(null);
                var methodCallExpression = (MethodCallExpression)methodExpression.Body;
                var methodInfo = methodCallExpression.Method;
                var methodArg = methodInfo.GetParameters().Single();
                var attributes = methodArg.GetCustomAttributes(typeof(CustomizeValidatorAttribute), false);

                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].ShouldBeType<CustomizeValidatorAttribute>();
                var customizeValidator = (CustomizeValidatorAttribute)attributes[0];
                customizeValidator.Properties.ShouldEqual(ConfirmEmailForm.SecretCodePropertyName);
            }

            [TestMethod]
            public void ReturnsTrue_WhenModelStateIsValid()
            {
                var model = new ConfirmEmailForm();
                var controller = new ConfirmEmailController(null);

                var result = controller.ValidateSecretCode(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(true);
            }

            [TestMethod]
            public void ReturnsErrorMessage_WhenModelStateIsInvalid_ForValueProperty()
            {
                const string errorMessage = "Here is your error message.";
                var model = new ConfirmEmailForm();
                var controller = new ConfirmEmailController(null);
                controller.ModelState.AddModelError(ConfirmEmailForm.SecretCodePropertyName, errorMessage);

                var result = controller.ValidateSecretCode(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<JsonResult>();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(errorMessage);
            }
        }


        [TestClass]
        public class TheGetRedeemedRouteValuesMethod
        {
            [TestMethod]
            public void ReturnsRouteValues_ForResetPasswordIntent()
            {
                var token = Guid.NewGuid();
                const string intent = EmailConfirmationIntent.PasswordReset;

                var result = ConfirmEmailController.GetRedeemedRouteValues(token, intent);

                result.ShouldNotBeNull();
                result["area"].ShouldEqual(MVC.Passwords.Name);
                result["controller"].ShouldEqual(MVC.Passwords.ResetPassword.Name);
                result["action"].ShouldEqual(MVC.Passwords.ResetPassword.ActionNames.Get);
                result["token"].ShouldEqual(token);
            }

            [TestMethod]
            public void ReturnsRouteValues_ForSignUpIntent()
            {
                var token = Guid.NewGuid();
                const string intent = EmailConfirmationIntent.SignUp;

                var result = ConfirmEmailController.GetRedeemedRouteValues(token, intent);

                result.ShouldNotBeNull();
                result["area"].ShouldEqual(MVC.Passwords.Name);
                result["controller"].ShouldEqual(MVC.Passwords.CreatePassword.Name);
                result["action"].ShouldEqual(MVC.Passwords.CreatePassword.ActionNames.Get);
                result["token"].ShouldEqual(token);
            }

            [TestMethod]
            public void ThrowsNotSupportedException_ForUnexpectedIntent()
            {
                var token = Guid.NewGuid();
                const string intent = "unexpected";
                NotSupportedException exception = null;

                try
                {
                    ConfirmEmailController.GetRedeemedRouteValues(token, intent);
                }
                catch (NotSupportedException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Message.ShouldContain(intent);
                // ReSharper restore PossibleNullReferenceException
            }
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(Guid token)
        {
            return q => q.Token == token;
        }

        private static Expression<Func<RedeemEmailConfirmationCommand, bool>> ConfirmationCommandBasedOn(ConfirmEmailForm model)
        {
            return q => q.Token == model.Token && q.SecretCode == model.SecretCode && q.Intent == model.Intent;
        }

        private const string TwoFiftySixLengthString1 = "j2X5ZwDg6n7J8CpWy3e9H4SoLk7m4A2KiGx9a5EMr36FbRq8f5Q9Tsd2B7Ytc8N3Pzk4M6RePw36JsLx72Ebd8H5ByCc4g9N8YrGz4n5X6KqWo27QaZm3p9DTf39Aij5FSt27Ran6M8Nkp4T2Qcq9A3Sis4G8WjYz57Jxw6C9DdEg3t6HFb7f5X2PyLm4e8Z3Bro7K6MwZo25Gxq8B9Ltb4NYp4a7F2EfKs96ReJz53DiWm87Hkr2P5QnSd39Cyc";
    }
}
