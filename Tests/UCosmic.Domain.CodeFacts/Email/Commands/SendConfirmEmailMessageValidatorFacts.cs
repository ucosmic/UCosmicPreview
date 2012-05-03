using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    // ReSharper disable UnusedMember.Global
    public class SendConfirmEmailMessageValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEmailAddressProperty
        {
            private const string PropertyName = "EmailAddress";

            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new SendConfirmEmailMessageCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailAddress.FailedBecauseValueWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = string.Empty
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailAddress.FailedBecauseValueWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "\r\n",
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailAddress.FailedBecauseValueWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsNotEmailAddress()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.",
                };
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoPerson()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                        command.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoEstablishment()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    DisplayName = "Adam West"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEstablishment.FailedBecauseEmailMatchedNoEntity,
                        command.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNonMemberEstablishment()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    DisplayName = "Adam West"
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = new Establishment(),
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEstablishment.FailedBecauseEstablishmentIsNotMember,
                        scenarioOptions.Establishment.RevisionId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesSamlEstablishment_AndIntentIsResetPassword()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var person = new Person
                {
                    DisplayName = "Adam West"
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    SamlSignOn = new EstablishmentSamlSignOn(),
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = establishment,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEstablishment.FailedBecauseEstablishmentHasSamlSignOn,
                        scenarioOptions.Establishment.RevisionId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithNullUser_AndIntentIsResetPassword()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var person = new Person
                {
                    DisplayName = "Adam West"
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = establishment,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePerson.FailedBecauseUserWasNull,
                        person.DisplayName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithSamlUser_AndIntentIsResetPassword()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "username",
                        EduPersonTargetedId = "something",
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = establishment,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        person.User.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithNoLocalMemberAccount_AndIntentIsResetPassword()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "username",
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = establishment,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateUser.FailedBecauseNameMatchedNoLocalMember,
                        person.User.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_EmailAddress_IsNotConfirmed_AndIntentIsResetPassword()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "username",
                    },
                    Emails = new[]
                    {
                        new EmailAddress
                        {
                            Value = command.EmailAddress
                        },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    LocalMemberExists = true,
                    Establishment = establishment,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        command.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesNonSamlMemberEstablishment_AndConfirmedEmail_WithPersonHavingLocalNonSamlUser()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "username",
                    },
                    Emails = new[]
                    {
                        new EmailAddress
                        {
                            Value = command.EmailAddress,
                            IsConfirmed = true,
                        },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    LocalMemberExists = true,
                    Establishment = establishment,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        private class ScenarioOptions
        {
            internal SendConfirmEmailMessageCommand Command { get; set; }
            internal Person Person { get; set; }
            internal Establishment Establishment { get; set; }
            internal bool LocalMemberExists { get; set; }
        }

        private static SendConfirmEmailMessageValidator CreateValidator(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();
            var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
            if (scenarioOptions.Command != null)
            {
                queryProcessor.Setup(m => m
                    .Execute(It.Is(PersonQueryBasedOn(scenarioOptions.Command))))
                    .Returns(scenarioOptions.Person);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(EstablishmentQueryBasedOn(scenarioOptions.Command))))
                    .Returns(scenarioOptions.Establishment);
            }
            var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
            if (scenarioOptions.Person != null &&
                scenarioOptions.Person.User != null &&
                !string.IsNullOrWhiteSpace(scenarioOptions.Person.User.Name))
                memberSigner.Setup(m => m
                    .IsSignedUp(scenarioOptions.Person.User.Name))
                    .Returns(scenarioOptions.LocalMemberExists);
            return new SendConfirmEmailMessageValidator(queryProcessor.Object, memberSigner.Object);
        }

        private static Expression<Func<GetPersonByEmailQuery, bool>> PersonQueryBasedOn(SendConfirmEmailMessageCommand command)
        {
            Expression<Func<GetPersonByEmailQuery, bool>> queryBasedOn = q => q.Email == command.EmailAddress;
            return queryBasedOn;
        }

        private static Expression<Func<GetEstablishmentByEmailQuery, bool>> EstablishmentQueryBasedOn(SendConfirmEmailMessageCommand command)
        {
            Expression<Func<GetEstablishmentByEmailQuery, bool>> queryBasedOn = q => q.Email == command.EmailAddress;
            return queryBasedOn;
        }
    }
}
