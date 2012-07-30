using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Impl;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class ForgotPasswordValidatorFacts
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_ForgotPasswordForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var validator = container.GetInstance<IValidator<ForgotPasswordForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<ForgotPasswordValidator>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            private const string PropertyName = "EmailAddress";
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ForgotPasswordForm();
                var validator = new ForgotPasswordValidator(null, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ForgotPasswordValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = String.Empty
                };
                var validator = new ForgotPasswordValidator(null, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ForgotPasswordValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "   ",
                };
                var validator = new ForgotPasswordValidator(null, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ForgotPasswordValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsNotEmailAddress()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user#domain.tld",
                };
                var validator = new ForgotPasswordValidator(null, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ForgotPasswordValidator.FailedBecauseEmailAddressWasNotValidEmailAddress);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoEstablishment()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseUserNameMatchedNoLocalMember,
                        validated.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNonMemberEstablishment()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var establishment = new Establishment();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseUserNameMatchedNoLocalMember,
                        validated.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesSamlEstablishment()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    SamlSignOn = new EstablishmentSamlSignOn(),
                    EmailDomains = new[] { new EstablishmentEmailDomain { Value = "@domain.tld" }, },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        validated.EmailAddress.GetEmailDomain()));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoPerson()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Query<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseUserNameMatchedNoLocalMember,
                        validated.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithNullUser()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var person = new Person();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Query<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseUserNameMatchedNoLocalMember,
                        validated.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithSamlUser()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    User = new User
                    {
                        EduPersonTargetedId = "something",
                    },
                    Emails = new[] { new EmailAddress { Value = validated.EmailAddress, }, },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new[] { new EstablishmentEmailDomain { Value = "@domain.tld" }, },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Query<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                        validated.EmailAddress.GetEmailDomain()));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesPerson_WithNoLocalUser()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "user@domain.sub.tld",
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(m => m
                    .Exists(It.Is(IsSignedUpBasedOn(person))))
                    .Returns(false);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Query<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ForgotPasswordValidator.FailedBecauseUserNameMatchedNoLocalMember,
                        validated.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesUnconfirmedEmailAddress()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "user@domain.sub.tld",
                    },
                    Emails = new[]
                    {
                        new EmailAddress
                        {
                            Value = validated.EmailAddress,
                        },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new[] { new EstablishmentEmailDomain { Value = "@domain.tld" }, },
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(m => m
                    .Exists(It.Is(IsSignedUpBasedOn(person))))
                    .Returns(true);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Query<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, passwords.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                        validated.EmailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesPerson_WithNonSamlLocalUser_AndConfirmedEmailAddress()
            {
                var validated = new ForgotPasswordForm
                {
                    EmailAddress = "user@domain.tld",
                };
                var person = new Person
                {
                    User = new User
                    {
                        Name = "user@domain.sub.tld",
                    },
                    Emails = new[]
                    {
                        new EmailAddress
                        {
                            Value = validated.EmailAddress,
                            IsConfirmed = true,
                        },
                    },
                };
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new[] { new EstablishmentEmailDomain { Value = "@domain.tld" }, },
                };
                var passwords = new Mock<IStorePasswords>(MockBehavior.Strict);
                passwords.Setup(m => m
                    .Exists(It.Is(IsSignedUpBasedOn(person))))
                    .Returns(true);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Query<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new ForgotPasswordValidator(entities.Object, passwords.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        private static Expression<Func<string, bool>> IsSignedUpBasedOn(Person person)
        {
            return s => s == person.User.Name;
        }
    }
}
