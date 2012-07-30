using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public static class RedeemEmailConfirmationValidatorFacts
    {
        [TestClass]
        public class TheTokenProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var command = new RedeemEmailConfirmationCommand { Token = Guid.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        Guid.Empty));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoEntity()
            {
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = Guid.NewGuid(),

                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword),
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        command.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesExpiredEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(-1),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsExpired,
                        command.Token, confirmation.ExpiresOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesRetiredEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    RetiredOnUtc = DateTime.UtcNow.AddMinutes(-1),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsRetired,
                        command.Token, confirmation.RetiredOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsNotEmpty_AndMatchesEntity_Unexpired_UnRedeemed_Unretired()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheSecretCodeProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new RedeemEmailConfirmationCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new RedeemEmailConfirmationCommand { SecretCode = string.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new RedeemEmailConfirmationCommand { SecretCode = "\t" };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrectMatch()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    SecretCode = "secret1",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "secret2",
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasIncorrect,
                        command.SecretCode, confirmation.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsNotEmpty_AndIsCorrectMatch()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    SecretCode = "secret",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "secret",
                    Intent = EmailConfirmationIntent.CreatePassword,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_IsNotEmpty_AndConfirmationWasNull()
            {
                var command = new RedeemEmailConfirmationCommand
                {
                    SecretCode = "test"
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheIntentProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsIncorrectMatch()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword);
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    Intent = EmailConfirmationIntent.CreatePassword,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                        command.Intent, confirmation.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsCorrectMatch()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    SecretCode = "tomato",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "tomato",
                    Intent = EmailConfirmationIntent.CreatePassword,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_Confirmation_WasNull()
            {
                var command = new RedeemEmailConfirmationCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldBeNull();
            }
        }

        private class ScenarioOptions
        {
            internal EmailConfirmation EmailConfirmation { get; set; }
        }

        private static RedeemEmailConfirmationValidator CreateValidator(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();
            var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
            entities.Setup(m => m.Query<EmailConfirmation>())
                .Returns(new[] { scenarioOptions.EmailConfirmation }.AsQueryable);
            return new RedeemEmailConfirmationValidator(entities.Object);
        }
    }
}
