using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

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
                var handler = new UpdateMyAffiliationHandler(null, null);
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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(null as Affiliation);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, null);
                NullReferenceException exception = null;

                try
                {
                    handler.Handle(command);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                queryProcessor.Verify(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))), 
                    Times.Once());
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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(AffiliationBasedOn(command))));
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(AffiliationBasedOn(command))));
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        JobTitles = "old value",
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsClaimingStudent = false,
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsClaimingEmployee = false,
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsClaimingInternationalOffice = false,
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsClaimingAdministrator = false,
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsClaimingFaculty = false,
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(AffiliationQueryBasedOn(command))))
                    .Returns(new Affiliation
                    {
                        IsClaimingStaff = false,
                        IsAcknowledged = true,
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                var affiliationBasedOnCommand = AffiliationBasedOn(command);
                entities.Setup(m => m.Update(It.Is(affiliationBasedOnCommand)))
                    .Callback((Entity entity) => outAffiliation = (Affiliation)entity);
                var handler = new UpdateMyAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outAffiliation.IsClaimingStaff.ShouldEqual(command.IsClaimingStaff);
            }

            private static Expression<Func<GetMyAffiliationByEstablishmentIdQuery, bool>> AffiliationQueryBasedOn(UpdateMyAffiliationCommand command)
            {
                Expression<Func<GetMyAffiliationByEstablishmentIdQuery, bool>> queryFromCommandParameters =
                    q => q.Principal == command.Principal && q.EstablishmentId == command.EstablishmentId;
                return queryFromCommandParameters;
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
