using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public static class UpdateMyEmailValueValidatorFacts
    {
        [TestClass]
        public class ThePrincipalProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new UpdateMyEmailValueCommand { Principal = null };
                var validator = new UpdateMyEmailValueValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecausePrincipalWasNull);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IdentityName_IsNull()
            {
                const string principalIdentityName = null;
                // ReSharper disable ExpressionIsAlwaysNull
                var principal = principalIdentityName.AsPrincipal();
                // ReSharper restore ExpressionIsAlwaysNull
                var command = new UpdateMyEmailValueCommand { Principal = principal };
                var validator = new UpdateMyEmailValueValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecausePrincipalWasNull);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IdentityName_IsEmptyString()
            {
                var principalIdentityName = string.Empty;
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand { Principal = principal };
                var validator = new UpdateMyEmailValueValidator(null);

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
            public void IsInvalidWhen_IdentityName_IsWhiteSpace()
            {
                const string principalIdentityName = "\t";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand { Principal = principal };
                var validator = new UpdateMyEmailValueValidator(null);
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
            public void IsInvalidWhen_IdentityName_DoesNotMatchUser()
            {
                const string principalIdentityName = "user@domain.";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand { Principal = principal };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<User>()).Returns(new User[] { }.AsQueryable);
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

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
            public void IsValidWhen_IdentityName_IsNotEmpty_AndMatchesUser()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand { Principal = principal };
                var user = new User { Name = principal.Identity.Name };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

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
            public void IsInvalidWhen_DoesNotMatchEmail_ForPrincipal()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand
                {
                    Principal = principal,
                    Number = 11,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<User>()).Returns(new User[] { }.AsQueryable);
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Number");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        command.Number, command.Principal.Identity.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesEmail_ForPrincipal()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand
                {
                    Principal = principal,
                    Number = 1,
                    NewValue = principalIdentityName.ToUpper(),
                };
                var emailAddress = new EmailAddress
                {
                    Number = command.Number,
                    Value = command.NewValue,
                    Person = new Person
                    {
                        User = new User
                        {
                            Name = principal.Identity.Name,
                        },
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                entities.Setup(m => m.Query<User>()).Returns(new User[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Number");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_PrincipalWasNull()
            {
                const string principalIdentityName = "user@domain.tld";
                var command = new UpdateMyEmailValueCommand
                {
                    Number = 1,
                    NewValue = principalIdentityName.ToUpper(),
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<User>()).Returns(new User[] { }.AsQueryable);
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Number");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheNewValueProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new UpdateMyEmailValueCommand { NewValue = null };
                var validator = new UpdateMyEmailValueValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidateEmailAddress.FailedBecauseValueWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new UpdateMyEmailValueCommand { NewValue = string.Empty };
                var validator = new UpdateMyEmailValueValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidateEmailAddress.FailedBecauseValueWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new UpdateMyEmailValueCommand { NewValue = "\t" };
                var validator = new UpdateMyEmailValueValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidateEmailAddress.FailedBecauseValueWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_FailsEmailAddressRegex()
            {
                var command = new UpdateMyEmailValueCommand { NewValue = "user@domain.", };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress,
                        command.NewValue));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_DoesNotMatchPreviousSpelling_CaseInsensitively()
            {
                const string principalIdentityName = "user@sub.domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyEmailValueCommand
                {
                    Principal = principal,
                    NewValue = "user@domain.tld",
                };
                var emailAddress = new EmailAddress
                {
                    Value = "user@domain2.tld",
                    Person = new Person
                    {
                        User = new User
                        {
                            Name = principal.Identity.Name,
                        },
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<User>()).Returns(new User[] { }.AsQueryable);
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseNewValueDidNotMatchCurrentValueCaseInvsensitively,
                        command.NewValue));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesPreviousSpelling_CaseInsensitively()
            {
                var command = new UpdateMyEmailValueCommand { NewValue = "User@Domain.Tld", };
                var emailAddress = new EmailAddress { Value = "user@domain.tld" };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_EmailAddressWasNull()
            {
                var command = new UpdateMyEmailValueCommand { NewValue = "User@Domain.Tld", };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateMyEmailValueValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "NewValue");
                error.ShouldBeNull();
            }
        }
    }
}
