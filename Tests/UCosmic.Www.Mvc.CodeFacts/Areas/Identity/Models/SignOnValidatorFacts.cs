using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class SignOnValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_SignOnForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<SignOnForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<SignOnValidator>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validator = new SignOnValidator(null);
                var model = new SignOnForm { EmailAddress = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(
                    It.Is<GetEstablishmentByEmailQuery>(q => q.Email == emailAddress)))
                        .Returns(null as Establishment);
                var validator = new SignOnValidator(queryProcessor.Object);
                var model = new SignOnForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
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
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(
                    It.Is<GetEstablishmentByEmailQuery>(q => q.Email == emailAddress)))
                        .Returns(new Establishment { IsMember = false, });
                var validator = new SignOnValidator(queryProcessor.Object);
                var model = new SignOnForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    SignOnValidator.FailedBecauseEstablishmentIsNotEligible, emailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsValidEmailAddress_AndBelongsToMemberEstablishment()
            {
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEstablishmentByEmailQuery>()))
                    .Returns(new Establishment { IsMember = true, });
                var validator = new SignOnValidator(queryProcessor.Object);
                var model = new SignOnForm { EmailAddress = "email@domain.tld" };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldBeNull();
            }
        }
    }
}
