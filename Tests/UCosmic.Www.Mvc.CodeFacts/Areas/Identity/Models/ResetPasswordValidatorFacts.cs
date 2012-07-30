using System;
using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class ResetPasswordValidatorFacts
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_ResetPasswordForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var validator = container.GetInstance<IValidator<ResetPasswordForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<ResetPasswordValidator>();
            }
        }

        [TestClass]
        public class TheTokenProperty
        {
            private const string PropertyName = "Token";

            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var validated = new ResetPasswordForm
                {
                    Token = Guid.Empty,
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        validated.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoEntity()
            {
                var validated = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new EmailConfirmation[] { }.AsQueryable);
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(entities.Object, passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        validated.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword);
                var validated = new ResetPasswordForm
                {
                    Token = confirmation.Token,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(entities.Object, passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePasswordProperty
        {
            private const string PropertyName = "Password";

            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ResetPasswordForm();
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ResetPasswordForm
                {
                    Password = string.Empty
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "   ",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_LengthIs_LessThanSixCharacters()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "12345",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ResetPasswordValidator.FailedBecausePasswordWasTooShort,
                        6));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_LengthIs_MoreThanFiveCharacters()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "123456",
                };
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(entities.Object, passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePasswordConfirmationProperty
        {
            private const string PropertyName = "PasswordConfirmation";

            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ResetPasswordForm();
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ResetPasswordForm
                {
                    PasswordConfirmation = string.Empty
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ResetPasswordForm
                {
                    PasswordConfirmation = "   ",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_NotEqualToPassword()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "123456",
                    PasswordConfirmation = "123457",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationDidNotEqualPassword);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_NotEqualToPassword_AndPasswordIsNull()
            {
                var validated = new ResetPasswordForm
                {
                    PasswordConfirmation = "123",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_NotEqualToPassword_AndPasswordIsEmptyString()
            {
                var validated = new ResetPasswordForm
                {
                    Password = string.Empty,
                    PasswordConfirmation = "123",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_NotEqualToPassword_AndPasswordIsWhiteSpace()
            {
                var validated = new ResetPasswordForm
                {
                    Password = " \t ",
                    PasswordConfirmation = "123",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_EqualsPassword()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "123456",
                    PasswordConfirmation = "123456",
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(p => p.MinimumPasswordLength).Returns(6);
                var validator = CreateValidator(passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        private static ResetPasswordValidator CreateValidator(IQueryEntities entities = null, IStorePasswords passwords = null)
        {
            return new ResetPasswordValidator(entities, passwords);
        }

        private static ResetPasswordValidator CreateValidator(IStorePasswords passwords)
        {
            return new ResetPasswordValidator(null, passwords);
        }

        //private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(ResetPasswordForm validated)
        //{
        //    return q => q.Token == validated.Token;
        //}
    }
}
