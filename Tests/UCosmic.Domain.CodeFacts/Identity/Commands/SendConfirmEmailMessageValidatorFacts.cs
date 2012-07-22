using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
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
                    Person = new Person(),
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
                    DisplayName = "Adam West",
                    Emails = new Collection<EmailAddress>
                    {
                        new EmailAddress { Value = command.EmailAddress, },
                    },
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = new Establishment
                    {
                        EmailDomains = new Collection<EstablishmentEmailDomain>(),
                    },
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
                    DisplayName = "Adam West",
                    Emails = new Collection<EmailAddress>
                    {
                        new EmailAddress { Value = command.EmailAddress, },
                    },
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    Establishment = new Establishment
                    {
                        EmailDomains = new Collection<EstablishmentEmailDomain>
                        {
                            new EstablishmentEmailDomain { Value = "@domain.tld" },
                        },
                    },
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
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                var person = new Person
                {
                    DisplayName = "Adam West",
                    Emails = new Collection<EmailAddress>
                    {
                        new EmailAddress { Value = command.EmailAddress, },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    SamlSignOn = new EstablishmentSamlSignOn(),
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@domain.tld", },
                    },
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
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                var person = new Person
                {
                    DisplayName = "Adam West",
                    Emails = new Collection<EmailAddress>
                    {
                        new EmailAddress { Value = command.EmailAddress, },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@domain.tld", },
                    },
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
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "username",
                        EduPersonTargetedId = "something",
                    },
                    Emails = new Collection<EmailAddress>
                    {
                        new EmailAddress { Value = command.EmailAddress, },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain{ Value = "@domain.tld" }
                    }
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
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "username",
                    },
                    Emails = new Collection<EmailAddress>
                    {
                        new EmailAddress { Value = command.EmailAddress, },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@domain.tld", },
                    },
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
                    Intent = EmailConfirmationIntent.ResetPassword,
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
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@domain.tld" },
                    }
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
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@domain.tld" },
                    },
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
            var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
            if (scenarioOptions.Command != null)
            {
                entities.Setup(m => m.Read<Person>()).Returns(new[] { scenarioOptions.Person }.AsQueryable);
                entities.Setup(m => m.Read<Establishment>()).Returns(new[] { scenarioOptions.Establishment }.AsQueryable);
            }
            var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
            if (scenarioOptions.Person != null &&
                scenarioOptions.Person.User != null &&
                !string.IsNullOrWhiteSpace(scenarioOptions.Person.User.Name))
                passwords.Setup(m => m
                    .Exists(scenarioOptions.Person.User.Name))
                    .Returns(scenarioOptions.LocalMemberExists);
            return new SendConfirmEmailMessageValidator(entities.Object, passwords.Object);
        }
    }
}
