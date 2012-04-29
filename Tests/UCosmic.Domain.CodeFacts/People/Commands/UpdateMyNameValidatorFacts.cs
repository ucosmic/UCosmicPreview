using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class UpdateMyNameValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheDisplayNameProperty
        {
            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsNull()
            {
                var command = new UpdateMyNameCommand { DisplayName = null };
                var validator = new UpdateMyNameValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePerson.FailedBecauseDisplayNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsEmptyString()
            {
                var command = new UpdateMyNameCommand { DisplayName = string.Empty };
                var validator = new UpdateMyNameValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePerson.FailedBecauseDisplayNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsWhiteSpace()
            {
                var command = new UpdateMyNameCommand { DisplayName = "\t" };
                var validator = new UpdateMyNameValidator(null);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(ValidatePerson.FailedBecauseDisplayNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_DisplayName_IsNotEmpty()
            {
                var command = new UpdateMyNameCommand { DisplayName = "Adam West" };
                var validator = new UpdateMyNameValidator(null);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePrincipalProperty
        {
            [TestMethod]
            public void IsInvalidWhen_Principal_IsNull()
            {
                var command = new UpdateMyNameCommand
                {
                    Principal = null,
                };
                var validator = new UpdateMyNameValidator(null);

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
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                };
                var validator = new UpdateMyNameValidator(null);

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
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                };
                var validator = new UpdateMyNameValidator(null);

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
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                };
                var validator = new UpdateMyNameValidator(null);

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
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.Principal.Identity.Name)))
                    .Returns(null as User);
                var validator = new UpdateMyNameValidator(queryProcessor.Object);

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
                var command = new UpdateMyNameCommand
                {
                    Principal = principal,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.Principal.Identity.Name)))
                    .Returns(new User());
                var validator = new UpdateMyNameValidator(queryProcessor.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldBeNull();
            }
        }
    }
}
