using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    // ReSharper disable UnusedMember.Global
    public class ResetPasswordValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheTokenProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var command = new ResetPasswordCommand { Token = Guid.Empty };
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
                var command = new ResetPasswordCommand
                {
                    Token = Guid.NewGuid()
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    EmailConfirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword),
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
            public void IsInvalidWhen_MatchesUnintendedEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(-1),
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                        confirmation.Intent, confirmation.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesExpiredEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(-1),
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsExpired,
                        confirmation.Token, confirmation.ExpiresOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesRetiredEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    RetiredOnUtc = DateTime.UtcNow.AddMinutes(-1),
                    RedeemedOnUtc = DateTime.UtcNow,
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsRetired,
                        confirmation.Token, confirmation.RetiredOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesUnredeemedEntity()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseIsNotRedeemed,
                        confirmation.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesUnconfirmedEmail()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    RedeemedOnUtc = DateTime.UtcNow,
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    EmailAddress = new EmailAddress
                    {
                        Value = "user@domain.tld",
                    },
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        confirmation.EmailAddress.Value));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoUser()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    RedeemedOnUtc = DateTime.UtcNow,
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    EmailAddress = new EmailAddress
                    {
                        IsConfirmed = true,
                        Person = new Person
                        {
                            DisplayName = "Adam West"
                        }
                    }
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePerson.FailedBecauseUserWasNull,
                        confirmation.EmailAddress.Person.DisplayName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesSamlUser()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    RedeemedOnUtc = DateTime.UtcNow,
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    EmailAddress = new EmailAddress
                    {
                        IsConfirmed = true,
                        Person = new Person
                        {
                            User = new User
                            {
                                EduPersonTargetedId = "something",
                            }
                        }
                    }
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        confirmation.EmailAddress.Person.User.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesUser_WithNoLocalAccount()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    RedeemedOnUtc = DateTime.UtcNow,
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    EmailAddress = new EmailAddress
                    {
                        IsConfirmed = true,
                        Person = new Person
                        {
                            User = new User
                            {
                                Name = "something",
                            }
                        }
                    }
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateUser.FailedBecauseNameMatchedNoLocalMember,
                        confirmation.EmailAddress.Person.User.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesEntity_Intended_Unexpired_Unretired_Redeemed_WithNonSamlLocalUser()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddMinutes(1),
                    RedeemedOnUtc = DateTime.UtcNow,
                    EmailAddress = new EmailAddress
                    {
                        IsConfirmed = true,
                        Person = new Person
                        {
                            User = new User
                            {
                                Name = "local"
                            }
                        }
                    }
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    EmailConfirmation = confirmation,
                    LocalMemberExists = true,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheTicketProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new ResetPasswordCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Ticket");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseTicketWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new ResetPasswordCommand { Ticket = string.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Ticket");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseTicketWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new ResetPasswordCommand { Ticket = "\t" };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Ticket");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailConfirmation.FailedBecauseTicketWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsIncorrectMatch()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    Ticket = "ticket1",
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
                    Ticket = "ticket2",
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Ticket");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTicketWasIncorrect,
                        command.Ticket, confirmation.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsNotEmpty_AndIsCorrectMatch()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    Ticket = "tomato",
                };
                var command = new ResetPasswordCommand
                {
                    Token = confirmation.Token,
                    Ticket = "tomato"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    EmailConfirmation = confirmation,
                    Command = command,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Ticket");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_IsNotEmpty_AndConfirmationWasNull()
            {
                var command = new ResetPasswordCommand { Ticket = "test" };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Ticket");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePasswordProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new ResetPasswordCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new ResetPasswordCommand { Password = string.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new ResetPasswordCommand { Password = "\t" };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Length_IsLessThanSixCharacters()
            {
                var command = new ResetPasswordCommand { Password = "12345" };
                var scenarioOptions = new ScenarioOptions
                {
                    MinimumPasswordLength = 6,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordWasTooShort(6));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_Length_IsSixCharactersOrMore()
            {
                var command = new ResetPasswordCommand { Password = "123456" };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePasswordConfirmationProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new ResetPasswordCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new ResetPasswordCommand { PasswordConfirmation = string.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new ResetPasswordCommand { PasswordConfirmation = string.Empty };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_DoesNotEqualPassword_AndPasswordIsValid()
            {
                var command = new ResetPasswordCommand
                {
                    Password = "123456",
                    PasswordConfirmation = "12345",
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidatePassword.FailedBecausePasswordConfirmationDidNotEqualPassword);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_PasswordIsEmpty()
            {
                var command = new ResetPasswordCommand
                {
                    Password = string.Empty,
                    PasswordConfirmation = "123",
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_PasswordIsIncorrectLength()
            {
                var command = new ResetPasswordCommand
                {
                    Password = "123",
                    PasswordConfirmation = "1234",
                };
                var scenarioOptions = new ScenarioOptions
                {
                    MinimumPasswordLength = 6,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_EqualsPassword_AndPasswordIsValid()
            {
                var command = new ResetPasswordCommand
                {
                    Password = "123456",
                    PasswordConfirmation = "123456",
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }
        }

        private class ScenarioOptions
        {
            internal ResetPasswordCommand Command { get; set; }
            internal EmailConfirmation EmailConfirmation { get; set; }
            internal bool LocalMemberExists { get; set; }
            internal int MinimumPasswordLength { get; set; }
        }

        private static ResetPasswordValidator CreateValidator(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();
            var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
            if (scenarioOptions.Command != null)
                entities.Setup(m => m.Query<EmailConfirmation>())
                    .Returns(new[] { scenarioOptions.EmailConfirmation }.AsQueryable);
            var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
            passwords.Setup(p => p.MinimumPasswordLength).Returns(scenarioOptions.MinimumPasswordLength);
            if (scenarioOptions.EmailConfirmation != null &&
                scenarioOptions.EmailConfirmation.EmailAddress != null &&
                scenarioOptions.EmailConfirmation.EmailAddress.Person != null &&
                scenarioOptions.EmailConfirmation.EmailAddress.Person.User != null &&
                !string.IsNullOrWhiteSpace(scenarioOptions.EmailConfirmation.EmailAddress.Person.User.Name))
                passwords.Setup(m => m
                    .Exists(scenarioOptions.EmailConfirmation.EmailAddress.Person.User.Name))
                    .Returns(scenarioOptions.LocalMemberExists);
            return new ResetPasswordValidator(entities.Object, passwords.Object);
        }
    }
}
