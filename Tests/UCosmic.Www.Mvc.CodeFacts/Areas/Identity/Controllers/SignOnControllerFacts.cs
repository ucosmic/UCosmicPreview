using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class SignOnControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Extends_BaseController()
            {
                var services = CreateSignOnServices();
                var controller = new SignOnController(services);
                controller.ShouldImplement<BaseController>();
            }
        }

        [TestClass]
        public class TheGetMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Get(null);

                var attributes = method.GetAttributes<SignOnController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_Using_SignOn()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Get(null);

                var attributes = method.GetAttributes<SignOnController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("sign-on");
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_Using_Home()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Get(null);

                var attributes = method.GetAttributes<SignOnController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void ReturnsView_WhenReturnUrlArg_IsNull()
            {
                var services = CreateSignOnServices();
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignOnController(services)
                {
                    Url = new UrlHelper(requestContext)
                };

                var result = controller.Get(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult) result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<SignOnForm>();
                var form = (SignOnForm)viewResult.Model;
                form.EmailAddress.ShouldBeNull();
                form.ReturnUrl.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsView_WhenReturnUrlArg_IsEmptyString()
            {
                var returnUrl = string.Empty;
                var services = CreateSignOnServices();
                var controller = new SignOnController(services);

                var result = controller.Get(returnUrl);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<SignOnForm>();
                var form = (SignOnForm)viewResult.Model;
                form.EmailAddress.ShouldBeNull();
                form.ReturnUrl.ShouldEqual(returnUrl);
            }

            [TestMethod]
            public void ReturnsView_WhenReturnUrlArg_IsNonEmptyString()
            {
                const string returnUrl = "/path/to/resource";
                var userSigner = new Mock<ISignUsers>(MockBehavior.Strict);
                userSigner.Setup(p => p.DefaultSignedOnUrl).Returns("/my/home");
                var services = CreateSignOnServices();
                var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
                var requestContext = new RequestContext(builder.HttpContext, new RouteData());
                var controller = new SignOnController(services)
                {
                    Url = new UrlHelper(requestContext)
                };

                var result = controller.Get(returnUrl);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<SignOnForm>();
                var form = (SignOnForm)viewResult.Model;
                form.EmailAddress.ShouldBeNull();
                form.ReturnUrl.ShouldEqual(returnUrl);
            }
        }

        [TestClass]
        public class ThePostMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<SignOnController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_Using_SignOn()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<SignOnController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("sign-on");
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_Using_Home()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Post(null);

                var attributes = method.GetAttributes<SignOnController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void ReturnsView_WhenModelState_IsInvalid()
            {
                var services = CreateSignOnServices();
                var controller = new SignOnController(services);
                controller.ModelState.AddModelError("SomeProperty", "SomeMessage");
                var model = new SignOnForm { EmailAddress = "invalid email" };

                var result = controller.Post(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<SignOnForm>();
                var form = (SignOnForm)viewResult.Model;
                form.ShouldEqual(model);
                form.EmailAddress.ShouldEqual(model.EmailAddress);
                form.ReturnUrl.ShouldEqual(model.ReturnUrl);
            }
        }

        private static SignOnServices CreateSignOnServices()
        {
            return new SignOnServices(null, null, null, null);
        }
    }
}
