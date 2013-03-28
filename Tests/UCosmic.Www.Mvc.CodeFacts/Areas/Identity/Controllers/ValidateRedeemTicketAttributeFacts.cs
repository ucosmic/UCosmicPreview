using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public static class ValidateRedeemTicketAttributeFacts
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenParamNameArgIsNull()
            {
                ArgumentNullException exception = null;

                try
                {
                    var obj = new ValidateRedeemTicketAttribute(null, EmailConfirmationIntent.ResetPassword);
                    obj.ShouldBeNull();
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("paramName");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void SetsParamNameProperty_ToParamNameArgValue()
            {
                const string paramName = "test";

                var attribute = new ValidateRedeemTicketAttribute(paramName, EmailConfirmationIntent.CreatePassword);

                attribute.ParamName.ShouldEqual(paramName);
            }

            [TestMethod]
            public void SetsIntentProperty_ToIntentArgValue()
            {
                const EmailConfirmationIntent intent = EmailConfirmationIntent.CreatePassword;

                var attribute = new ValidateRedeemTicketAttribute("paramName", intent);

                attribute.Intent.ShouldEqual(intent);
            }
        }

        [TestClass]
        public class TheOnActionExecutingMethod
        {
            [TestMethod]
            public void SetsResult_To404_WhenToken_IsEmptyGuid()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.ResetPassword;
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { new EmailConfirmation(EmailConfirmationIntent.CreatePassword) }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var filterContext = CreateFilterContext(paramName, Guid.Empty);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void SetsResult_To404_WhenToken_MatchesNoEntity()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.CreatePassword;
                var tokenValue = Guid.NewGuid();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new EmailConfirmation[] { }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var filterContext = CreateFilterContext(paramName, tokenValue);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<HttpNotFoundResult>();
                entities.Verify(m => m.Query<EmailConfirmation>(), Times.Once());
            }

            [TestMethod]
            public void SetsResult_ToPartialExpiredDenial_WhenToken_MatchesExpiredEntity()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.ResetPassword;
                var confirmation = new EmailConfirmation(intent)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(-5),
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied_all);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.IsExpired);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsResult_ToPartialRetiredDenial_WhenToken_MatchesExpiredEntity()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.CreatePassword;
                var confirmation = new EmailConfirmation(intent)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RetiredOnUtc = DateTime.UtcNow.AddSeconds(-5),
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied_all);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.IsRetired);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsResult_ToPartialCrashDenaial_WhenIntent_ConflictsWithEntity()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.ResetPassword;
                const string ticket = "ticket value";
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Ticket = ticket,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var controller = new ConfirmEmailController(null);
                controller.TempData.EmailConfirmationTicket(ticket);
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied_all);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.OtherCrash);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsResult_ToConfirmEmailRedirect_WhenToken_MatchesUnredeemedEntity()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.ResetPassword;
                var confirmation = new EmailConfirmation(intent)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var controller = new ConfirmEmailController(null);
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)filterContext.Result;
                routeResult.Permanent.ShouldBeFalse();
                routeResult.RouteValues["area"].ShouldEqual(MVC.Identity.Name);
                routeResult.RouteValues["controller"].ShouldEqual(MVC.Identity.ConfirmEmail.Name);
                routeResult.RouteValues["action"].ShouldEqual(MVC.Identity.ConfirmEmail.ActionNames.Get);
                routeResult.RouteValues["token"].ShouldEqual(confirmation.Token);
            }

            [TestMethod]
            public void SetsResult_ToConfirmEmailRedirect_WhenTicket_ConflictsWithController()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.CreatePassword;
                const string ticket = "ticket value";
                var confirmation = new EmailConfirmation(intent)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Ticket = ticket,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var controller = new ConfirmEmailController(null);
                controller.TempData.EmailConfirmationTicket("a different ticket value");
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)filterContext.Result;
                routeResult.Permanent.ShouldBeFalse();
                routeResult.RouteValues["area"].ShouldEqual(MVC.Identity.Name);
                routeResult.RouteValues["controller"].ShouldEqual(MVC.Identity.ConfirmEmail.Name);
                routeResult.RouteValues["action"].ShouldEqual(MVC.Identity.ConfirmEmail.ActionNames.Get);
                routeResult.RouteValues["token"].ShouldEqual(confirmation.Token);
            }

            [TestMethod]
            public void SetsNoResult_WhenTokenMatchesEntity_Unexpired_Redeemed_Unretired_WithCorrectTicketAndIntent()
            {
                const string paramName = "some value";
                const EmailConfirmationIntent intent = EmailConfirmationIntent.CreatePassword;
                const string ticket = "ticket value";
                var confirmation = new EmailConfirmation(intent)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Ticket = ticket,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    Entities = entities.Object,
                };
                var controller = new ConfirmEmailController(null);
                controller.TempData.EmailConfirmationTicket(ticket);
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldBeNull();
                controller.TempData.EmailConfirmationTicket().ShouldEqual(ticket);
            }
        }

        private static ActionExecutingContext CreateFilterContext(string tokenKey, object tokenValue,
            Controller forController = null)
        {
            var parameters = new Dictionary<string, object>();
            var isModeled = new Random().Next(0, 1) == 1;

            if (tokenKey != null)
            {
                if (isModeled && tokenValue is Guid)
                {
                    var model = new Mock<IModelConfirmAndRedeem>(MockBehavior.Strict);
                    model.Setup(p => p.Token).Returns((Guid)tokenValue);
                    tokenValue = model.Object;
                }
                parameters.Add(tokenKey, tokenValue);
            }

            var filterContext = new Mock<ActionExecutingContext>(MockBehavior.Strict);
            filterContext.Setup(p => p.ActionParameters).Returns(parameters);
            if (forController != null)
                filterContext.Setup(p => p.Controller).Returns(forController);

            return filterContext.Object;
        }
    }
}
