using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using Should;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class SignInControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheIsValidReturnUrlMethod
        {
            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithSignIn()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = SignInRouteMapper.SignIn.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithSignOut()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = SignInRouteMapper.SignOut.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithSignUp()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = SignUpRouteMapper.SendEmail.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithConfirmPasswordReset()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                const string returnUrl = "/confirm-password-reset/t-";
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithForgotPassword()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = PasswordRouteMapper.ForgotPassword.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }
        }
    }

}
