using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailFormValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheSecretCodeProperty
        {
            private const string PropertyName = "SecretCode";

            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ConfirmEmailForm();
                var validator = new ConfirmEmailFormValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ConfirmEmailForm
                {
                    SecretCode = String.Empty
                };
                var validator = new ConfirmEmailFormValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ConfirmEmailForm
                {
                    SecretCode = "   ",
                };
                var validator = new ConfirmEmailFormValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseSecretCodeWasEmpty);
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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Intent_IsNull()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation());
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Intent_IsEmptyString()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret",
                    Intent = string.Empty,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation());
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Intent_IsWhiteSpace()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret",
                    Intent = " \r",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation());
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Intent_IsIncorrect()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret",
                    Intent = "intent1",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation
                    {
                        Intent = "intent2",
                    });
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseOfInconsistentData);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrect()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret1",
                    Intent = "intent1",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation
                    {
                        Intent = "intent1",
                        SecretCode = "secret2"
                    });
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ConfirmEmailFormValidator.FailedBecauseSecretCodeWasIncorrect);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsIncorrect_ButConfirmationIsRedeemed()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret1",
                    Intent = "intent1",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation
                    {
                        Intent = "intent1",
                        SecretCode = "secret2",
                        RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-10),
                    });
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_IsCorrect_AndConfirmationIsNotRedeemed()
            {
                var validated = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "secret1",
                    Intent = "intent1",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation
                    {
                        Intent = "intent1",
                        SecretCode = "secret1",
                    });
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheIsExpiredProperty
        {
            private const string PropertyName = "IsExpired";

            [TestMethod]
            public void IsInvalidWhen_ConfirmationIsExpired()
            {
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    SecretCode = "secret",
                    Intent = "intent",
                };
                var validated = new ConfirmEmailForm
                {
                    Token = confirmation.Token,
                    SecretCode = confirmation.SecretCode,
                    Intent = confirmation.Intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsExpired,
                        confirmation.Token, confirmation.ExpiresOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_ConfirmationIsNotExpired()
            {
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(5),
                    SecretCode = "secret",
                    Intent = "intent",
                };
                var validated = new ConfirmEmailForm
                {
                    Token = confirmation.Token,
                    SecretCode = confirmation.SecretCode,
                    Intent = confirmation.Intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_ConfirmationWasNull()
            {
                var validated = new ConfirmEmailForm();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheIsRedeemedProperty
        {
            private const string PropertyName = "IsRedeemed";

            [TestMethod]
            public void IsInvalidWhen_ConfirmationIsRedeemed()
            {
                var confirmation = new EmailConfirmation
                {
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    SecretCode = "secret",
                    Intent = "intent",
                };
                var validated = new ConfirmEmailForm
                {
                    Token = confirmation.Token,
                    SecretCode = confirmation.SecretCode,
                    Intent = confirmation.Intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsRedeemed,
                        confirmation.Token, confirmation.RedeemedOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_ConfirmationIsNotRedeemed()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "secret",
                    Intent = "intent",
                };
                var validated = new ConfirmEmailForm
                {
                    Token = confirmation.Token,
                    SecretCode = confirmation.SecretCode,
                    Intent = confirmation.Intent,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_ConfirmationWasNull()
            {
                var validated = new ConfirmEmailForm();
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ConfirmEmailFormValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(ConfirmEmailForm validated)
        {
            return q => q.Token == validated.Token;
        }
    }
}
