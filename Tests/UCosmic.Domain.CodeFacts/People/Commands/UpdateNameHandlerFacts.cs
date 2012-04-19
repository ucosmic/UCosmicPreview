using System;
using System.Linq.Expressions;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class UpdateNameHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new UpdateNameHandler(null, null);
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
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName =
                    q => q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(null as User);
                var handler = new UpdateNameHandler(queryProcessor.Object, null);
                NullReferenceException exception = null;

                try
                {
                    handler.Handle(command);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                queryProcessor.Verify(m => m.Execute(It.Is(userByPrincipalIdentityName)), Times.Once());
                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesQuery_ToGenerateDisplayName_WhenDisplayNameIsDerived()
            {
                const string generatedDisplayName = "Generated Displayname";
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    IsDisplayNameDerived = true,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                Expression<Func<GenerateDisplayNameQuery, bool>> generateDisplayName = q =>
                    q.Salutation == command.Salutation &&
                    q.FirstName == command.FirstName &&
                    q.MiddleName == command.MiddleName &&
                    q.LastName == command.LastName &&
                    q.Suffix == command.Suffix;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User { Person = new Person() });
                queryProcessor.Setup(m => m.Execute(It.Is(generateDisplayName)))
                    .Returns(generatedDisplayName);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.IsAny<Person>()));
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                queryProcessor.Verify(m => m.Execute(It.Is(generateDisplayName)), Times.Once());
            }

            [TestMethod]
            public void DoesNotExecuteQuery_ToGenerateDisplayName_WhenDisplayNameIsNotDerived()
            {
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    IsDisplayNameDerived = false,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User { Person = new Person() });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.IsAny<Person>()));
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                queryProcessor.Verify(m => m.Execute(It.IsAny<GenerateDisplayNameQuery>()), Times.Never());
            }

            [TestMethod]
            public void UpdatesPersonName_WhenFieldsHaveChanged()
            {
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    DisplayName = "Display Name",
                    FirstName = "Display",
                    LastName = "Name",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person()
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)));
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(personBasedOnCommand)), Times.Once());
            }

            [TestMethod]
            public void DoesNotUpdatePersonName_WhenFieldsHaveNotChanged()
            {
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    DisplayName = "Display Name",
                    FirstName = "Display",
                    LastName = "Name",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            DisplayName = "Display Name",
                            FirstName = "Display",
                            LastName = "Name",
                        }
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)));
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(personBasedOnCommand)), Times.Never());
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenIsDisplayNameDerived_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    IsDisplayNameDerived = false,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            IsDisplayNameDerived = true,
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

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
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    DisplayName = "Display Name",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            DisplayName = "Old Name"
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.DisplayName.ShouldEqual(command.DisplayName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenSalutation_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    Salutation = "Dr",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            Salutation = "Dr.",
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.Salutation.ShouldEqual(command.Salutation);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenFirstName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    FirstName = "Adam ",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            FirstName = "Adam",
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.FirstName.ShouldEqual(command.FirstName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenMiddleName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    MiddleName = "B",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            MiddleName = "B.",
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.MiddleName.ShouldEqual(command.MiddleName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenLastName_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    LastName = " West",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            LastName = "West",
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.LastName.ShouldEqual(command.LastName);
            }

            [TestMethod]
            public void IncrementsChangeCount_WhenSuffix_IsDifferent()
            {
                Person outPerson = null;
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                    Suffix = "Jr.",
                };
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            Suffix = "Jr",
                        },
                    });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                Expression<Func<Person, bool>> personBasedOnCommand = p =>
                    p.IsDisplayNameDerived == command.IsDisplayNameDerived &&
                    p.DisplayName == command.DisplayName &&
                    p.Salutation == command.Salutation &&
                    p.FirstName == command.FirstName &&
                    p.MiddleName == command.MiddleName &&
                    p.LastName == command.LastName &&
                    p.Suffix == command.Suffix;
                entities.Setup(m => m.Update(It.Is(personBasedOnCommand)))
                    .Callback((Entity entity) => outPerson = (Person)entity);
                var handler = new UpdateNameHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.ChangeCount.ShouldEqual(1);
                outPerson.Suffix.ShouldEqual(command.Suffix);
            }
        }
    }
}
