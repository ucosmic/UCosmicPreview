using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class ConfirmEmailValidatorFacts
    {
        [TestClass]
        public class TheSecretCodeProperty
        {
            private const string PropertyName = "SecretCode";

            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ConfirmEmailForm();
                var validator = new ConfirmEmailValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailValidator.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ConfirmEmailForm
                {
                    SecretCode = String.Empty
                };
                var validator = new ConfirmEmailValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailValidator.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ConfirmEmailForm
                {
                    SecretCode = "   ",
                };
                var validator = new ConfirmEmailValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailValidator.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Token_MatchesNoEntity()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret",
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new EmailConfirmation[] { }.AsQueryable);
                var validator = new ConfirmEmailValidator(entities.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Intent_IsIncorrect()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret",
                    Intent = EmailConfirmationIntent.CreatePassword,
                };
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var validator = new ConfirmEmailValidator(entities.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrect()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    SecretCode = "secret2",
                };
                var validated = new ConfirmEmailForm
                {
                    Token = confirmation.Token,
                    SecretCode = "secret1",
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var validator = new ConfirmEmailValidator(entities.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailValidator.FailedBecauseSecretCodeWasIncorrect);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsCorrect_AndConfirmationIsNotRedeemed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    SecretCode = "secret1",
                };
                var validated = new ConfirmEmailForm
                {
                    Token = confirmation.Token,
                    SecretCode = "secret1",
                    Intent = EmailConfirmationIntent.CreatePassword,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var validator = new ConfirmEmailValidator(entities.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }
    }
}
