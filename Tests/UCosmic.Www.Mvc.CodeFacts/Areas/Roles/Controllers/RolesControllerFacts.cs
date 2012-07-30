using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Roles.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Roles.Controllers
{
    public static class RolesControllerFacts
    {
        [TestClass]
        public class TheClass
        {
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
            public void Invokes_Query()
            {
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindAllRolesQuery>()))
                    .Returns(new[]
                    {
                        new Role { Name = "role 1" },
                        new Role { Name = "role 2" },
                    });
                var controller = new RolesController(CreateRolesServices(queryProcessor.Object));

                var result = controller.Browse();

                result.ShouldNotBeNull();
                queryProcessor.Verify(r => r.Execute(It.IsAny<FindAllRolesQuery>()), Times.Once());
            }

            [TestMethod]
            public void Returns_RoleSearchResults()
            {
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindAllRolesQuery>()))
                    .Returns(new[]
                    {
                        new Role { Name = "role 1" },
                        new Role { Name = "role 2" },
                    });
                var controller = new RolesController(CreateRolesServices(queryProcessor.Object));

                var result = controller.Browse();

                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldImplement<IEnumerable<RoleSearchResult>>();
                var strongModel = ((IEnumerable<RoleSearchResult>)viewResult.Model).ToArray();
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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetRoleBySlugQuery>())).Returns(null as Role);
                var controller = new RolesController(CreateRolesServices(queryProcessor.Object));

                var result = controller.Form(slug);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void Invokes_Roles_GetBySlug()
            {
                const string slug = "role-1";
                const string name = "Role 1";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetRoleBySlugQuery>())).
                    Returns(new Role { Name = name });
                var controller = new RolesController(CreateRolesServices(queryProcessor.Object));

                var result = controller.Form(slug);

                result.ShouldNotBeNull();
                queryProcessor.Verify(r => r.Execute(It.IsAny<GetRoleBySlugQuery>()), Times.Once());
            }

            [TestMethod]
            public void Returns_RoleForm()
            {
                const string slug = "role-1";
                const string name = "Role 1";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetRoleBySlugQuery>()))
                    .Returns(new Role { Name = name });
                var controller = new RolesController(CreateRolesServices(queryProcessor.Object));

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

        private static RolesServices CreateRolesServices(IProcessQueries queryProcessor)
        {
            return new RolesServices(queryProcessor, null);
        }
    }
}
