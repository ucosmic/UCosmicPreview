using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class CreateAffiliationHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                var handler = new CreateAffiliationHandler(null, null);
                ArgumentNullException exception = null;
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
            public void ExecutesQuery_ForPerson()
            {
                const int personId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(null as Person);
                var handler = new CreateAffiliationHandler(queryProcessor.Object, null);
                NullReferenceException exception = null;
                try
                {
                    handler.Handle(command);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesCreate_OnAffiliationEntity()
            {
                const int personId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Create(It.Is(AffiliationBasedOn(command))));
                var handler = new CreateAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Create(It.Is(AffiliationBasedOn(command))), Times.Once());
            }

            [TestMethod]
            public void CreatesAffiliation_WithEstablishmentId()
            {
                Affiliation outEntity = null;
                const int personId = 13;
                const int establishmentId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                    EstablishmentId = establishmentId,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Create(It.Is(AffiliationBasedOn(command))))
                    .Callback((Entity entity) => outEntity = (Affiliation)entity);
                var handler = new CreateAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                outEntity.ShouldNotBeNull();
                outEntity.EstablishmentId.ShouldEqual(command.EstablishmentId);
            }

            [TestMethod]
            public void CreatesAffiliation_WithIsClaimingStudent()
            {
                Affiliation outEntity = null;
                const int personId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                    IsClaimingStudent = true,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Create(It.Is(AffiliationBasedOn(command))))
                    .Callback((Entity entity) => outEntity = (Affiliation)entity);
                var handler = new CreateAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                outEntity.ShouldNotBeNull();
                outEntity.IsClaimingStudent.ShouldEqual(command.IsClaimingStudent);
            }

            [TestMethod]
            public void CreatesAffiliation_WithIsClaimingEmployee()
            {
                Affiliation outEntity = null;
                const int personId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                    IsClaimingEmployee = true,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Create(It.Is(AffiliationBasedOn(command))))
                    .Callback((Entity entity) => outEntity = (Affiliation)entity);
                var handler = new CreateAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                outEntity.ShouldNotBeNull();
                outEntity.IsClaimingEmployee.ShouldEqual(command.IsClaimingEmployee);
            }

            [TestMethod]
            public void CreatesAffiliation_WithIsDefault_WhenPersonHasNoDefaultAffiliation()
            {
                Affiliation outEntity = null;
                const int personId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person());
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Create(It.Is(AffiliationBasedOn(command))))
                    .Callback((Entity entity) => outEntity = (Affiliation)entity);
                var handler = new CreateAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                outEntity.ShouldNotBeNull();
                outEntity.IsDefault.ShouldBeTrue();
            }

            [TestMethod]
            public void CreatesAffiliation_WithNotIsDefault_WhenPersonAlreadyHasDefaultAffiliation()
            {
                Affiliation outEntity = null;
                const int personId = 13;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person
                    {
                        Affiliations = new Collection<Affiliation>
                        {
                            new Affiliation { IsDefault = true, }
                        }
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Create(It.Is(AffiliationBasedOn(command))))
                    .Callback((Entity entity) => outEntity = (Affiliation)entity);
                var handler = new CreateAffiliationHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                outEntity.ShouldNotBeNull();
                outEntity.IsDefault.ShouldBeFalse();
            }

            private static Expression<Func<GetPersonByIdQuery, bool>> PersonQueryBasedOn(CreateAffiliationCommand command)
            {
                Expression<Func<GetPersonByIdQuery, bool>> personQueryBasedOn = q =>
                    q.Id == command.PersonId &&
                    q.EagerLoad != null &&
                    q.EagerLoad.Count() == 1
                ;
                return personQueryBasedOn;
            }

            private static Expression<Func<Affiliation, bool>> AffiliationBasedOn(CreateAffiliationCommand command)
            {
                Expression<Func<Affiliation, bool>> affiliationBasedOn = e => 
                    e.EstablishmentId == command.EstablishmentId &&
                    e.IsClaimingStudent == command.IsClaimingStudent &&
                    e.IsClaimingEmployee == command.IsClaimingEmployee
                ;
                return affiliationBasedOn;
            }
        }
    }
}
