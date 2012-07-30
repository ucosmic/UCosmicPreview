using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.People
{
    public static class CreatePersonHandlerFacts
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new CreatePersonHandler(null);
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
            public void CreatesPerson_WithDisplayName()
            {
                var command = new CreatePersonCommand
                {
                    DisplayName = "Display Name",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Person outPerson = null;
                entities.Setup(m => m.Create(It.IsAny<Person>()))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new CreatePersonHandler(entities.Object);

                handler.Handle(command);

                outPerson.ShouldNotBeNull();
                outPerson.DisplayName.ShouldEqual(command.DisplayName);
                outPerson.ShouldEqual(command.CreatedPerson);
                entities.Verify(m => m.Create(It.Is<Person>(p =>
                    p.DisplayName == command.DisplayName)), Times.Once());
            }

            [TestMethod]
            public void CreatesPerson_WithFirstName()
            {
                var command = new CreatePersonCommand
                {
                    FirstName = "Adam",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Person outPerson = null;
                entities.Setup(m => m.Create(It.IsAny<Person>()))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new CreatePersonHandler(entities.Object);

                handler.Handle(command);

                outPerson.ShouldNotBeNull();
                outPerson.ShouldEqual(command.CreatedPerson);
                outPerson.FirstName.ShouldEqual(command.FirstName);
                entities.Verify(m => m.Create(It.Is<Person>(p =>
                    p.FirstName == command.FirstName)), Times.Once());
            }

            [TestMethod]
            public void CreatesPerson_WithLastName()
            {
                var command = new CreatePersonCommand
                {
                    LastName = "West",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Person outPerson = null;
                entities.Setup(m => m.Create(It.IsAny<Person>()))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new CreatePersonHandler(entities.Object);

                handler.Handle(command);

                outPerson.ShouldNotBeNull();
                outPerson.LastName.ShouldEqual(command.LastName);
                entities.Verify(m => m.Create(It.Is<Person>(p =>
                    p.LastName == command.LastName)), Times.Once());
            }

            [TestMethod]
            public void CreatesPerson_AndUnregisteredUser()
            {
                var command = new CreatePersonCommand
                {
                    UserName = "user@domain.tld",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Person outPerson = null;
                entities.Setup(m => m.Create(It.IsAny<Person>()))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new CreatePersonHandler(entities.Object);

                handler.Handle(command);

                outPerson.ShouldNotBeNull();
                outPerson.ShouldEqual(command.CreatedPerson);
                outPerson.User.ShouldNotBeNull();
                outPerson.User.Name.ShouldEqual(command.UserName);
                outPerson.User.IsRegistered.ShouldBeFalse();
                entities.Verify(m => m.Create(It.Is<Person>(p =>
                    p.User.Name == command.UserName)), Times.Once());
            }

            [TestMethod]
            public void CreatesPerson_AndRegisteredUser()
            {
                var command = new CreatePersonCommand
                {
                    UserName = "user@domain.tld",
                    UserIsRegistered = true,
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Person outPerson = null;
                entities.Setup(m => m.Create(It.IsAny<Person>()))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new CreatePersonHandler(entities.Object);

                handler.Handle(command);

                outPerson.ShouldNotBeNull();
                outPerson.ShouldEqual(command.CreatedPerson);
                outPerson.User.ShouldNotBeNull();
                outPerson.User.IsRegistered.ShouldBeTrue();
                entities.Verify(m => m.Create(It.Is<Person>(p =>
                    p.User.IsRegistered == command.UserIsRegistered)), Times.Once());
            }
        }
    }
}
