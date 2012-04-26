using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class RolesControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_EnforceHttps()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(RolesController), typeof(EnforceHttpsAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<EnforceHttpsAttribute>();
            }

            [TestMethod]
            public void IsDecoratedWith_Authorize_Using_Authorization_Roles()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(RolesController), typeof(AuthorizeAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<AuthorizeAttribute>();
                var strongAttribute = (AuthorizeAttribute)attribute;
                strongAttribute.Roles.ShouldEqual(RoleName.AuthorizationAgent);
            }

            [TestMethod]
            public void Extends_BaseController()
            {
                var controller = new RolesController(CreateRolesServices());
                controller.ShouldImplement<BaseController>();
            }
        }

        [TestClass]
        public class TheBrowseMethod
        {
            [TestMethod]
            public void Invokes_Roles_Get()
            {
                var roles = new Mock<RoleFacade>(MockBehavior.Strict);
                roles.Setup(m => m.Get()).Returns(new[] { new Role { Name = "role 1" }, new Role { Name = "role 2" }, });
                var controller = new RolesController(CreateRolesServices(roles.Object));

                var result = controller.Browse();

                result.ShouldNotBeNull();
                roles.Verify(r => r.Get(), Times.Once());
            }

            [TestMethod]
            public void Returns_RoleSearchResults()
            {
                var roles = new Mock<RoleFacade>(MockBehavior.Strict);
                roles.Setup(m => m.Get()).Returns(new[] { new Role { Name = "role 1" }, new Role { Name = "role 2" }, });
                var controller = new RolesController(CreateRolesServices(roles.Object));

                var result = controller.Browse();

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult) result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldImplement<IEnumerable<RoleSearchResult>>();
                var strongModel = ((IEnumerable<RoleSearchResult>) viewResult.Model).ToArray();
                strongModel.Length.ShouldEqual(2);
            }

            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<RolesController, ActionResult>> method = m => m.Browse();

                var attributes = method.GetAttributes<RolesController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_Using_Browse()
            {
                Expression<Func<RolesController, ActionResult>> method = m => m.Browse();

                var attributes = method.GetAttributes<RolesController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("browse");
            }

        }

        [TestClass]
        public class TheFormMethod
        {
            [TestMethod]
            public void Returns404_WhenSlug_IsNull()
            {
                var controller = new RolesController(CreateRolesServices());

                var result = controller.Form(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Returns404_WhenSlug_IsEmptyString()
            {
                var controller = new RolesController(CreateRolesServices());

                var result = controller.Form(string.Empty);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Returns404_WhenSlug_IsWhiteSpaceString()
            {
                var controller = new RolesController(CreateRolesServices());

                var result = controller.Form(" \t ");

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Returns404_WhenSlug_DoesNotMatchRole()
            {
                const string slug = "role-1";
                var roles = new Mock<RoleFacade>(MockBehavior.Strict);
                roles.Setup(m => m.GetBySlug(slug)).Returns(null as Role);
                var controller = new RolesController(CreateRolesServices(roles.Object));

                var result = controller.Form(slug);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Invokes_Roles_GetBySlug()
            {
                const string slug = "role-1";
                const string name = "Role 1";
                var roles = new Mock<RoleFacade>(MockBehavior.Strict);
                roles.Setup(m => m.GetBySlug(slug)).Returns(new Role { Name = name });
                var controller = new RolesController(CreateRolesServices(roles.Object));

                var result = controller.Form(slug);

                result.ShouldNotBeNull();
                roles.Verify(r => r.GetBySlug(slug), Times.Once());
            }

            [TestMethod]
            public void Returns_RoleForm()
            {
                const string slug = "role-1";
                const string name = "Role 1";
                var roles = new Mock<RoleFacade>(MockBehavior.Strict);
                roles.Setup(m => m.GetBySlug(slug)).Returns(new Role { Name = name });
                var controller = new RolesController(CreateRolesServices(roles.Object));

                var result = controller.Form(slug);

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldImplement<RoleForm>();
                var strongModel = (RoleForm)viewResult.Model;
                strongModel.Name.ShouldEqual(name);
            }

        }

        private static RolesServices CreateRolesServices()
        {
            return new RolesServices(null, null);
        }

        private static RolesServices CreateRolesServices(RoleFacade roles)
        {
            return new RolesServices(roles, null);
        }
    }
}
