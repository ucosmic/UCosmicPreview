using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class UpdateMyAffiliationValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class ThePrincipalProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Principal_IsNull()
            {
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = null,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new UpdateMyAffiliationValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecausePrincipalWasNull);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsNull()
            {
                const string principalIdentityName = null;
                // ReSharper disable ExpressionIsAlwaysNull
                var principal = principalIdentityName.AsPrincipal();
                // ReSharper restore ExpressionIsAlwaysNull
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new UpdateMyAffiliationValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecausePrincipalWasNull);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsEmptyString()
            {
                var principalIdentityName = string.Empty;
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new UpdateMyAffiliationValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecauseIdentityNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsWhiteSpace()
            {
                const string principalIdentityName = "\t";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new UpdateMyAffiliationValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePrincipal.FailedBecauseIdentityNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_DoesNotMatchUser()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(UserQueryBasedOn(command))))
                    .Returns(null as User);
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new UpdateMyAffiliationValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        command.Principal.Identity.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_PrincipalIdentityName_MatchesUser()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var command = new UpdateMyAffiliationCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(UserQueryBasedOn(command))))
                    .Returns(new User());
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new UpdateMyAffiliationValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldBeNull();
            }
        }

        private static Expression<Func<GetEstablishmentByIdQuery, bool>> EstablishmentQueryBasedOn(UpdateMyAffiliationCommand command)
        {
            Expression<Func<GetEstablishmentByIdQuery, bool>> establishmentQueryBasedOn = q => q.Id == command.EstablishmentId;
            return establishmentQueryBasedOn;
        }

        private static Expression<Func<GetUserByNameQuery, bool>> UserQueryBasedOn(UpdateMyAffiliationCommand command)
        {
            Expression<Func<GetUserByNameQuery, bool>> userQueryBasedOn = q => q.Name == command.Principal.Identity.Name;
            return userQueryBasedOn;
        }
    }
}
