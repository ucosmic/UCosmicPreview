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
    public class ResetPasswordControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_EnforceHttps()
            {
                var attribute = Attribute.GetCustomAttribute(
                    typeof(ResetPasswordController),
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
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Get(Guid.Empty);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Get(Guid.Empty);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingResetPassword()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Get(Guid.Empty);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("reset-password");
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateRedeemTicket_UsingToken_AndResetPassword()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Get(Guid.Empty);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, ValidateRedeemTicketAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].ParamName.ShouldEqual("token");
                attributes[0].Intent.ShouldEqual(EmailConfirmationIntent.PasswordReset);
            }

            [TestMethod]
            public void ExecutesQuery_ForConfirmation()
            {
                var token = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(token))))
                    .Returns(null as EmailConfirmation);
                var services = new ResetPasswordServices(queryProcessor.Object, null);
                var controller = new ResetPasswordController(services);

                controller.Get(token);

                queryProcessor.Verify(m => m.Execute(
                    It.Is(ConfirmationQueryBasedOn(token))),
                        Times.Once());
            }

            [TestMethod]
            public void Returns404_WhenToken_IsEmptyGuid()
            {
                var token = Guid.Empty;
                var controller = new ResetPasswordController(null);

                var result = controller.Get(token);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Returns404_WhenConfirmation_CannotBeFound()
            {
                var token = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(token))))
                    .Returns(null as EmailConfirmation);
                var services = new ResetPasswordServices(queryProcessor.Object, null);
                var controller = new ResetPasswordController(services);

                var result = controller.Get(token);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsView_WithModel_WhenConfirmation_IsFound()
            {
                var confirmation = new EmailConfirmation();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var services = new ResetPasswordServices(queryProcessor.Object, null);
                var controller = new ResetPasswordController(services);

                var result = controller.Get(confirmation.Token);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ResetPasswordForm>();
                var model = (ResetPasswordForm)viewResult.Model;
                model.Token.ShouldEqual(confirmation.Token);
                model.Password.ShouldBeNull();
                model.PasswordConfirmation.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePostMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_UnitOfWork()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, UnitOfWorkAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateAntiForgeryToken()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, ValidateAntiForgeryTokenAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingResetPassword()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("reset-password");
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ValidateRedeemTicket_UsingModel()
            {
                Expression<Func<ResetPasswordController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<ResetPasswordController, ActionResult, ValidateRedeemTicketAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].ParamName.ShouldEqual("model");
                attributes[0].Intent.ShouldEqual(EmailConfirmationIntent.PasswordReset);
            }

            [TestMethod]
            public void Returns404_WhenModel_IsNull()
            {
                var services = new ResetPasswordServices(null, null);
                var controller = new ResetPasswordController(services);

                var result = controller.Post(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsView_WhenModelState_IsInvalid()
            {
                var form = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                };
                var services = new ResetPasswordServices(null, null);
                var controller = new ResetPasswordController(services);
                controller.ModelState.AddModelError("error", String.Empty);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ResetPasswordForm>();
                var model = (ResetPasswordForm)viewResult.Model;
                model.ShouldEqual(form);
                model.Token.ShouldEqual(form.Token);
            }

            [TestMethod]
            public void ExecutesCommand_WhenAction_IsValid()
            {
                var form = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                };
                var commandHandler = new Mock<IHandleCommands<ResetPasswordCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ResetCommandBasedOn(form, TwoFiftySixLengthString1))));
                var services = new ResetPasswordServices(null, commandHandler.Object);
                var controller = new ResetPasswordController(services);
                controller.TempData.EmailConfirmationTicket(TwoFiftySixLengthString1);

                controller.Post(form);

                commandHandler.Verify(m =>
                    m.Handle(It.Is(ResetCommandBasedOn(form, TwoFiftySixLengthString1))),
                        Times.Once());
            }

            [TestMethod]
            public void SetsCommandValue_Ticket_UsingTempDataValue()
            {
                var form = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                    Password = "password",
                    PasswordConfirmation = "password",
                };
                ResetPasswordCommand outCommand = null;
                var commandHandler = new Mock<IHandleCommands<ResetPasswordCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ResetCommandBasedOn(form, TwoFiftySixLengthString2))))
                    .Callback((ResetPasswordCommand command) => outCommand = command);
                var services = new ResetPasswordServices(null, commandHandler.Object);
                var controller = new ResetPasswordController(services);
                controller.TempData.EmailConfirmationTicket(TwoFiftySixLengthString2);

                controller.Post(form);

                outCommand.Ticket.ShouldEqual(TwoFiftySixLengthString2);
            }

            [TestMethod]
            public void ClearsTempDataValue_Ticket()
            {
                var form = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                    Password = "password",
                    PasswordConfirmation = "password",
                };
                var commandHandler = new Mock<IHandleCommands<ResetPasswordCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ResetCommandBasedOn(form, TwoFiftySixLengthString3))));
                var services = new ResetPasswordServices(null, commandHandler.Object);
                var controller = new ResetPasswordController(services);
                controller.TempData.EmailConfirmationTicket(TwoFiftySixLengthString3);
                controller.TempData.EmailConfirmationTicket().ShouldNotBeNull();

                controller.Post(form);

                controller.TempData.EmailConfirmationTicket().ShouldBeNull();
            }

            [TestMethod]
            public void FlashesSuccessMessage()
            {
                var form = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                    Password = "password",
                    PasswordConfirmation = "password",
                };
                var commandHandler = new Mock<IHandleCommands<ResetPasswordCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ResetCommandBasedOn(form, TwoFiftySixLengthString1))));
                var services = new ResetPasswordServices(null, commandHandler.Object);
                var controller = new ResetPasswordController(services);
                controller.TempData.EmailConfirmationTicket(TwoFiftySixLengthString1);

                controller.Post(form);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(ResetPasswordController.SuccessMessage);
            }

            [TestMethod]
            public void ReturnsRedirect_ToSignIn()
            {
                var form = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                    Password = "password",
                    PasswordConfirmation = "password",
                };
                var commandHandler = new Mock<IHandleCommands<ResetPasswordCommand>>
                    (MockBehavior.Strict);
                commandHandler.Setup(m => m.Handle(It.Is(ResetCommandBasedOn(form, TwoFiftySixLengthString2))));
                var services = new ResetPasswordServices(null, commandHandler.Object);
                var controller = new ResetPasswordController(services);
                controller.TempData.EmailConfirmationTicket(TwoFiftySixLengthString2);

                var result = controller.Post(form);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.Permanent.ShouldBeFalse();
                routeResult.RouteValues["area"].ShouldEqual(MVC.Identity.Name);
                routeResult.RouteValues["controller"].ShouldEqual(MVC.Identity.SignIn.Name);
                routeResult.RouteValues["action"].ShouldEqual(MVC.Identity.SignIn.ActionNames.Get);
            }
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(Guid token)
        {
            return q => q.Token == token;
        }

        private static Expression<Func<ResetPasswordCommand, bool>> ResetCommandBasedOn(ResetPasswordForm model, string ticket)
        {
            return q =>
                q.Token == model.Token &&
                q.Password == model.Password &&
                q.PasswordConfirmation == model.PasswordConfirmation &&
                q.Ticket == ticket
            ;
        }

        private const string TwoFiftySixLengthString1 = "j2X5ZwDg6n7J8CpWy3e9H4SoLk7m4A2KiGx9a5EMr36FbRq8f5Q9Tsd2B7Ytc8N3Pzk4M6RePw36JsLx72Ebd8H5ByCc4g9N8YrGz4n5X6KqWo27QaZm3p9DTf39Aij5FSt27Ran6M8Nkp4T2Qcq9A3Sis4G8WjYz57Jxw6C9DdEg3t6HFb7f5X2PyLm4e8Z3Bro7K6MwZo25Gxq8B9Ltb4NYp4a7F2EfKs96ReJz53DiWm87Hkr2P5QnSd39Cyc";

        private const string TwoFiftySixLengthString2 = "Qo7c6YZg89Drf3W2BqGz45Kjy8RSk9p4N7TeHw35EbAi2s6C6Ptd2LJm9x7FXa53Mnm8B4Pcp4Q8Etf7WSe9z3TMj6b2KJd55RyGk3w9ZHn6i2D8AqNr4x7XFs37Coa4Y8LgKa56QmZt29Bio5A2SnPy4s8G3YbNx96XfJe72Wgd4REw7z5C8Mkq9T3DcFp6r7HLj48Qrn3KZf9q6D5LjFi22Jzk9X5Sxc6N8Hey4PYd3p7WGs92RgTm5w8MBa43";

        private const string TwoFiftySixLengthString3 = "b8YLe45ZqEk7a9T2Hct6RMm33BoDp9i5SPg78Krz2W6JjXf4y6Q8AsNw45GdCx3n9FHk7n2R3Kfd4L2CmEo56PwAb78Wtc9J5Bea3GNg89XrDy4z2SYq67Fsi9MZj87TpQx5t3QEw24NxKi63Afz7G8Xyo6CBd42Lmp9FHs5b5T3Wka2Z8DrSg64RcPn97Yeq6JMj7x2F9Prw3E5Nfo8Q4KjAn5i8ZSg4t2JYy63CpDm9e7GLq8s6THd7b2WMk5c";
    }
}
