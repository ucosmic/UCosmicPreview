using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Identity;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class CreatePersonValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void HasErrorWhen_DisplayName_IsNull()
            {
                var command = new CreatePersonCommand { DisplayName = null };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().PropertyName.ShouldEqual("DisplayName");
            }

            [TestMethod]
            public void HasErrorWhen_UserIsAlreadyAssociated_WithAnotherPerson()
            {
                const string userName = "user@domain.tld";
                const string userDisplayName = "Bruce Wayne";
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = userName,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.UserName)))
                    .Returns(new User { Person = new Person { DisplayName = userDisplayName, } });
                var validator = new CreatePersonValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().PropertyName.ShouldEqual("UserName");
                results.Errors.Single().ErrorMessage.ShouldEqual(string.Format(
                    CreatePersonValidator.UserIsAlreadyAssociatedWithAnotherPersonErrorFormat,
                        userName, personDisplayName, userDisplayName));
            }

            [TestMethod]
            public void HasNoErrorWhen_UserNameIsNull()
            {
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = null,
                };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeTrue();
                results.Errors.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void HasNoErrorWhen_UserNameIsEmptyString()
            {
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = string.Empty,
                };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeTrue();
                results.Errors.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void HasNoErrorWhen_UserNameIsWhiteSpace()
            {
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = "\t",
                };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeTrue();
                results.Errors.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void HasNoErrorWhen_UserNameDoesNotMatch_AnyUser()
            {
                const string userName = "user@domain.tld";
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = userName,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.UserName)))
                    .Returns(null as User);
                var validator = new CreatePersonValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeTrue();
                results.Errors.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void HasNoErrorWhen_UserNameMatches_UserWithNullPerson()
            {
                const string userName = "user@domain.tld";
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = userName,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.UserName)))
                    .Returns(new User { Person = null });
                var validator = new CreatePersonValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeTrue();
                results.Errors.Count.ShouldEqual(0);
            }
        }
    }
}
