using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class UpdateAffiliationControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void IsDecoratedWith_Authorize()
            {
                var attribute = Attribute.GetCustomAttribute(typeof(UpdateAffiliationController), typeof(AuthorizeAttribute));

                attribute.ShouldNotBeNull();
                attribute.ShouldBeType<AuthorizeAttribute>();
            }
        }

        [TestClass]
        public class TheGetMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpGet()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Get(1);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, HttpGetAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Get(1);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingUpdateAffiliation()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Get(1);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("update-affiliation");
            }

            [TestMethod]
            public void IsDecoratedWith_ReturnUrlReferrer_UsingMyProfile()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Get(1);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, ReturnUrlReferrerAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Fallback.ShouldEqual(ProfileRouter.Get.Route);
            }

            [TestMethod]
            public void ExecutesQuery_ForAffiliation_ByPrincipalAndEstablishmentId()
            {
                const int establishmentId = 2;
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = userName,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(AffiliationQueryBasedOn(userName, establishmentId))))
                        .Returns(null as Affiliation);

                controller.Get(establishmentId);

                scenarioOptions.MockQueryProcessor.Verify(m => m.Execute(
                    It.Is(AffiliationQueryBasedOn(userName, establishmentId))),
                        Times.Once());
            }

            [TestMethod]
            public void Returns404_WhenAffiliation_CannotBeFound()
            {
                const int establishmentId = 2;
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = userName,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(AffiliationQueryBasedOn(userName, establishmentId))))
                        .Returns(null as Affiliation);

                var result = controller.Get(establishmentId);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsPartialView_WhenAffiliation_IsFound()
            {
                const int establishmentId = 2;
                const string userName = "user@domain.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = userName,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockQueryProcessor.Setup(m => m.Execute(
                    It.Is(AffiliationQueryBasedOn(userName, establishmentId))))
                        .Returns(new Affiliation());

                var result = controller.Get(establishmentId);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialViewResult = (PartialViewResult)result;
                partialViewResult.Model.ShouldNotBeNull();
                partialViewResult.Model.ShouldBeType<UpdateAffiliationForm>();
            }
        }

        [TestClass]
        public class ThePutMethod
        {
            [TestMethod]
            public void IsDecoratedWith_HttpPut()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, HttpPutAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_UnitOfWork()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, UnitOfWorkAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
            }

            [TestMethod]
            public void IsDecoratedWith_OpenTopTab_UsingHome()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, OpenTopTabAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].TabName.ShouldEqual(TopTabName.Home);
            }

            [TestMethod]
            public void IsDecoratedWith_ActionName_UsingUpdateAffiliation()
            {
                Expression<Func<UpdateAffiliationController, ActionResult>> method = m => m.Put(null);

                var attributes = method.GetAttributes<UpdateAffiliationController, ActionResult, ActionNameAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldNotBeNull();
                attributes[0].Name.ShouldEqual("update-affiliation");
            }

            [TestMethod]
            public void Returns404_WhenModel_IsNull()
            {
                var controller = CreateController();

                var result = controller.Put(null);

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod]
            public void ReturnsPartialView_WhenModelState_IsInvalid()
            {
                var scenarioOptions = new ScenarioOptions();
                var model = new UpdateAffiliationForm();
                var controller = CreateController(scenarioOptions);
                controller.ModelState.AddModelError("error", "message");

                var result = controller.Put(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<PartialViewResult>();
                var partialViewResult = (PartialViewResult)result;
                partialViewResult.Model.ShouldNotBeNull();
                partialViewResult.Model.ShouldBeType<UpdateAffiliationForm>();
                partialViewResult.Model.ShouldEqual(model);
            }

            [TestMethod]
            public void ExecutesCommand_WhenAction_IsValid()
            {
                const string principalIdentityName = "user@domain.tld";
                const int establishmentId = 8;
                const string jobTitles = "job titles";
                const EmployeeOrStudentAffiliate employeeOrStudentAffiliation 
                    = EmployeeOrStudentAffiliate.StudentOnly;
                const string returnUrl = "http://www.site.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = principalIdentityName,
                };
                var model = new UpdateAffiliationForm
                {
                    JobTitles = jobTitles,
                    EstablishmentId = establishmentId,
                    EmployeeOrStudentAffiliation = employeeOrStudentAffiliation,
                    IsClaimingInternationalOffice = true,
                    ReturnUrl = returnUrl,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(
                    It.Is(CommandBasedOn(model, principalIdentityName))));

                controller.Put(model);

                scenarioOptions.MockCommandHandler.Verify(m =>
                    m.Handle(It.Is(CommandBasedOn(model, principalIdentityName))),
                        Times.Once());
            }

            [TestMethod]
            public void FlashesSuccessMessage_WhenCommand_ChangedState()
            {
                const string principalIdentityName = "user@domain.tld";
                const int establishmentId = 8;
                const string jobTitles = "job titles";
                const EmployeeOrStudentAffiliate employeeOrStudentAffiliation
                    = EmployeeOrStudentAffiliate.StudentOnly;
                const string returnUrl = "http://www.site.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = principalIdentityName,
                };
                var model = new UpdateAffiliationForm
                {
                    JobTitles = jobTitles,
                    EstablishmentId = establishmentId,
                    EmployeeOrStudentAffiliation = employeeOrStudentAffiliation,
                    IsClaimingAdministrator = true,
                    ReturnUrl = returnUrl,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(
                    It.Is(CommandBasedOn(model, principalIdentityName))))
                        .Callback((UpdateMyAffiliationCommand command) => command.ChangeCount = 1);

                controller.Put(model);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(UpdateAffiliationController.SuccessMessage);
            }

            [TestMethod]
            public void FlashesNoChangesMessage_WhenCommand_ChangedState()
            {
                const string principalIdentityName = "user@domain.tld";
                const int establishmentId = 8;
                const string jobTitles = "job titles";
                const EmployeeOrStudentAffiliate employeeOrStudentAffiliation
                    = EmployeeOrStudentAffiliate.StudentOnly;
                const string returnUrl = "http://www.site.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = principalIdentityName,
                };
                var model = new UpdateAffiliationForm
                {
                    JobTitles = jobTitles,
                    EstablishmentId = establishmentId,
                    EmployeeOrStudentAffiliation = employeeOrStudentAffiliation,
                    IsClaimingFaculty = true,
                    ReturnUrl = returnUrl,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(
                    It.Is(CommandBasedOn(model, principalIdentityName))));

                controller.Put(model);

                controller.TempData.ShouldNotBeNull();
                var message = controller.TempData.FeedbackMessage();
                message.ShouldNotBeNull();
                message.ShouldEqual(UpdateAffiliationController.NoChangesMessage);
            }

            [TestMethod]
            public void ReturnsRedirect_ToModelReturnUrl_AfterCommandIsExecuted()
            {
                const string principalIdentityName = "user@domain.tld";
                const int establishmentId = 8;
                const string jobTitles = "job titles";
                const EmployeeOrStudentAffiliate employeeOrStudentAffiliation
                    = EmployeeOrStudentAffiliate.StudentOnly;
                const string returnUrl = "http://www.site.tld";
                var scenarioOptions = new ScenarioOptions
                {
                    PrincipalIdentityName = principalIdentityName,
                };
                var model = new UpdateAffiliationForm
                {
                    JobTitles = jobTitles,
                    EstablishmentId = establishmentId,
                    EmployeeOrStudentAffiliation = employeeOrStudentAffiliation,
                    IsClaimingStaff = true,
                    ReturnUrl = returnUrl,
                };
                var controller = CreateController(scenarioOptions);
                scenarioOptions.MockCommandHandler.Setup(m => m.Handle(
                    It.Is(CommandBasedOn(model, principalIdentityName))));

                var result = controller.Put(model);

                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectResult>();
                var redirectResult = (RedirectResult)result;
                redirectResult.Url.ShouldEqual(model.ReturnUrl);
                redirectResult.Permanent.ShouldBeFalse();
            }
        }

        private class ScenarioOptions
        {
            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
            internal Mock<IHandleCommands<UpdateMyAffiliationCommand>> MockCommandHandler { get; set; }
            internal string PrincipalIdentityName { get; set; }
        }

        private static UpdateAffiliationController CreateController(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();

            scenarioOptions.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            scenarioOptions.MockCommandHandler = new Mock<IHandleCommands<UpdateMyAffiliationCommand>>(MockBehavior.Strict);

            var services = new UpdateAffiliationServices(scenarioOptions.MockQueryProcessor.Object, scenarioOptions.MockCommandHandler.Object);

            var controller = new UpdateAffiliationController(services);

            var builder = ReuseMock.TestControllerBuilder();

            builder.HttpContext.User = null;
            if (!string.IsNullOrWhiteSpace(scenarioOptions.PrincipalIdentityName))
            {
                var principal = scenarioOptions.PrincipalIdentityName.AsPrincipal();
                builder.HttpContext.User = principal;
            }

            builder.InitializeController(controller);

            return controller;
        }

        private static Expression<Func<GetMyAffiliationByEstablishmentIdQuery, bool>> AffiliationQueryBasedOn(string userName, int establishmentId)
        {
            Expression<Func<GetMyAffiliationByEstablishmentIdQuery, bool>> affiliationQueryBasedOn = q => 
                q.Principal.Identity.Name == userName && 
                q.EstablishmentId == establishmentId
            ;
            return affiliationQueryBasedOn;
        }

        private static Expression<Func<UpdateMyAffiliationCommand, bool>> CommandBasedOn(UpdateAffiliationForm model, string principalIdentityName)
        {
            Expression<Func<UpdateMyAffiliationCommand, bool>> commandBasedOn = command =>
                command.Principal.Identity.Name == principalIdentityName &&
                command.EstablishmentId == model.EstablishmentId &&
                command.JobTitles == model.JobTitles &&
                command.IsClaimingStudent == model.IsClaimingStudent &&
                command.IsClaimingEmployee == model.IsClaimingEmployee &&
                command.IsClaimingInternationalOffice == model.IsClaimingInternationalOffice &&
                command.IsClaimingAdministrator == model.IsClaimingAdministrator &&
                command.IsClaimingFaculty == model.IsClaimingFaculty &&
                command.IsClaimingStaff == model.IsClaimingStaff
            ;
            return commandBasedOn;
        }
    }
}
