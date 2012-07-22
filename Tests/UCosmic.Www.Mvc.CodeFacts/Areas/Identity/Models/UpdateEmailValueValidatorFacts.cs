using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
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
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var validator = container.GetInstance<IValidator<UpdateEmailValueForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<UpdateEmailValueValidator>();
            }
        }

        [TestClass]
        public class TheValueProperty
        {
            private const string PropertyName = "Value";

            [TestMethod]
            public void IsInvalidWhen_Value_IsNull()
            {
                var validator = new UpdateEmailValueValidator(null);
                var model = new UpdateEmailValueForm { Value = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
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
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateEmailValueValidator.FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Value_DoesNotMatchPreviousSpelling_CaseInsensitively()
            {
                var model = new UpdateEmailValueForm
                {
                    Value = "user2@domain.tld",
                    PersonUserName = string.Empty
                };
                var emailAddress = new EmailAddress
                {
                    Value = "user@domain.tld",
                    Person = new Person { User = new User { Name = model.PersonUserName }, }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var validator = new UpdateEmailValueValidator(entities.Object);
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
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
                    PersonUserName = emailValue,
                    Number = 13,
                    Value = emailValue.ToUpper(),
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateEmailValueValidator(entities.Object);
                var results = validator.Validate(form);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
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
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var validator = new UpdateEmailValueValidator(entities.Object);
                var results = validator.Validate(form);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
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
                var emailAddress = new EmailAddress
                {
                    Value = emailValue.ToLower(),
                    Number = form.Number,
                    Person = new Person { User = new User { Name = form.PersonUserName, } }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var validator = new UpdateEmailValueValidator(entities.Object);
                var results = validator.Validate(form);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_Value_MatchesPreviousSpelling_CaseInsensitively()
            {
                var model = new UpdateEmailValueForm
                {
                    Value = "User@Domain.Tld",
                    PersonUserName = string.Empty
                };
                var emailAddress = new EmailAddress
                {
                    Value = "user@domain.tld",
                    Person = new Person { User = new User { Name = model.PersonUserName } },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var validator = new UpdateEmailValueValidator(entities.Object);
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }
    }
}
