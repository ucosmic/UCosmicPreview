using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class SignOnValidatorFacts
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_SignOnForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var validator = container.GetInstance<IValidator<SignOnForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<SignOnValidator>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            private const string PropertyName = "EmailAddress";

            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validator = new SignOnValidator(null);
                var model = new SignOnForm { EmailAddress = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var validator = new SignOnValidator(null);
                var model = new SignOnForm { EmailAddress = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validator = new SignOnValidator(null);
                var model = new SignOnForm { EmailAddress = " \t " };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsNotValidEmailAddress()
            {
                var validator = new SignOnValidator(null);
                var model = new SignOnForm { EmailAddress = "email@domain" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnValidator.FailedBecauseEmailAddressIsNotValidEmailAddress);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_HasNoMatchingEstablishment()
            {
                const string emailAddress = "email@domain.tld";
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new SignOnValidator(entities.Object);
                var model = new SignOnForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    SignOnValidator.FailedBecauseEstablishmentIsNotEligible, emailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchingEstablishment_IsNotMember()
            {
                const string emailAddress = "email@domain.tld";
                var establishment = new Establishment
                {
                    IsMember = false,
                    EmailDomains = new[] { new EstablishmentEmailDomain { Value = "@domain.tld", } }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                var validator = new SignOnValidator(entities.Object);
                var model = new SignOnForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    SignOnValidator.FailedBecauseEstablishmentIsNotEligible, emailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsValidEmailAddress_AndBelongsToMemberEstablishment()
            {
                var establishment = new Establishment
                {
                    IsMember = true,
                    EmailDomains = new[] { new EstablishmentEmailDomain { Value = "@domain.tld", } }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                var validator = new SignOnValidator(entities.Object);
                var model = new SignOnForm { EmailAddress = "email@domain.tld" };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }
    }
}
