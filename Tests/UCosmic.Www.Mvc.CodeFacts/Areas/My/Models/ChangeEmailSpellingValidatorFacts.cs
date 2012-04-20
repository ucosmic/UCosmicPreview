using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class ChangeEmailSpellingValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_ChangeEmailSpellingForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<ChangeEmailSpellingForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<ChangeEmailSpellingValidator>();
            }
        }

        [TestClass]
        public class TheValueProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Value_IsNull()
            {
                var validator = new ChangeEmailSpellingValidator(null);
                var model = new ChangeEmailSpellingForm { Value = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_IsEmpty()
            {
                var validator = new ChangeEmailSpellingValidator(null);
                var model = new ChangeEmailSpellingForm { Value = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_IsWhiteSpace()
            {
                var validator = new ChangeEmailSpellingValidator(null);
                var model = new ChangeEmailSpellingForm { Value = " \r" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_IsMissingTldExtension()
            {
                var validator = new ChangeEmailSpellingValidator(null);
                var model = new ChangeEmailSpellingForm { Value = "email@domain" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_DoesNotMatchPreviousSpelling_CaseInsensitively()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetMyEmailAddressByNumberQuery>()))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new ChangeEmailSpellingValidator(queryProcessor.Object);
                var model = new ChangeEmailSpellingForm { Value = "user2@domain.tld", PersonUserName = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PreviousSpelling_CannotBeFound()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetMyEmailAddressByNumberQuery>()))
                    .Returns(null as EmailAddress);
                var validator = new ChangeEmailSpellingValidator(queryProcessor.Object);
                var model = new ChangeEmailSpellingForm { Value = "user@domain.tld", PersonUserName = string.Empty, };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_Value_MatchesPreviousSpelling_CaseInsensitively()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetMyEmailAddressByNumberQuery>()))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new ChangeEmailSpellingValidator(queryProcessor.Object);
                var model = new ChangeEmailSpellingForm { Value = "User@Domain.Tld", PersonUserName = string.Empty };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldBeNull();
            }
        }
    }
}
