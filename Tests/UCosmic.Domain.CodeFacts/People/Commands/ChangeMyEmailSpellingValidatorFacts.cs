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
    public class ChangeMyEmailSpellingValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class ThePrincipalProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Principal_IsNull()
            {
                var command = new ChangeMyEmailSpellingCommand { Principal = null };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsNull()
            {
                const string principalIdentityName = null;
                // ReSharper disable ExpressionIsAlwaysNull
                var principal = principalIdentityName.AsPrincipal();
                // ReSharper restore ExpressionIsAlwaysNull
                var command = new ChangeMyEmailSpellingCommand { Principal = principal };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecauseIdentityNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsEmptyString()
            {
                var principalIdentityName = string.Empty;
                var principal = principalIdentityName.AsPrincipal();
                var command = new ChangeMyEmailSpellingCommand { Principal = principal };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecauseIdentityNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsWhiteSpace()
            {
                const string principalIdentityName = "\t";
                var principal = principalIdentityName.AsPrincipal();
                var command = new ChangeMyEmailSpellingCommand { Principal = principal };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecauseIdentityNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_DoesNotMatchUser()
            {
                const string principalIdentityName = "user@domain.";
                var principal = principalIdentityName.AsPrincipal();
                var command = new ChangeMyEmailSpellingCommand { Principal = principal };
                var queryProcessor = new Mock<IProcessQueries>();
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName))).Returns(null as User);
                var validator = new ChangeMyEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        principalIdentityName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_PrincipalIdentityName_IsNotEmpty_AndMatchesUser()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new ChangeMyEmailSpellingCommand { Principal = principal };
                var queryProcessor = new Mock<IProcessQueries>();
                Expression<Func<GetUserByNameQuery, bool>> userByPrincipalIdentityName = q =>
                    q.Name == command.Principal.Identity.Name;
                queryProcessor.Setup(m => m.Execute(It.Is(userByPrincipalIdentityName))).Returns(new User());
                var validator = new ChangeMyEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheNumberProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Number_IsLessThan1()
            {
                var command = new ChangeMyEmailSpellingCommand { Number = 0 };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Number");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsValidWhen_Number_IsGreaterThanZero()
            {
                var command = new ChangeMyEmailSpellingCommand { Number = 1 };
                var validator = new ChangeMyEmailSpellingValidator(null);
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
                var command = new ChangeMyEmailSpellingCommand { NewValue = null };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_IsEmptyString()
            {
                var command = new ChangeMyEmailSpellingCommand { NewValue = string.Empty };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_IsWhiteSpace()
            {
                var command = new ChangeMyEmailSpellingCommand { NewValue = "\t" };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_FailsEmailAddressRegex()
            {
                var command = new ChangeMyEmailSpellingCommand { NewValue = "user@domain." };
                var validator = new ChangeMyEmailSpellingValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_PreviousSpelling_CannotBeDetermined()
            {
                var command = new ChangeMyEmailSpellingCommand { NewValue = "user@domain.tld" };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressByUserNameAndNumber = q =>
                    q.Principal == command.Principal;
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumber)))
                    .Returns(null as EmailAddress);
                var validator = new ChangeMyEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_NewValue_DoesNotMatchPreviousSpelling_CaseInvariantly()
            {
                var command = new ChangeMyEmailSpellingCommand { NewValue = "user@domain.tld" };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressByUserNameAndNumber = q =>
                    q.Principal == command.Principal;
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumber)))
                    .Returns(new EmailAddress { Value = "user@domain2.tld" });
                var validator = new ChangeMyEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_NewValue_MatchesPreviousSpelling_CaseInvariantly()
            {
                var command = new ChangeMyEmailSpellingCommand { NewValue = "User@Domain.Tld" };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressByUserNameAndNumber = q =>
                    q.Principal == command.Principal;
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressByUserNameAndNumber)))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new ChangeMyEmailSpellingValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldBeNull();
            }
        }
    }
}
