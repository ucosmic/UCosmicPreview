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
    public class SignOnBeginValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_SignOnBeginForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<SignOnBeginForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<SignOnBeginValidator>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validator = new SignOnBeginValidator(null);
                var model = new SignOnBeginForm { EmailAddress = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnBeginValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var validator = new SignOnBeginValidator(null);
                var model = new SignOnBeginForm { EmailAddress = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnBeginValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validator = new SignOnBeginValidator(null);
                var model = new SignOnBeginForm { EmailAddress = " \t " };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnBeginValidator.FailedBecauseEmailAddressWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsNotValidEmailAddress()
            {
                var validator = new SignOnBeginValidator(null);
                var model = new SignOnBeginForm { EmailAddress = "email@domain" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    SignOnBeginValidator.FailedBecauseEmailAddressIsNotValidEmailAddress);
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
                var validator = new SignOnBeginValidator(queryProcessor.Object);
                var model = new SignOnBeginForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    SignOnBeginValidator.FailedBecauseEstablishmentIsNotEligible, emailAddress));
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
                var validator = new SignOnBeginValidator(queryProcessor.Object);
                var model = new SignOnBeginForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    SignOnBeginValidator.FailedBecauseEstablishmentIsNotEligible, emailAddress));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_IsValidEmailAddress_AndBelongsToMemberEstablishment()
            {
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEstablishmentByEmailQuery>()))
                    .Returns(new Establishment { IsMember = true, });
                var validator = new SignOnBeginValidator(queryProcessor.Object);
                var model = new SignOnBeginForm { EmailAddress = "email@domain.tld" };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EmailAddress");
                error.ShouldBeNull();
            }
        }
    }
}
