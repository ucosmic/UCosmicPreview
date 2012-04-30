using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    // ReSharper disable UnusedMember.Global
    public class ResetPasswordValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_ResetPasswordForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<ResetPasswordForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<ResetPasswordValidator>();
            }
        }

        [TestClass]
        public class TheTokenProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var validated = new ResetPasswordForm
                {
                    Token = Guid.Empty,
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        validated.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoEntity()
            {
                var validated = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ResetPasswordValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        validated.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesEntity()
            {
                var validated = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation());
                var validator = new ResetPasswordValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Token");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePasswordProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ResetPasswordForm();
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ResetPasswordForm
                {
                    Password = string.Empty
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "   ",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_LengthIs_LessThanSixCharacters()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "12345",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ResetPasswordValidator.FailedBecausePasswordWasTooShort,
                        ValidatePassword.MinimumLength));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_LengthIs_MoreThanFiveCharacters()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "123456",
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation());
                var validator = new ResetPasswordValidator(queryProcessor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Password");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePasswordConfirmationProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsNull()
            {
                var validated = new ResetPasswordForm();
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsEmptyString()
            {
                var validated = new ResetPasswordForm
                {
                    PasswordConfirmation = string.Empty
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_IsWhiteSpace()
            {
                var validated = new ResetPasswordForm
                {
                    PasswordConfirmation = "   ",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_NotEqualToPassword()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "123456",
                    PasswordConfirmation = "123457",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    ResetPasswordValidator.FailedBecausePasswordConfirmationDidNotEqualPassword);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_NotEqualToPassword_AndPasswordIsNull()
            {
                var validated = new ResetPasswordForm
                {
                    PasswordConfirmation = "123",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_NotEqualToPassword_AndPasswordIsEmptyString()
            {
                var validated = new ResetPasswordForm
                {
                    Password = string.Empty,
                    PasswordConfirmation = "123",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_NotEqualToPassword_AndPasswordIsWhiteSpace()
            {
                var validated = new ResetPasswordForm
                {
                    Password = " \t ",
                    PasswordConfirmation = "123",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_EqualsPassword()
            {
                var validated = new ResetPasswordForm
                {
                    Password = "123456",
                    PasswordConfirmation = "123456",
                };
                var validator = new ResetPasswordValidator(null);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PasswordConfirmation");
                error.ShouldBeNull();
            }
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(ResetPasswordForm validated)
        {
            return q => q.Token == validated.Token;
        }
    }
}
