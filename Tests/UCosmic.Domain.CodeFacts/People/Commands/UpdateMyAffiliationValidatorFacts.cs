using System.Linq;
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
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new UpdateMyAffiliationValidator(entities.Object);
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
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new UpdateMyAffiliationValidator(entities.Object);
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
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new UpdateMyAffiliationValidator(entities.Object);
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
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new UpdateMyAffiliationValidator(entities.Object);
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
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<User>()).Returns(new User[] { }.AsQueryable);
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new UpdateMyAffiliationValidator(entities.Object);
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
                var user = new User { Name = principal.Identity.Name };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<User>()).Returns(new[] { user }.AsQueryable);
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                var validator = new UpdateMyAffiliationValidator(entities.Object);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldBeNull();
            }
        }
    }
}
