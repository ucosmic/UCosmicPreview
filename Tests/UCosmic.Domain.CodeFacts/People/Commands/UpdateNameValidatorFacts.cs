using System.Linq;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class UpdateNameValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheDisplayNameProperty
        {
            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsNull()
            {
                var command = new UpdateNameCommand { DisplayName = null };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsEmptyString()
            {
                var command = new UpdateNameCommand { DisplayName = string.Empty };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsWhiteSpace()
            {
                var command = new UpdateNameCommand { DisplayName = "\t" };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "DisplayName");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsValidWhen_DisplayName_IsNotEmpty()
            {
                var command = new UpdateNameCommand { DisplayName = "Adam West" };
                var validator = new UpdateNameValidator(null);
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
                var command = new UpdateNameCommand
                {
                    Principal = null,
                };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsNull()
            {
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(null as string);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(UpdateNameValidator.PrincipalIdentityNameIsEmptyMessage);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsEmptyString()
            {
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(string.Empty);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(UpdateNameValidator.PrincipalIdentityNameIsEmptyMessage);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PrincipalIdentityName_IsWhiteSpace()
            {
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns("\t");
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                };
                var validator = new UpdateNameValidator(null);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(UpdateNameValidator.PrincipalIdentityNameIsEmptyMessage);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_Principal_DoesNotMatchExistingUser()
            {
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.Principal.Identity.Name)))
                    .Returns(null as User);
                var validator = new UpdateNameValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    UpdateNameValidator.PrincipalIdentityNameDoesNotMatchExistingUserErrorFormat,
                        command.Principal.Identity.Name));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_Principal_MatchesExistingUser()
            {
                const string principalIdentityName = "user@domain.tld";
                var identity = new Mock<IIdentity>();
                var principal = new Mock<IPrincipal>();
                identity.Setup(p => p.Name).Returns(principalIdentityName);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                var command = new UpdateNameCommand
                {
                    Principal = principal.Object,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is<GetUserByNameQuery>(q => q.Name == command.Principal.Identity.Name)))
                    .Returns(new User());
                var validator = new UpdateNameValidator(queryProcessor.Object);
                var results = validator.Validate(command);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "Principal");
                error.ShouldBeNull();
            }
        }
    }
}
