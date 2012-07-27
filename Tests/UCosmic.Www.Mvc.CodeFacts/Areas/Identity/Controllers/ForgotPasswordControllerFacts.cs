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
    public class ForgotPasswordControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Extends_BaseController()
            {
                var controller = new ForgotPasswordController(null);
                controller.ShouldImplement<BaseController>();
            }
        }

        [TestClass]
        public class TheGetMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingForgotPassword()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("forgot-password");
            }

            [TestMethod]
            public void IsDecoratedWith_ReturnUrlReferrer_UsingSignOn()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Get();

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, ReturnUrlReferrerAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Fallback.ShouldEqual(SignInRouter.Get.Route);
            }

            [TestMethod]
            public void ReturnsPartialView_WithEmptyModel()
            {
                var controller = new ForgotPasswordController(null);

                var result = controller.Get();

                result.ShouldNotBeNull();
                result.Model.ShouldNotBeNull();
                result.Model.ShouldBeType<ForgotPasswordForm>();
                var model = (ForgotPasswordForm)result.Model;
                model.EmailAddress.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePostMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_UnitOfWork()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, UnitOfWorkAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateAntiForgeryToken()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, ValidateAntiForgeryTokenAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingForgotPassword()
            {
                Expression<Func<ForgotPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ForgotPasswordController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("forgot-password");
            }

            [TestMethod]
            public void Returns404_WhenModel_IsNull()
            {
                var controller = new ForgotPasswordController(null);

                var result = controller.Post(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnView_WhenModelState_IsInvalid()
            {
                var form = new ForgotPasswordForm
                {
                    EmailAddress = "wrong",
                };
                var controller = new ForgotPasswordController(null);
                controller.ModelState.AddModelError("error", string.Empty);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ForgotPasswordForm>();
                var model = (ForgotPasswordForm)viewResult.Model;
                model.ShouldEqual(form);
                model.EmailAddress.ShouldEqual(form.EmailAddress);
            }

            [TestMethod]
            public void ExecutesCommand_WhenAction_IsValid()
            {
                var form = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var commandHandler = new Mock<IHandleCommands<SendConfirmEmailMessageCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(SendCommandBasedOn(form))));
                var services = new ForgotPasswordServices(commandHandler.Object);
                var controller = new ForgotPasswordController(services);
                ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper)
                    .InitializeController(controller);

                controller.Post(form);

                commandHandler.Verify(m =>
                    m.Handle(It.Is(SendCommandBasedOn(form))),
                        Times.Once());
            }

            [TestMethod]
            public void FlashesSuccessMessage_UsingModelProperty_EmailAddress()
            {
                var form = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var commandHandler = new Mock<IHandleCommands<SendConfirmEmailMessageCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(SendCommandBasedOn(form))));
                var services = new ForgotPasswordServices(commandHandler.Object);
                var controller = new ForgotPasswordController(services);
                ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper)
                    .InitializeController(controller);

                controller.Post(form);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(string.Format(
                    ForgotPasswordController.SuccessMessageFormat,
                        form.EmailAddress));
            }

            [TestMethod]
            public void ReturnsRedirect_ToConfirmEmail_WithCommandToken()
            {
                var form = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var commandHandler = new Mock<IHandleCommands<SendConfirmEmailMessageCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(SendCommandBasedOn(form))));
                var services = new ForgotPasswordServices(commandHandler.Object);
                var controller = new ForgotPasswordController(services);
                ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper)
                    .InitializeController(controller);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.Permanent.ShouldBeFalse();
                routeResult.RouteValues["area"].ShouldEqual(MVC.Identity.Name);
                routeResult.RouteValues["controller"].ShouldEqual(MVC.Identity.ConfirmEmail.Name);
                routeResult.RouteValues["action"].ShouldEqual(MVC.Identity.ConfirmEmail.ActionNames.Get);
                routeResult.RouteValues["token"].ShouldEqual(Guid.Empty);
            }

            private static Expression<Func<SendConfirmEmailMessageCommand, bool>> SendCommandBasedOn(ForgotPasswordForm model)
            {
                return q => q.EmailAddress == model.EmailAddress && q.Intent == EmailConfirmationIntent.ResetPassword;
            }
        }
    }
}
