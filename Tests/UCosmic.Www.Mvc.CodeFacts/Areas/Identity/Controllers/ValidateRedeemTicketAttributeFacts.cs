using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ValidateRedeemTicketAttributeFacts
    // ReSharper restore UnusedMember.Global
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
                    new ValidateRedeemTicketAttribute(null, null);
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
            public void ThrowsArgumentNullException_WhenIntentArgIsNull()
            {
                ArgumentNullException exception = null;

                try
                {
                    new ValidateRedeemTicketAttribute("test", null);
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("intent");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void SetsParamNameProperty_ToParamNameArgValue()
            {
                const string paramName = "test";

                var attribute = new ValidateRedeemTicketAttribute(paramName, "intent");

                attribute.ParamName.ShouldEqual(paramName);
            }

            [TestMethod]
            public void SetsIntentProperty_ToIntentArgValue()
            {
                const string intent = "test";

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
                const string intent = "some other value";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(Guid.Empty))))
                    .Returns(new EmailConfirmation());
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
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
                const string intent = "some other value";
                var tokenValue = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(tokenValue))))
                    .Returns(null as EmailConfirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(paramName, tokenValue);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<HttpNotFoundResult>();
                queryProcessor.Verify(m => m.
                    Execute(It.Is(ConfirmationQueryBasedOn(tokenValue))),
                    Times.Once());
            }

            [TestMethod]
            public void SetsResult_ToPartialExpiredDenial_WhenToken_MatchesExpiredEntity()
            {
                const string paramName = "some value";
                const string intent = "some other value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied);
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
                const string intent = "some other value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RetiredOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.IsRetired);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsResult_ToPartialCrashDenaial_WhenToken_MatchesUnredeemedEntity()
            {
                const string paramName = "some value";
                const string intent = EmailConfirmationIntent.PasswordReset;
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    Intent = intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var controller = new ConfirmEmailController(null);
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.OtherCrash);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsResult_ToPartialCrashDenaial_WhenIntent_ConflictsWithEntity()
            {
                const string paramName = "some value";
                const string intent = EmailConfirmationIntent.PasswordReset;
                const string ticket = "ticket value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = EmailConfirmationIntent.SignUp,
                    Ticket = ticket,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var controller = new ConfirmEmailController(null);
                controller.TempData.EmailConfirmationTicket(ticket);
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.OtherCrash);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsResult_ToPartialCrashDenaial_WhenTicket_ConflictsWithController()
            {
                const string paramName = "some value";
                const string intent = EmailConfirmationIntent.SignUp;
                const string ticket = "ticket value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = intent,
                    Ticket = ticket,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var controller = new ConfirmEmailController(null);
                controller.TempData.EmailConfirmationTicket("a different ticket value");
                var filterContext = CreateFilterContext(paramName, confirmation.Token, controller);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.ConfirmEmail.Views._denied);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.OtherCrash);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsNoResult_WhenTokenMatchesEntity_Unexpired_Redeemed_Unretired_WithCorrectTicketAndIntent()
            {
                const string paramName = "some value";
                const string intent = EmailConfirmationIntent.SignUp;
                const string ticket = "ticket value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = intent,
                    Ticket = ticket,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateRedeemTicketAttribute(paramName, intent)
                {
                    QueryProcessor = queryProcessor.Object,
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

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(Guid tokenValue)
        {
            return q => q.Token == tokenValue;
        }
    }
}
