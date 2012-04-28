using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
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
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var command = new SendConfirmEmailMessageCommand();
                var scenarioOptions = new ScenarioOptions();
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                        command.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithNullUser()
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePerson.FailedBecauseUserWasNull,
                        person.DisplayName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithSamlUser()
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
                        EduPersonTargetedId = "something",
                    },
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        person.User.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithNoLocalMemberAccount()
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateUser.FailedBecauseNameMatchedNoLocalMember,
                        person.User.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_EmailAddress_IsNotConfirmed()
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
                            Value = command.EmailAddress
                        },
                    },
                };
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    LocalMemberExists = true,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        command.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesConfirmedEmail_WithPerson_HavingLocalNonSamlUser()
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
                var scenarioOptions = new ScenarioOptions
                {
                    Command = command,
                    Person = person,
                    LocalMemberExists = true,
                };
                var validator = CreateValidator(scenarioOptions);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldBeNull();
            }
        }

        private class ScenarioOptions
        {
            internal SendConfirmEmailMessageCommand Command { get; set; }
            internal Person Person { get; set; }
            internal bool LocalMemberExists { get; set; }
        }

        private static SendConfirmEmailMessageValidator CreateValidator(ScenarioOptions scenarioOptions = null)
        {
            scenarioOptions = scenarioOptions ?? new ScenarioOptions();
            var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
            if (scenarioOptions.Command != null)
                queryProcessor.Setup(m => m
                    .Execute(It.Is(PersonQueryBasedOn(scenarioOptions.Command))))
                    .Returns(scenarioOptions.Person);
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
            Expression<Func<GetPersonByEmailQuery, bool>> confirmationQueryBasedOn = q => q.Email == command.EmailAddress;
            return confirmationQueryBasedOn;
        }
    }
}
