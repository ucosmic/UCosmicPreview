using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    // ReSharper disable UnusedMember.Global
    public class RedeemEmailConfirmationValidatorFacts
    // ReSharper restore UnusedMember.Global
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
                    Token = Guid.NewGuid()
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
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
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(-1),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command, 
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
                var confirmation = new EmailConfirmation
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
                    Command = command,
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
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
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
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "secret1",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "secret2"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                    Command = command,
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
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "secret",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "secret"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                    Command = command,
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
            public void IsInvalidWhen_IsNull()
            {
                var command = new RedeemEmailConfirmationCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseIntentWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new RedeemEmailConfirmationCommand { Intent = string.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseIntentWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new RedeemEmailConfirmationCommand { Intent = "\t" };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseIntentWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrectMatch()
            {
                var confirmation = new EmailConfirmation
                {
                    Intent = "intent1",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    Intent = "intent2"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                    Command = command,
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
            public void IsValidWhen_IsNotEmpty_AndIsCorrectMatch()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "tomato",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "tomato"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                    Command = command,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_Confirmation_WasNull()
            {
                var command = new RedeemEmailConfirmationCommand
                {
                    Intent = "test"
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldBeNull();
            }
        }

        private class ScenarioOptions
        {
            internal RedeemEmailConfirmationCommand Command { get; set; }
            internal EmailConfirmation EmailConfirmation { get; set; }
        }

        private static RedeemEmailConfirmationValidator CreateValidator(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();
            var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
            queryProcessor.Setup(m => m
                .Execute(It.Is(ConfirmationQueryBasedOn(scenarioOptions.Command))))
                .Returns(scenarioOptions.EmailConfirmation);
            return new RedeemEmailConfirmationValidator(queryProcessor.Object);
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(RedeemEmailConfirmationCommand command)
        {
            Expression<Func<GetEmailConfirmationQuery, bool>> confirmationQueryBasedOn = q => q.Token == command.Token;
            return confirmationQueryBasedOn;
        }
    }
}
