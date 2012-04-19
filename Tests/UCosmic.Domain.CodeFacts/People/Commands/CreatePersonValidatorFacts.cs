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
        public class TheDisplayNameProperty
        {
            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsNull()
            {
                var command = new CreatePersonCommand { DisplayName = null };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsEmptyString()
            {
                var command = new CreatePersonCommand { DisplayName = string.Empty };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsWhiteSpace()
            {
                var command = new CreatePersonCommand { DisplayName = " " };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsValidWhen_DisplayName_IsNotEmpty()
            {
                var command = new CreatePersonCommand { DisplayName = "Adam West" };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheUserNameProperty
        {
            [TestMethod]
            public void IsInvalidWhen_UserIsAlreadyAssociated_WithAnotherPerson()
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
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    CreatePersonValidator.UserIsAlreadyAssociatedWithAnotherPersonErrorFormat,
                        userName, personDisplayName, userDisplayName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_UserNameIsNull()
            {
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = null,
                };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_UserNameIsEmptyString()
            {
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = string.Empty,
                };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_UserNameIsWhiteSpace()
            {
                const string personDisplayName = "Adam West";
                var command = new CreatePersonCommand
                {
                    DisplayName = personDisplayName,
                    UserName = "\t",
                };
                var validator = new CreatePersonValidator(null);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_UserNameDoesNotMatch_AnyUser()
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldBeNull();
            }
        }
    }
}
