using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class ValidateConfirmEmailAttributeFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenArgIsNull()
            {
                ArgumentNullException exception = null;

                try
                {
                    new ValidateConfirmEmailAttribute(null);
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
            public void SetsParamNameProperty_ToArgValue()
            {
                const string paramName = "test";

                var attribute = new ValidateConfirmEmailAttribute(paramName);

                attribute.ParamName.ShouldEqual(paramName);
            }
        }

        [TestClass]
        public class TheQueryProcessorProperty
        {
            [TestMethod]
            public void CanBeSet_ByIocContainer()
            {
                const string paramName = "test";
                var attribute = new ValidateConfirmEmailAttribute(paramName)
                {
                    QueryProcessor = null
                };
                attribute.QueryProcessor.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheGetTokenMethod
        {
            [TestMethod]
            public void ThrowsInvalidOperationException_WhenParamName_IsNotFound()
            {
                const string paramName = "some value";
                var attribute = new ValidateConfirmEmailAttribute(paramName);
                var filterContext = CreateFilterContext(null, null);
                InvalidOperationException exception = null;

                try
                {
                    attribute.GetToken(filterContext);
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Message.ShouldContain(paramName);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_WhenTokenParamValue_IsNotFound()
            {
                const string paramName = "some value";
                var attribute = new ValidateConfirmEmailAttribute(paramName);
                var filterContext = CreateFilterContext(paramName, "not a guid or interface");
                InvalidOperationException exception = null;

                try
                {
                    attribute.GetToken(filterContext);
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Message.ShouldContain(paramName);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ReturnsToken_FromScalarParameter()
            {
                const string paramName = "some value";
                var tokenValue = Guid.NewGuid();
                var attribute = new ValidateConfirmEmailAttribute(paramName);
                var filterContext = CreateFilterContext(paramName, tokenValue, false);

                var value = attribute.GetToken(filterContext);

                value.ShouldEqual(tokenValue);
            }

            [TestMethod]
            public void ReturnsToken_FromModeledParameter()
            {
                var tokenValue = Guid.NewGuid();
                const string paramName = "some value";
                var attribute = new ValidateConfirmEmailAttribute(paramName);
                var modeled = new Mock<IModelConfirmAndRedeem>(MockBehavior.Strict);
                modeled.Setup(p => p.Token).Returns(tokenValue);
                var filterContext = CreateFilterContext(paramName, tokenValue, true);

                var value = attribute.GetToken(filterContext);

                value.ShouldEqual(tokenValue);
            }
        }

        [TestClass]
        public class TheOnActionExecutingMethod
        {
            [TestMethod]
            public void SetsResult_To404_WhenToken_IsEmptyGuid()
            {
                const string paramName = "some value";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(Guid.Empty))))
                    .Returns(new EmailConfirmation());
                var attribute = new ValidateConfirmEmailAttribute(paramName)
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
                var tokenValue = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(tokenValue))))
                    .Returns(null as EmailConfirmation);
                var attribute = new ValidateConfirmEmailAttribute(paramName)
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
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = "intent",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateConfirmEmailAttribute(paramName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.Shared.Views._confirm_email_denied);
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
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RetiredOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = "intent",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateConfirmEmailAttribute(paramName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<PartialViewResult>();
                var partialView = (PartialViewResult)filterContext.Result;
                partialView.ViewName.ShouldEqual(MVC.Identity.Shared.Views._confirm_email_denied);
                partialView.Model.ShouldNotBeNull();
                partialView.Model.ShouldBeType<ConfirmDeniedModel>();
                var model = (ConfirmDeniedModel)partialView.Model;
                model.Reason.ShouldEqual(ConfirmDeniedBecause.IsRetired);
                model.Intent.ShouldEqual(confirmation.Intent);
            }

            [TestMethod]
            public void SetsNoResult_WhenTokenMatchesEntity_Unexpired_Unredeemed_Unretired()
            {
                const string paramName = "some value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateConfirmEmailAttribute(paramName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(paramName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldBeNull();
            }
        }

        private static ActionExecutingContext CreateFilterContext(string tokenKey, object tokenValue, bool? isModeled = null)
        {
            var parameters = new Dictionary<string, object>();
            if (!isModeled.HasValue) isModeled = new Random().Next(0, 1) == 1;
            Debug.Assert(isModeled.HasValue);

            if (tokenKey != null)
            {
                if (isModeled.Value && tokenValue is Guid)
                {
                    var model = new Mock<IModelConfirmAndRedeem>(MockBehavior.Strict);
                    model.Setup(p => p.Token).Returns((Guid)tokenValue);
                    tokenValue = model.Object;
                }
                parameters.Add(tokenKey, tokenValue);
            }

            var filterContext = new Mock<ActionExecutingContext>(MockBehavior.Strict);
            filterContext.Setup(p => p.ActionParameters).Returns(parameters);
            return filterContext.Object;
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(Guid tokenValue)
        {
            return q => q.Token == tokenValue;
        }
    }
}
