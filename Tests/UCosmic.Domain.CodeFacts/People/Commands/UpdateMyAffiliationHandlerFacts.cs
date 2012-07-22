using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class UpdateMyAffiliationHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new UpdateMyAffiliationHandler(null);
                try
                {
                    handler.Handle(null);
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("command");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ExecutesQuery_ToGetAffiliation_FromPrincipalAndEstablishmentId()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                };
                var affiliation = new Affiliation
                {
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                var handler = new UpdateMyAffiliationHandler(entities.Object);
                NullReferenceException exception = null;

                try
                {
                    handler.Handle(command);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                entities.Verify(m => m.Get2<Affiliation>(), Times.Once());
                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void UpdatesAffiliation_WhenFieldsHaveChanged()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                };
                var affiliation = new Affiliation
                {
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(AffiliationBasedOn(command))));
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(AffiliationBasedOn(command))), Times.Once());
            }

            [TestMethod]
            public void DoesNotUpdateAffiliation_WhenFieldsHaveNotChanged()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                };
                var affiliation = new Affiliation
                {
                    EstablishmentId = command.EstablishmentId,
                    IsAcknowledged = true,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(AffiliationBasedOn(command))));
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<Entity>()), Times.Never());
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenCurrentAffiliation_IsNotAcknowledged()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                };
                var affiliation = new Affiliation
                {
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsAcknowledged.ShouldBeTrue();
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenJobTitles_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    JobTitles = "new value",
                };
                var affiliation = new Affiliation
                {
                    JobTitles = "old value",
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.JobTitles.ShouldEqual(command.JobTitles);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsClaimingStudent_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    IsClaimingStudent = true,
                };
                var affiliation = new Affiliation
                {
                    IsClaimingStudent = false,
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingStudent.ShouldEqual(command.IsClaimingStudent);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsClaimingEmployee_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    IsClaimingEmployee = true,
                };
                var affiliation = new Affiliation
                {
                    IsClaimingEmployee = false,
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingEmployee.ShouldEqual(command.IsClaimingEmployee);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsClaimingInternationalOffice_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    IsClaimingInternationalOffice = true,
                };
                var affiliation = new Affiliation
                {
                    IsClaimingInternationalOffice = false,
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingInternationalOffice.ShouldEqual(command.IsClaimingInternationalOffice);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsClaimingAdministrator_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    IsClaimingAdministrator = true,
                };
                var affiliation = new Affiliation
                {
                    IsClaimingAdministrator = false,
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingAdministrator.ShouldEqual(command.IsClaimingAdministrator);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsClaimingFaculty_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    IsClaimingFaculty = true,
                };
                var affiliation = new Affiliation
                {
                    IsClaimingFaculty = false,
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingFaculty.ShouldEqual(command.IsClaimingFaculty);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsClaimingStaff_IsDifferent()
            {
                Affiliation outAffiliation = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                    EstablishmentId = 4,
                    IsClaimingStaff = true,
                };
                var affiliation = new Affiliation
                {
                    IsClaimingStaff = false,
                    IsAcknowledged = true,
                    EstablishmentId = command.EstablishmentId,
                    Person = new Person { User = new User { Name = principal.Identity.Name } },
                };
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingStaff.ShouldEqual(command.IsClaimingStaff);
            }

            private static Expression<Func<Affiliation, bool>> AffiliationBasedOn(UpdateMyAffiliationCommand command)
            {
                Expression<Func<Affiliation, bool>> affiliationBasedOnCommand = a =>
                    a.JobTitles == command.JobTitles &&
                    a.IsClaimingStudent == command.IsClaimingStudent &&
                    a.IsClaimingEmployee == command.IsClaimingEmployee &&
                    a.IsClaimingInternationalOffice == command.IsClaimingInternationalOffice &&
                    a.IsClaimingAdministrator == command.IsClaimingAdministrator &&
                    a.IsClaimingFaculty == command.IsClaimingFaculty &&
                    a.IsClaimingStaff == command.IsClaimingStaff;
                return affiliationBasedOnCommand;
            }
        }
    }
}
