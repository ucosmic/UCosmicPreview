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
                var command = new RedeemEmailConfirmationCommand { Token = Guid.NewGuid() };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(null as EmailConfirmation);
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
                var command = new RedeemEmailConfirmationCommand { Token = Guid.NewGuid() };
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(-1),
                };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(m => 
                    m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
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
            public void IsInvalidWhen_MatchesRedeemedEntity()
            {
                var command = new RedeemEmailConfirmationCommand { Token = Guid.NewGuid() };
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    RedeemedOnUtc = DateTime.UtcNow,
                };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(m =>
                    m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIRedeemed,
                        command.Token, confirmation.RedeemedOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsNotEmpty_AndMatches_Unexpired_UnRedeemed_Entity()
            {
                var command = new RedeemEmailConfirmationCommand { Token = Guid.NewGuid() };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(m =>
                    m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation
                    {
                        ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    });
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
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation());
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
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation());
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
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation());
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
            public void IsInvalidWhen_Confirmation_WasNull()
            {
                var command = new RedeemEmailConfirmationCommand { SecretCode = "test" };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(null as EmailConfirmation);
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseSecretCodeWasIncorrect, 
                        command.SecretCode, Guid.Empty));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrectMatch()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "tomatto",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "tomato"
                };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
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
                    SecretCode = "tomato",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    SecretCode = "tomato"
                };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
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
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation());
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
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation());
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
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(new EmailConfirmation());
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
            public void IsInvalidWhen_Confirmation_WasNull()
            {
                var command = new RedeemEmailConfirmationCommand { Intent = "test" };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(null as EmailConfirmation);
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Intent");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                        command.Intent, Guid.Empty));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrectMatch()
            {
                var confirmation = new EmailConfirmation
                {
                    Intent = "tomatto",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                    Intent = "tomato"
                };
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
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
                var scenarioOptions = new ScenarioOptions();
                scenarioOptions.MockQueryProcessor.Setup(
                    m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "SecretCode");
                error.ShouldBeNull();
            }
        }

        private class ScenarioOptions
        {
            internal ScenarioOptions()
            {
                MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
            }

            internal readonly Mock<IProcessQueries> MockQueryProcessor;
        }

        private static RedeemEmailConfirmationValidator CreateValidator(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();
            return new RedeemEmailConfirmationValidator(scenarioOptions.MockQueryProcessor.Object);
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(RedeemEmailConfirmationCommand command)
        {
            Expression<Func<GetEmailConfirmationQuery, bool>> confirmationQueryBasedOn = q => q.Token == command.Token;
            return confirmationQueryBasedOn;
        }
    }
}
