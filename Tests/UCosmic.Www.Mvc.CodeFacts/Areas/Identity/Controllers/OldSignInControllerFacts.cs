using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.Passwords.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class OldSignInControllerFacts
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
                var controller = new OldSignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = OldSignInRouteMapper.SignIn.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithSignOut()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new OldSignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = OldSignInRouteMapper.SignOut.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithSignUp()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new OldSignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = OldSignUpRouteMapper.SendEmail.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenReturnUrl_StartsWithConfirmPasswordReset()
            {
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new OldSignInController(null)
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
                var controller = new OldSignInController(null)
                {
                    Url = new UrlHelper(requestContext)
                };

                var returnUrl = ForgotPasswordRouter.Get.Route.ToUrlHelperResult();
                var result = controller.IsValidReturnUrl(returnUrl);

                result.ShouldBeFalse();
            }
        }
    }

}
