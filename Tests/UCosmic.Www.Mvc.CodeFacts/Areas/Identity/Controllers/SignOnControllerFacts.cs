using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignOn;
using UCosmic.Www.Mvc.Areas.Identity.Services;
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
        public class TheBeginMethodGet
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Begin(null as string);

                var attributes = method.GetAttributes<SignOnController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_Using_SignOn()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Begin(null as string);

                var attributes = method.GetAttributes<SignOnController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("sign-on");
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_Using_Home()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Begin(null as string);

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
                var controller = new SignOnController(services);

                var result = controller.Begin(null as string);

                result.ShouldNotBeNull();
                result.ViewName.ShouldEqual(string.Empty);
                result.Model.ShouldNotBeNull();
                result.Model.ShouldBeType<SignOnBeginForm>();
                var form = (SignOnBeginForm)result.Model;
                form.EmailAddress.ShouldBeNull();
                form.ReturnUrl.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsView_WhenReturnUrlArg_IsEmptyString()
            {
                var returnUrl = string.Empty;
                var services = CreateSignOnServices();
                var controller = new SignOnController(services);

                var result = controller.Begin(returnUrl);

                result.ShouldNotBeNull();
                result.ViewName.ShouldEqual(string.Empty);
                result.Model.ShouldNotBeNull();
                result.Model.ShouldBeType<SignOnBeginForm>();
                var form = (SignOnBeginForm)result.Model;
                form.EmailAddress.ShouldBeNull();
                form.ReturnUrl.ShouldEqual(returnUrl);
            }

            [TestMethod]
            public void ReturnsView_WhenReturnUrlArg_IsNonEmptyString()
            {
                const string returnUrl = "/path/to/resource";
                var services = CreateSignOnServices();
                var controller = new SignOnController(services);

                var result = controller.Begin(returnUrl);

                result.ShouldNotBeNull();
                result.ViewName.ShouldEqual(string.Empty);
                result.Model.ShouldNotBeNull();
                result.Model.ShouldBeType<SignOnBeginForm>();
                var form = (SignOnBeginForm)result.Model;
                form.EmailAddress.ShouldBeNull();
                form.ReturnUrl.ShouldEqual(returnUrl);
            }
        }

        [TestClass]
        public class TheBeginMethodPost
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPost()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Begin(null as SignOnBeginForm);

                var attributes = method.GetAttributes<SignOnController, ActionResult, HttpPostAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_Using_SignOn()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Begin(null as string);

                var attributes = method.GetAttributes<SignOnController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("sign-on");
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_Using_Home()
            {
                Expression<Func<SignOnController, ActionResult>> method = m => m.Begin(null as string);

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
                var model = new SignOnBeginForm { EmailAddress = "invalid email" };

                var result = controller.Begin(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<SignOnBeginForm>();
                var form = (SignOnBeginForm)viewResult.Model;
                form.ShouldEqual(model);
                form.EmailAddress.ShouldEqual(model.EmailAddress);
                form.ReturnUrl.ShouldEqual(model.ReturnUrl);
            }
        }

        private static SignOnServices CreateSignOnServices()
        {
            return new SignOnServices(null, null, null, null, null, null, null);
        }

    }
}
