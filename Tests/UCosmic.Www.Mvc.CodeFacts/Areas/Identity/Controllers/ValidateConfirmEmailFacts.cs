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
using UCosmic.Www.Mvc.Models;
using System.Diagnostics;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class ValidateConfirmEmailFacts
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
                exception.ParamName.ShouldEqual("tokenParamName");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void SetsTokenParamValue_UsingArg()
            {
                const string tokenParamName = "test";

                var attribute = new ValidateConfirmEmailAttribute(tokenParamName);

                attribute.TokenParamName.ShouldEqual(tokenParamName);
            }
        }

        [TestClass]
        public class TheQueryProcessorProperty
        {
            [TestMethod]
            public void CanBeSet_ByIocContainer()
            {
                const string tokenParamName = "test";
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
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
            public void ThrowsInvalidOperationException_WhenTokenParamName_IsNotFound()
            {
                const string tokenParamName = "some value";
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName);
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
                exception.Message.ShouldContain(tokenParamName);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_WhenTokenParamValue_IsNotFound()
            {
                const string tokenParamName = "some value";
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName);
                var filterContext = CreateFilterContext(tokenParamName, "not a guid or interface");
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
                exception.Message.ShouldContain(tokenParamName);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ReturnsToken_FromScalarParameter()
            {
                const string tokenParamName = "some value";
                var tokenValue = Guid.NewGuid();
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName);
                var filterContext = CreateFilterContext(tokenParamName, tokenValue, false);

                var value = attribute.GetToken(filterContext);

                value.ShouldEqual(tokenValue);
            }

            [TestMethod]
            public void ReturnsToken_FromModeledParameter()
            {
                var tokenValue = Guid.NewGuid();
                const string tokenParamName = "some value";
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName);
                var modeled = new Mock<IModelEmailConfirmation>(MockBehavior.Strict);
                modeled.Setup(p => p.Token).Returns(tokenValue);
                var filterContext = CreateFilterContext(tokenParamName, tokenValue, true);

                var value = attribute.GetToken(filterContext);

                value.ShouldEqual(tokenValue);
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

                var result = ValidateConfirmEmailAttribute.GetRedeemedRouteValues(token, intent);

                result.ShouldNotBeNull();
                result["area"].ShouldEqual(MVC.Passwords.Name);
                result["controller"].ShouldEqual(MVC.Passwords.ResetPassword.Name);
                result["action"].ShouldEqual(MVC.Passwords.ResetPassword.ActionNames.Get);
                result["token"].ShouldEqual(token);
            }

            [TestMethod]
            public void ThrowsNotSupportedException_ForSignUpIntent()
            {
                var token = Guid.NewGuid();
                const string intent = EmailConfirmationIntent.SignUp;
                NotSupportedException exception = null;

                try
                {
                    ValidateConfirmEmailAttribute.GetRedeemedRouteValues(token, intent);
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

            [TestMethod]
            public void ThrowsNotSupportedException_ForUnexpectedIntent()
            {
                var token = Guid.NewGuid();
                const string intent = "unexpected";
                NotSupportedException exception = null;

                try
                {
                    ValidateConfirmEmailAttribute.GetRedeemedRouteValues(token, intent);
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

        [TestClass]
        public class TheOnActionExecutingMethod
        {
            [TestMethod]
            public void SetsResult_To404_WhenToken_IsEmptyGuid()
            {
                const string tokenParamName = "some value";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(Guid.Empty))))
                    .Returns(new EmailConfirmation());
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(tokenParamName, Guid.Empty);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void SetsResult_To404_WhenToken_MatchesNoEntity()
            {
                const string tokenParamName = "some value";
                var tokenValue = Guid.NewGuid();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(tokenValue))))
                    .Returns(null as EmailConfirmation);
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(tokenParamName, tokenValue);

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
                const string tokenParamName = "some value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = "intent",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(tokenParamName, confirmation.Token);

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
                const string tokenParamName = "some value";
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
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(tokenParamName, confirmation.Token);

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
            public void SetsResult_ToResetPasswordRoute_WhenToken_MatchesRedeemedEntity()
            {
                const string tokenParamName = "some value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(tokenParamName, confirmation.Token);

                attribute.OnActionExecuting(filterContext);

                filterContext.Result.ShouldNotBeNull();
                filterContext.Result.ShouldBeType<RedirectToRouteResult>();
                var redirectRoute = (RedirectToRouteResult)filterContext.Result;
                redirectRoute.RouteValues["area"].ShouldEqual(MVC.Passwords.Name);
                redirectRoute.RouteValues["controller"].ShouldEqual(MVC.Passwords.ResetPassword.Name);
                redirectRoute.RouteValues["action"].ShouldEqual(MVC.Passwords.ResetPassword.ActionNames.Get);
                redirectRoute.RouteValues["token"].ShouldEqual(confirmation.Token);
            }

            [TestMethod]
            public void SetsNoResult_WhenTokenMatchesEntity_Unexpired_Unredeemed_Unretired()
            {
                const string tokenParamName = "some value";
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(confirmation.Token))))
                    .Returns(confirmation);
                var attribute = new ValidateConfirmEmailAttribute(tokenParamName)
                {
                    QueryProcessor = queryProcessor.Object,
                };
                var filterContext = CreateFilterContext(tokenParamName, confirmation.Token);

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
                    var model = new Mock<IModelEmailConfirmation>(MockBehavior.Strict);
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
