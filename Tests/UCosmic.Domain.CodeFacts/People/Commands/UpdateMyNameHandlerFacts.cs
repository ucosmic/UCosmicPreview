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
    public class UpdateMyNameHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new UpdateMyNameHandler(null);
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
            public void ExecutesQuery_ToGetUserPerson_FromPrincipalIdentityName()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                };
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person(),
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Get<User>(), Times.Once());
            }

            [TestMethod]
            public void UpdatesPersonName_WhenFieldsHaveChanged()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person(),
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    DisplayName = "Display Name",
                    FirstName = "Display",
                    LastName = "Name",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))));
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(PersonBasedOn(command))), Times.Once());
            }

            [TestMethod]
            public void DoesNotUpdatePersonName_WhenFieldsHaveNotChanged()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        DisplayName = "Display Name",
                        FirstName = "Display",
                        LastName = "Name",
                    }
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    DisplayName = user.Person.DisplayName,
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                };

                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(x => x.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))));
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<Entity>()), Times.Never());
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsDisplayNameDerived_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        IsDisplayNameDerived = true,
                    }
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    IsDisplayNameDerived = false,
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.IsDisplayNameDerived.ShouldEqual(command.IsDisplayNameDerived);
                outPerson.IsDisplayNameDerived.ShouldBeFalse();
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenDisplayName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person(),
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    DisplayName = "Display Name",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.DisplayName.ShouldEqual(command.DisplayName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenSalutation_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        Salutation = "Dr.",
                    },
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    Salutation = "Dr",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.Salutation.ShouldEqual(command.Salutation);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenFirstName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        FirstName = "Adam",
                    },
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    FirstName = "Adam ",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.FirstName.ShouldEqual(command.FirstName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenMiddleName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        MiddleName = "B.",
                    },
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    MiddleName = "B",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.MiddleName.ShouldEqual(command.MiddleName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenLastName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        LastName = "West",
                    },
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    LastName = " West",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.LastName.ShouldEqual(command.LastName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenSuffix_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var user = new User
                {
                    Name = principalIdentityName,
                    Person = new Person
                    {
                        Suffix = "Jr",
                    },
                };
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                    Suffix = "Jr.",
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(PersonBasedOn(command))))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateMyNameHandler(entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.Suffix.ShouldEqual(command.Suffix);
            }

            private static Expression<Func<Person, bool>> PersonBasedOn(UpdateMyNameCommand command)
            {
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                return personBasedOnCommand;
            }
        }
    }
}
