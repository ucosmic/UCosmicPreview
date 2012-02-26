using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    // ReSharper disable UnusedMember.Global
    public class SendEmailFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class EmailAddress_Property
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenNull()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = null,
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(SendEmailForm.RequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenEmptyString()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = string.Empty,
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(SendEmailForm.RequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenWhiteSpaceString()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = " \t \r \n",
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(SendEmailForm.RequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenFailsRegex()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "fail regular expression",
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(SendEmailForm.RegexErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenTooLong()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                        "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567@invalid1.edu",
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(SendEmailForm.LengthErrorMessage, null, 200));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");
            }

        }

        [TestClass]
        public class Validate
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenEstablishmentIsNull()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "user@invalid1.edu",
                };
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.Establishments).Returns(new Establishment[] { }.AsQueryable);
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(SendEmailForm.IneligibleErrorMessage, "@invalid1.edu"));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenEstablishmentIsNotMember()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "user@invalid2.edu",
                };
                var establishment = new Establishment
                {
                    EmailDomains = new List<EstablishmentEmailDomain>(),
                    OfficialName = "Test Establishment 2",
                    IsMember = false,
                };
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.Establishments)
                    .Returns(new[] { establishment }.AsQueryable);
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(SendEmailForm.IneligibleErrorMessage, "@invalid2.edu"));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenPersonHasRegisteredUser()
            {
                // arrange
                var emailAddress = "user@invalid3.edu";
                var model = new SendEmailForm
                {
                    EmailAddress = emailAddress,
                };
                #region Establishment & Person Aggregates

                var establishment = new Establishment
                {
                    EmailDomains = new[]
                    {
                        new EstablishmentEmailDomain { Value = "@invalid3.edu" }
                    },
                    OfficialName = "Test Establishment 3",
                    IsMember = true,
                };
                var person = new Person
                {
                    DisplayName = "Test Person 3",
                    User = new User
                    {
                        UserName = model.EmailAddress,
                        IsRegistered = true,
                    },
                    Emails = new[]
                    {
                        new EmailAddress{Value = emailAddress}
                    }
                };

                #endregion
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>().Initialize();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.Establishments)
                        .Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(SendEmailForm.AlreadySignedUpErrorMessage, model.EmailAddress));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenPersonIsNull()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "user@valid4.edu",
                };
                var establishment = new Establishment
                {
                    EmailDomains = new List<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@valid4.edu" }
                    },
                    OfficialName = "Test Establishment 4",
                    IsMember = true,
                };
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>().Initialize();
                var memberSigner = new Mock<ISignMembers>();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                di.Setup(m => m.GetService(typeof(ISignMembers))).Returns(memberSigner.Object);
                entityQueries.Setup(m => m.Establishments)
                        .Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People)
                        .Returns(new Person[] { }.AsQueryable());
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)))
                    .Returns(false);
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenPersonHasNullUser()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "user@valid5.edu",
                };
                #region Establishment & Person Aggregates

                var establishment = new Establishment
                {
                    EmailDomains = new List<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@valid5.edu" }
                    },
                    OfficialName = "Test Establishment 5",
                    IsMember = true,
                };
                var person = new Person
                {
                    DisplayName = "Test Person 5",
                    User = null,
                };

                #endregion
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>().Initialize();
                var memberSigner = new Mock<ISignMembers>();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                di.Setup(m => m.GetService(typeof(ISignMembers))).Returns(memberSigner.Object);
                entityQueries.Setup(m => m.Establishments)
                        .Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)))
                    .Returns(false);
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenPersonHasUnregisteredUser()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "user@valid6.edu",
                };
                #region Establishment & Person Aggregates

                var establishment = new Establishment
                {
                    EmailDomains = new List<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@valid6.edu" }
                    },
                    OfficialName = "Test Establishment 6",
                    IsMember = true,
                };
                var person = new Person
                {
                    DisplayName = "Test Person 6",
                    User = new User
                    {
                        UserName = model.EmailAddress,
                        IsRegistered = false,
                    },
                };

                #endregion
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>().Initialize();
                var memberSigner = new Mock<ISignMembers>();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                di.Setup(m => m.GetService(typeof(ISignMembers))).Returns(memberSigner.Object);
                entityQueries.Setup(m => m.Establishments)
                        .Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)))
                    .Returns(false);
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenMemberIsSignedUp()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = "user@invalid4.edu",
                };
                #region Establishment & Person Aggregates

                var establishment = new Establishment
                {
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@invalid4.edu" }
                    },
                    OfficialName = "Test Establishment 4",
                    IsMember = true,
                };
                var person = new Person
                {
                    DisplayName = "Test Person 4",
                    User = new User
                    {
                        UserName = model.EmailAddress,
                        IsRegistered = false,
                    },
                };

                #endregion
                var di = new Mock<IInjectDependencies>();
                var entityQueries = new Mock<IQueryEntities>().Initialize();
                var memberSigner = new Mock<ISignMembers>();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                di.Setup(m => m.GetService(typeof(ISignMembers))).Returns(memberSigner.Object);
                entityQueries.Setup(m => m.Establishments)
                        .Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)))
                    .Returns(true);
                DependencyInjector.SetInjector(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(SendEmailForm.AlreadySignedUpErrorMessage, model.EmailAddress));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("EmailAddress");

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)),
                    Times.Once());
            }
        }
    }
}
