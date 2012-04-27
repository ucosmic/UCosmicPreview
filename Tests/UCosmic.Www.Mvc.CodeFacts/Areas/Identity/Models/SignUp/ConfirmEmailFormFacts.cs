using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class SecretCode
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenNull()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.Empty,
                    SecretCode = null,
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.RequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenEmptyString()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.Empty,
                    SecretCode = string.Empty,
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.RequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenWhiteSpaceString()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.Empty,
                    SecretCode = "\r\n   \t",
                };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.RequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");
            }
        }

        [TestClass]
        public class ViewModel
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenPersonIsNull()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                };
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var queryEntities = new Mock<IQueryEntities>(MockBehavior.Default);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(queryEntities.Object);
                queryEntities.Setup(p => p.People)
                    .Returns(new Person[] { }.AsQueryable);
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.InvalidSecretCodeErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");

                queryEntities.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenConfirmationIsNull()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            //IsCurrent = false,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation
                            }
                        }
                    }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var people = new Mock<IQueryEntities>(MockBehavior.Default);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(people.Object);
                people.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable);
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.InvalidSecretCodeErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");

                people.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenTokenDoesNotMatch()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        Confirmations = new List<EmailConfirmation>
                        {
                            tokenConfirmation
                        }
                    }
                }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var people = new Mock<IQueryEntities>(MockBehavior.Default);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(people.Object);
                people.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.InvalidSecretCodeErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");

                people.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenIntentDoesNotMatch()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    Intent = "Some other intent",
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        Confirmations = new List<EmailConfirmation>
                        {
                            tokenConfirmation
                        }
                    }
                }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var people = new Mock<IQueryEntities>(MockBehavior.Default);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(people.Object);
                people.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.InvalidSecretCodeErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");

                people.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenSecretCodeDoesNotMatch()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode.ToUpper(),
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        Confirmations = new List<EmailConfirmation>
                        {
                            tokenConfirmation
                        }
                    }
                }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var people = new Mock<IQueryEntities>(MockBehavior.Default);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(people.Object);
                people.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(ConfirmEmailForm.InvalidSecretCodeErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("SecretCode");

                people.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenSecretCodeIsMatch()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User
                    {
                        IsRegistered = false,
                    },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation
                            }
                        }
                    }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenSecretCodeIsMatch_AndConfirmationIsExpired()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation
                            }
                        }
                    }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenSecretCodeIsMatch_AndUserIsSignedUp()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = null,
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User
                    {
                        IsRegistered = true,
                    },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation
                            }
                        }
                    }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsValid_WhenSecretCodeIsMatch_AndConfirmationIsRedeemed()
            {
                // arrange
                const string secretCode = "its a secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                };
                var model = new ConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = secretCode,
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User
                    {
                        IsRegistered = false,
                    },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation
                            }
                        }
                    }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable());
                DependencyInjector.Set(di.Object);
                var results = new List<ValidationResult>();

                // act 
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);

                // assert
                isValid.ShouldBeTrue();
                results.Count.ShouldEqual(0);
                entityQueries.Verify(m => m.People, Times.Once());
            }
        }

    }
}
