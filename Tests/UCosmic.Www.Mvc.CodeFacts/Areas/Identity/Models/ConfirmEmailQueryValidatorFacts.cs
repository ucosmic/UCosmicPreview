using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailQueryValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_ConfirmEmailQuery()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<ConfirmEmailQuery>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<ConfirmEmailQueryValidator>();
            }
        }

        [TestClass]
        public class TheTokenProperty
        {
            private const string PropertyName = "Token";

            [TestMethod]
            public void IsInvalidWhen_IsEmpty()
            {
                var validated = new ConfirmEmailQuery
                {
                    Token = Guid.Empty,
                };
                var validator = new ConfirmEmailQueryValidator(null);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(String.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        validated.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_MatchesNoEntity()
            {
                var validated = new ConfirmEmailQuery
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(String.Format(
                    ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        validated.Token));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesEntity()
            {
                var validated = new ConfirmEmailQuery
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(new EmailConfirmation());
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheIsExpiredProperty
        {
            private const string PropertyName = "IsExpired";

            [TestMethod]
            public void IsInvalidWhen_True()
            {
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(-5),
                };
                var validated = new ConfirmEmailQuery
                {
                    Token = confirmation.Token,
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(String.Format(
                    ValidateEmailConfirmation.FailedBecauseIsExpired,
                        confirmation.Token, confirmation.ExpiresOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_False()
            {
                var confirmation = new EmailConfirmation
                {
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(5),
                };
                var validated = new ConfirmEmailQuery
                {
                    Token = confirmation.Token,
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_ConfirmationWasNull()
            {
                var validated = new ConfirmEmailQuery
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheIsRedeemedProperty
        {
            private const string PropertyName = "IsRedeemed";

            [TestMethod]
            public void IsInvalidWhen_True()
            {
                var confirmation = new EmailConfirmation
                {
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                };
                var validated = new ConfirmEmailQuery
                {
                    Token = confirmation.Token,
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(String.Format(
                    ValidateEmailConfirmation.FailedBecauseIsRedeemed,
                        confirmation.Token, confirmation.RedeemedOnUtc));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_False()
            {
                var confirmation = new EmailConfirmation();
                var validated = new ConfirmEmailQuery
                {
                    Token = confirmation.Token,
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(confirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }

            [TestMethod]
            public void IsValidWhen_ConfirmationWasNull()
            {
                var validated = new ConfirmEmailQuery
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcesor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcesor.Setup(m => m
                    .Execute(It.Is(ConfirmationQueryBasedOn(validated))))
                    .Returns(null as EmailConfirmation);
                var validator = new ConfirmEmailQueryValidator(queryProcesor.Object);

                var results = validator.Validate(validated);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(ConfirmEmailQuery validated)
        {
            return q => q.Token == validated.Token;
        }
    }
}
