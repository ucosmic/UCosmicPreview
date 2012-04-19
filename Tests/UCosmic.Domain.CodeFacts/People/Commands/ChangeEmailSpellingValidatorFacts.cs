using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class ChangeEmailSpellingValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheUserNameProperty
        {
            [TestMethod]
            public void IsInvalidWhen_UserName_IsNull()
            {
                var command = new ChangeEmailSpellingCommand { UserName = null };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_UserName_IsEmptyString()
            {
                var command = new ChangeEmailSpellingCommand { UserName = string.Empty };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_UserName_IsWhiteSpace()
            {
                var command = new ChangeEmailSpellingCommand { UserName = "\t" };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_UserName_FailsEmailAddressRegex()
            {
                var command = new ChangeEmailSpellingCommand { UserName = "user@domain." };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsValidWhen_UserName_IsNotEmpty_AndPassesEmailAddressRegex()
            {
                var command = new ChangeEmailSpellingCommand { UserName = "user@domain.tld" };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "UserName");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheNumberProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Number_IsLessThan1()
            {
                var command = new ChangeEmailSpellingCommand { Number = 0 };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Number");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsValidWhen_Number_IsGreaterThanZero()
            {
                var command = new ChangeEmailSpellingCommand { Number = 1 };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Number");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheNewValueProperty
        {
            [TestMethod]
            public void IsInvalidWhen_NewValue_IsNull()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = null };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_IsEmptyString()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = string.Empty };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_IsWhiteSpace()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = "\t" };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_FailsEmailAddressRegex()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = "user@domain." };
                var validator = new ChangeEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_PreviousSpelling_CannotBeDetermined()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = "user@domain.tld" };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressByUserNameAndNumber = q =>
                    q.UserName == command.UserName;
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumber)))
                    .Returns(null as EmailAddress);
                var validator = new ChangeEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ChangeEmailSpellingValidator.ChangeEmailSpellingErrorMessage);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_DoesNotMatchPreviousSpelling_CaseInvariantly()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = "user@domain.tld" };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressByUserNameAndNumber = q =>
                    q.UserName == command.UserName;
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumber)))
                    .Returns(new EmailAddress { Value = "user@domain2.tld" });
                var validator = new ChangeEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ChangeEmailSpellingValidator.ChangeEmailSpellingErrorMessage);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_NewValue_MatchesPreviousSpelling_CaseInvariantly()
            {
                var command = new ChangeEmailSpellingCommand { NewValue = "User@Domain.Tld" };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressByUserNameAndNumber = q =>
                    q.UserName == command.UserName;
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumber)))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new ChangeEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldBeNull();
            }
        }
    }
}
