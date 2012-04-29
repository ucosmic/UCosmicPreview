using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class UpdateEmailValueValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_UpdateEmailValueForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<UpdateEmailValueForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<UpdateEmailValueValidator>();
            }
        }

        [TestClass]
        public class TheValueProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Value_IsNull()
            {
                var validator = new UpdateEmailValueValidator(null);
                var model = new UpdateEmailValueForm { Value = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_IsEmpty()
            {
                var validator = new UpdateEmailValueValidator(null);
                var model = new UpdateEmailValueForm { Value = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_IsWhiteSpace()
            {
                var validator = new UpdateEmailValueValidator(null);
                var model = new UpdateEmailValueForm { Value = " \r" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_IsMissingTldExtension()
            {
                var validator = new UpdateEmailValueValidator(null);
                var model = new UpdateEmailValueForm { Value = "email@domain" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_DoesNotMatchPreviousSpelling_CaseInsensitively()
            {
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetMyEmailAddressByNumberQuery>()))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new UpdateEmailValueValidator(queryProcessor.Object);
                var model = new UpdateEmailValueForm { Value = "user2@domain.tld", PersonUserName = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Number_DoesNotMatchEmail_ForNullPersonUserName()
            {
                const string emailValue = "user1@domain.tld";
                var form = new UpdateEmailValueForm
                {
                    PersonUserName = null,
                    Number = 13,
                    Value = emailValue.ToUpper(),
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EmailQueryBasedOn(form))))
                    .Returns(null as EmailAddress);
                var validator = new UpdateEmailValueValidator(queryProcessor.Object);
                var results = validator.Validate(form);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        form.Number, form.PersonUserName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Number_DoesNotMatchEmail_ForNonNullPersonUserName()
            {
                const string personUserName = "user@domain.tld";
                const string emailValue = "user1@domain.tld";
                var form = new UpdateEmailValueForm
                {
                    PersonUserName = personUserName,
                    Number = 13,
                    Value = emailValue.ToUpper(),
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EmailQueryBasedOn(form))))
                    .Returns(null as EmailAddress);
                var validator = new UpdateEmailValueValidator(queryProcessor.Object);
                var results = validator.Validate(form);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        form.Number, form.PersonUserName));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_Number_MatchesEmail_ForPersonUserName()
            {
                const string personUserName = "user@domain.tld";
                const string emailValue = "user1@domain.tld";
                var form = new UpdateEmailValueForm
                {
                    PersonUserName = personUserName,
                    Number = 13,
                    Value = emailValue.ToUpper(),
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EmailQueryBasedOn(form))))
                    .Returns(new EmailAddress { Value = emailValue.ToLower() });
                var validator = new UpdateEmailValueValidator(queryProcessor.Object);
                var results = validator.Validate(form);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_Value_MatchesPreviousSpelling_CaseInsensitively()
            {
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetMyEmailAddressByNumberQuery>()))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new UpdateEmailValueValidator(queryProcessor.Object);
                var model = new UpdateEmailValueForm { Value = "User@Domain.Tld", PersonUserName = string.Empty };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Value");
                error.ShouldBeNull();
            }
        }

        private static Expression<Func<GetMyEmailAddressByNumberQuery, bool>> EmailQueryBasedOn(UpdateEmailValueForm form)
        {
            Expression<Func<GetMyEmailAddressByNumberQuery, bool>> queryBasedOn = q =>
                q.Principal == null &&
                q.Number == form.Number
            ;
            if (form.PersonUserName != null)
                queryBasedOn = q =>
                    q.Principal.Identity.Name == form.PersonUserName &&
                    q.Number == form.Number
                ;
            return queryBasedOn;
        }
    }
}
