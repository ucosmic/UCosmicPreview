using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Email;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignIn;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignUp;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class OldSignUpControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.OldSignUp.Name;

        [TestClass]
        public class SendEmail_Get
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WithNullEmailAddress_Always()
            {
                // arrange
                var controller = CreateController();

                // act 
                var result = controller.SendEmail();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var model = result.Model;
                model.ShouldNotBeNull();
                model.ShouldBeType<OldSendEmailForm>();
                var form = (OldSendEmailForm)model;
                form.EmailAddress.ShouldBeNull();
            }
        }

        [TestClass]
        public class SendEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenModelIsNull()
            {
                // arrange
                var controller = CreateController();

                // act 
                var result = controller.SendEmail(null);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenModelStateIsInvalid()
            {
                // arrange
                var model = new OldSendEmailForm { EmailAddress = "user@invalid1.edu", };
                var controller = CreateController();
                controller.ModelState.AddModelError("error", "error");

                // act 
                var result = controller.SendEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                var viewModel = viewResult.Model;
                viewModel.ShouldNotBeNull();
                viewModel.ShouldBeType<OldSendEmailForm>();
                var form = (OldSendEmailForm)viewModel;
                form.EmailAddress.ShouldEqual(model.EmailAddress);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenPersonDoesNotExist()
            {
                // arrange
                var model = new OldSendEmailForm { EmailAddress = "user@valid1.edu" };
                #region Establishment & EmailTemplate Aggregates

                var establishment = new Establishment
                {
                    RevisionId = 2,
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@valid1.edu" }
                    },
                    OfficialName = "Test Member Establishment 1",
                    IsMember = true,
                    Type = new EstablishmentType
                    {
                        EnglishName = "University System",
                        Category = new EstablishmentCategory
                        {
                            EnglishName = "Institution",
                            Code = EstablishmentCategoryCode.Inst,
                        }
                    },
                };

                const string emailTokens = "{EmailAddress}|{ConfirmationCode}|{StartUrl}|{ConfirmationUrl}";
                var emailTemplate = new EmailTemplate
                {
                    Name = EmailTemplateName.SignUpConfirmation,
                    SubjectFormat = emailTokens,
                    BodyFormat = emailTokens,
                };

                #endregion
                var commander = new Mock<ICommandObjects>(MockBehavior.Default);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                var emailSender = new Mock<ISendEmails>(MockBehavior.Default);
                var configurationManager = new Mock<IManageConfigurations>(MockBehavior.Default);
                var createPersonHandler = new Mock<IHandleCommands<CreatePersonCommand>>(MockBehavior.Strict);
                createPersonHandler.Setup(m => m.Handle(It.IsAny<CreatePersonCommand>()))
                    .Callback((CreatePersonCommand command) => command.CreatedPerson = new Person
                    {
                        DisplayName = model.EmailAddress,
                    });
                entityQueries.Setup(m => m.Establishments).Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable());
                entityQueries.Setup(m => m.EmailTemplates).Returns(new[] { emailTemplate }.AsQueryable);
                configurationManager.Setup(m => m.SignUpUrl).Returns("https://sub.domain.tld/sign-up");
                configurationManager.Setup(m => m.SignUpEmailConfirmationUrlFormat).Returns("https://sub.domain.tld/confirm-email/t-{0}/{1}");
                var controller = CreateController(entityQueries.Object, commander.Object,
                    emailSender.Object, configurationManager.Object, createPersonHandler.Object);

                // act 
                var result = controller.SendEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(5);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", Controller), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignUp.ActionNames.ConfirmEmail), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.Keys.ShouldContain("token");
                routeResult.RouteValues["token"].ShouldNotBeNull();
                Guid token;
                Guid.TryParse(routeResult.RouteValues["token"].ToString(), out token);
                token.ShouldNotEqual(Guid.Empty);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>("secretCode", null));
                routeResult.RouteValues["secretCode"].ShouldBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(BaseController.FeedbackMessageKey,
                    string.Format("A confirmation email has been sent to {0}", model.EmailAddress)));

                //commander.Verify(m => m.Insert(It.Is<Person>(e => e.DisplayName == model.EmailAddress), true), Times.Once());
                commander.Verify(m => m.Insert(It.IsAny<Person>(), true), Times.Once());
                emailSender.Verify(m => m.Send(It.IsAny<EmailMessage>()), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenAffiliationDoesNotExist()
            {
                // arrange
                var model = new OldSendEmailForm { EmailAddress = "user@valid2.edu" };
                #region Entity Aggregates

                var establishment = new Establishment
                {
                    RevisionId = 2,
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@valid2.edu" }
                    },
                    OfficialName = "Test Member Establishment 2",
                    IsMember = true,
                    Type = new EstablishmentType
                    {
                        EnglishName = "University System",
                        Category = new EstablishmentCategory
                        {
                            EnglishName = "Institution",
                            Code = EstablishmentCategoryCode.Inst,
                        }
                    },
                };

                var person = new Person
                {
                    RevisionId = 1,
                    DisplayName = model.EmailAddress,
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = model.EmailAddress
                        },
                    },
                };
                person.Emails.ToList().ForEach(e => e.Person = person);

                const string emailTokens = "{EmailAddress}|{ConfirmationCode}|{StartUrl}|{ConfirmationUrl}";
                var emailTemplate = new EmailTemplate
                {
                    Name = EmailTemplateName.SignUpConfirmation,
                    SubjectFormat = emailTokens,
                    BodyFormat = emailTokens,
                };

                #endregion
                var commander = new Mock<ICommandObjects>(MockBehavior.Default);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                var emailSender = new Mock<ISendEmails>(MockBehavior.Default);
                var configurationManager = new Mock<IManageConfigurations>(MockBehavior.Default);
                entityQueries.Setup(m => m.Establishments).Returns(new[] { establishment }.AsQueryable);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                entityQueries.Setup(m => m.EmailTemplates).Returns(new[] { emailTemplate }.AsQueryable);
                configurationManager.Setup(m => m.SignUpUrl).Returns("https://sub.domain.tld/sign-up");
                configurationManager.Setup(m => m.SignUpEmailConfirmationUrlFormat).Returns("https://sub.domain.tld/confirm-email/t-{0}/{1}");
                var controller = CreateController(entityQueries.Object, commander.Object,
                    emailSender.Object, configurationManager.Object);

                // act 
                var result = controller.SendEmail(model);
                var confirmation = person.Emails.Single().Confirmations.Single();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(5);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", Controller), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignUp.ActionNames.ConfirmEmail), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>("token", confirmation.Token));
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>("secretCode", null));
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(BaseController.FeedbackMessageKey,
                    string.Format("A confirmation email has been sent to {0}", model.EmailAddress)));

                commander.Verify(m => m.Update(It.Is<Person>(e => e == person), true), Times.Once());
                emailSender.Verify(m => m.Send(It.Is<EmailMessage>(e =>
                    e == person.Messages.Single())),
                        Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenAffiliationExists()
            {
                // arrange
                var model = new OldSendEmailForm { EmailAddress = "user@valid3.edu" };
                #region Entity Aggregates

                var establishment = new Establishment
                {
                    RevisionId = 2,
                    EmailDomains = new Collection<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain{Value="@valid3.edu"}
                    },
                    OfficialName = "Test Member Establishment 3",
                    IsMember = true,
                    Type = new EstablishmentType
                    {
                        EnglishName = "University System",
                        Category = new EstablishmentCategory
                        {
                            EnglishName = "Institution",
                            Code = EstablishmentCategoryCode.Inst,
                        }
                    },
                };

                var person = new Person
                {
                    RevisionId = 1,
                    DisplayName = model.EmailAddress,
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = model.EmailAddress
                        },
                    },
                    Affiliations = new List<Affiliation>
                    {
                        new Affiliation
                        {
                            Establishment = establishment
                        },
                    },
                };
                person.Emails.ToList().ForEach(e => e.Person = person);

                const string emailTokens = "{EmailAddress}|{ConfirmationCode}|{StartUrl}|{ConfirmationUrl}";
                var emailTemplate = new EmailTemplate
                {
                    Name = EmailTemplateName.SignUpConfirmation,
                    SubjectFormat = emailTokens,
                    BodyFormat = emailTokens,
                };

                #endregion
                var commander = new Mock<ICommandObjects>(MockBehavior.Default);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                var emailSender = new Mock<ISendEmails>(MockBehavior.Default);
                var configurationManager = new Mock<IManageConfigurations>(MockBehavior.Default);
                entityQueries.Setup(m => m.Establishments).Returns(new[] { establishment }.AsQueryable);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                entityQueries.Setup(m => m.EmailTemplates).Returns(new[] { emailTemplate }.AsQueryable);
                configurationManager.Setup(m => m.SignUpUrl).Returns("https://sub.domain.tld/sign-up");
                configurationManager.Setup(m => m.SignUpEmailConfirmationUrlFormat).Returns("https://sub.domain.tld/confirm-email/t-{0}/{1}");
                var controller = CreateController(entityQueries.Object, commander.Object,
                    emailSender.Object, configurationManager.Object);

                // act 
                var result = controller.SendEmail(model);
                var confirmation = person.Emails.Single().Confirmations.Single();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(5);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", Controller), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignUp.ActionNames.ConfirmEmail), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>("token", confirmation.Token));
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>("secretCode", null));
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(BaseController.FeedbackMessageKey,
                    string.Format("A confirmation email has been sent to {0}", model.EmailAddress)));

                commander.Verify(m => m.Update(It.Is<Person>(e => e == person), true), Times.Once());
                emailSender.Verify(m => m.Send(It.Is<EmailMessage>(e => e == person.Messages.Single())), Times.Once());
            }
        }

        [TestClass]
        public class ValidateSendEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsErrorMessage_WhenModelIsInvalid()
            {
                // arrange
                var model = new OldSendEmailForm
                {
                    EmailAddress = null,
                };
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                DependencyInjector.Set(di.Object);
                var controller = CreateController();

                // act 
                var result = controller.ValidateSendEmail(model.EmailAddress);

                // assert
                result.ShouldNotBeNull();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(OldSendEmailForm.RequiredErrorMessage);

                entityQueries.Verify(m => m.Establishments, Times.Never());
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsTrue_WhenModelIsValid()
            {
                // arrange
                var model = new OldSendEmailForm
                {
                    EmailAddress = "user@valid6.edu",
                };
                var establishment = new Establishment
                {
                    EmailDomains = new List<EstablishmentEmailDomain>
                    {
                        new EstablishmentEmailDomain { Value = "@valid6.edu" }
                    },
                    OfficialName = "Test Establishment 6",
                    IsMember = true,
                };
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                di.Setup(m => m.GetService(typeof(ISignMembers))).Returns(memberSigner.Object);
                entityQueries.Setup(m => m.Establishments)
                    .Returns(new[] { establishment }.AsQueryable);
                entityQueries.Setup(m => m.People)
                    .Returns(new Person[] { }.AsQueryable);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)))
                    .Returns(false);
                DependencyInjector.Set(di.Object);
                var controller = CreateController();

                // act 
                var result = controller.ValidateSendEmail(model.EmailAddress);

                // assert
                result.ShouldNotBeNull();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(true);

                entityQueries.Verify(m => m.Establishments, Times.Once());
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == model.EmailAddress)),
                    Times.Once());
            }

        }

        [TestClass]
        public class ConfirmEmail_Get
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenTokenIsEmpty()
            {
                // arrange
                var model = new OldConfirmEmailForm
                {
                    Token = Guid.Empty,
                    SecretCode = null,
                };
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenPersonIsNull()
            {
                // arrange
                var model = new OldConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = null,
                };
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People)
                        .Returns(new Person[] { }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationIsNull()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation();
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People)
                    .Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenConfirmationIsValid()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.First();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(false);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmEmailForm>();
                var form = (OldConfirmEmailForm)viewResult.Model;
                form.Token.ShouldNotEqual(Guid.Empty);
                form.SecretCode.ShouldBeNull();
                form.IsUrlConfirmation.ShouldBeFalse();

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenConfirmationIsValid_AndSecretCodeIsPassed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = "its a secret",
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
                            tokenConfirmation,
                            new EmailConfirmation(),
                        }
                    },
                }
                };
                var emailAddress = person.Emails.First();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(false);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmEmailForm>();
                var form = (OldConfirmEmailForm)viewResult.Model;
                form.Token.ShouldNotEqual(Guid.Empty);
                form.SecretCode.ShouldNotBeNull();
                form.SecretCode.ShouldEqual(model.SecretCode);
                form.IsUrlConfirmation.ShouldBeTrue();

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                            tokenConfirmation,
                            new EmailConfirmation(),
                        }
                    },
                }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndPersonHasRegisteredUser()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                            tokenConfirmation,
                            new EmailConfirmation(),
                        }
                    },
                }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndConfirmationIsRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                    tokenConfirmation,
                                    new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp_AndConfirmationIsRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(false);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsRedeemed);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                            Value = "pending@valid.edu",
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(true);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.MemberIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

        }

        [TestClass]
        public class ConfirmEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenModelIsNull()
            {
                // arrange
                var controller = CreateController();

                // act
                var result = controller.ConfirmEmail(null);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenTokenIsEmptyGuid()
            {
                // arrange
                var controller = CreateController();

                // act
                var result = controller.ConfirmEmail(new OldConfirmEmailForm { Token = Guid.Empty });

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenPersonIsNull()
            {
                // arrange
                var model = new OldConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                };
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationIsNull()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = "its a secret",
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
                                tokenConfirmation,
                            }
                        }
                    }
                };

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenModelStateIsInvalid_AndIsNotUrlConfirmation()
            {
                // arrange
                var model = new OldConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                    IsUrlConfirmation = false,
                };
                var controller = CreateController();
                controller.ModelState.AddModelError("error", "error");

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmEmailForm>();
                var form = (OldConfirmEmailForm)viewResult.Model;
                form.SecretCode.ShouldBeNull();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenModelStateIsInvalid_AndIsUrlConfirmation()
            {
                // arrange
                var model = new OldConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                    IsUrlConfirmation = true,
                };
                var controller = CreateController();
                controller.ModelState.AddModelError("error", "error");

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmEmailForm>();
                var form = (OldConfirmEmailForm)viewResult.Model;
                form.SecretCode.ShouldEqual(model.SecretCode);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndPersonHasRegisteredUser()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndConfirmationIsRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp_AndConfirmationIsRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(false);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.ConfirmationIsRedeemed);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                var model = new OldConfirmEmailForm
                {
                    Token = tokenConfirmation.Token,
                    SecretCode = null,
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
                            Value = "pending@valid.edu",
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                                new EmailConfirmation(),
                            }
                        },
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.First();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(true);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldConfirmDeniedPage>();
                var page = (OldConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldConfirmDeniedPage.DeniedBecause.MemberIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenConfirmationIsValid()
            {
                // arrange
                const string secretCode = "valid secret";
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    SecretCode = secretCode,
                    RedeemedOnUtc = null,
                };
                var model = new OldConfirmEmailForm
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
                            IsConfirmed = false,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            }
                        }
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var commander = new Mock<ICommandObjects>(MockBehavior.Default);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(false);
                var controller = CreateController(entityQueries.Object, commander.Object, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(3);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", Controller), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignUp.ActionNames.CreatePassword), new CaseInsensitiveKeyComparer());

                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, confirmation.Token));
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(BaseController.FeedbackMessageKey, "Your email address was successfully confirmed"));

                entityQueries.Verify(m => m.People, Times.Once());
                commander.Verify(m => m.Update(It.Is<Person>(p => p == person), true), Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
                emailAddress.IsConfirmed.ShouldBeTrue();
                confirmation.RedeemedOnUtc.ShouldNotEqual(null);
            }
        }

        [TestClass]
        public class ValidateConfirmEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsErrorMessage_WhenModelIsInvalid()
            {
                // arrange
                var model = new OldConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = null,
                };
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People)
                        .Returns(new Person[] { }.AsQueryable);
                DependencyInjector.Set(di.Object);
                var controller = CreateController();

                // act 
                var result = controller.ValidateConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(OldConfirmEmailForm.RequiredErrorMessage);
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsTrue_WhenModelIsValid()
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
                var model = new OldConfirmEmailForm
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
                                tokenConfirmation,
                            }
                        }
                    }
                };

                #endregion
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People)
                        .Returns(new[] { person }.AsQueryable);
                DependencyInjector.Set(di.Object);
                var controller = CreateController();

                // act 
                var result = controller.ValidateConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(true);
                entityQueries.Verify(m => m.People, Times.Once());
            }

        }

        [TestClass]
        public class CreatePassword_Get
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenTempDataIsLost()
            {
                // arrange
                var controller = CreateController();

                // act
                var result = controller.CreatePassword();

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenIsEmptyGuid()
            {
                // arrange
                var token = Guid.Empty;
                var controller = CreateController();
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void KeepsConfirmationTokenInTempData_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var controller = CreateController(new Mock<IQueryEntities>(MockBehavior.Default).Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(
                    OldSignUpController.ConfirmationTokenKey, token));
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void LooksForConfirmation_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNull()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenEmailIsNotConfirmed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = false,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNotRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = null,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = false,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationIsExpired);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenPersonHasSignedUpUser()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User { IsRegistered = true },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.UserIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User { IsRegistered = false },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = "member@valid.edu",
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(true);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.MemberIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessView_WhenConfirmationIsValid()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User { IsRegistered = false },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = "confirmed@valid.edu",
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(false);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreatePasswordForm>();
                var form = (OldCreatePasswordForm)viewResult.Model;
                form.Password.ShouldBeNull();
                form.ConfirmPassword.ShouldBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
            }
        }

        [TestClass]
        public class CreatePassword_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenModelIsNull()
            {
                // arrange
                var controller = CreateController();

                // act
                var result = controller.CreatePassword(null);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenTempDataIsLost()
            {
                // arrange
                var model = new OldCreatePasswordForm();
                var controller = CreateController();

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenIsEmptyGuid()
            {
                // arrange
                var token = Guid.Empty;
                var model = new OldCreatePasswordForm();
                var controller = CreateController();
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenModelStateIsInvalid()
            {
                // arrange
                var token = Guid.NewGuid();
                var model = new OldCreatePasswordForm();
                var controller = CreateController();
                controller.ModelState.AddModelError("error", "error");
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreatePasswordForm>();
                var form = (OldCreatePasswordForm)viewResult.Model;
                form.Password.ShouldEqual(model.Password);
                form.ConfirmPassword.ShouldEqual(model.ConfirmPassword);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void KeepsConfirmationTokenInTempData_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var controller = CreateController(new Mock<IQueryEntities>(MockBehavior.Strict).Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;
                controller.ModelState.AddModelError("error", "error");

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(
                    OldSignUpController.ConfirmationTokenKey, token));
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void LooksForConfirmation_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNull()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenEmailIsNotConfirmed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = false,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNotRedeemed()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = null,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = false,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.ConfirmationIsExpired);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenPersonHasSignedUpUser()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User { IsRegistered = true },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.UserIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = new User { IsRegistered = false },
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = "member@valid.edu",
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)))
                    .Returns(true);
                var controller = CreateController(entityQueries.Object, memberSigner.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword(new OldCreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.OldSignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<OldCreateDeniedPage>();
                var page = (OldCreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(OldCreateDeniedPage.DeniedBecause.MemberIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(OldSignUpController.ConfirmationTokenKey, tokenConfirmation.Token));
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenPersonHasNullUser()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                    RedeemedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                };
                var model = new OldCreatePasswordForm
                {
                    Password = "password",
                    ConfirmPassword = "password",
                };
                #region Person Aggregate

                var person = new Person
                {
                    User = null,
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = "new@member.edu",
                            IsConfirmed = true,
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            },
                        },
                    },
                };
                var emailAddress = person.Emails.Single();
                emailAddress.Person = person;
                var confirmation = emailAddress.Confirmations.Single();
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var memberSigner = new Mock<ISignMembers>(MockBehavior.Strict);
                memberSigner.Setup(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value))).Returns(false);
                memberSigner.Setup(m => m.SignUp(emailAddress.Value, model.Password));
                var commander = new Mock<ICommandObjects>(MockBehavior.Default);
                var controller = CreateController(entityQueries.Object, commander.Object, memberSigner.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(3);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", Controller), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignUp.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(
                    BaseController.FeedbackMessageKey, "Your password was created successfully"));
                entityQueries.Verify(m => m.People, Times.Once());
                commander.Verify(m => m.Update(It.IsAny<Person>(), false), Times.Once());
                commander.Verify(m => m.SaveChanges(), Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(emailAddress.Value), Times.Once());
                memberSigner.Verify(m => m.SignUp(emailAddress.Value, model.Password), Times.Once());
                person.User.ShouldNotBeNull();
                person.User.Name.ShouldEqual(emailAddress.Value);
                person.User.IsRegistered.ShouldBeTrue();
            }
        }

        [TestClass]
        public class SignIn_Get
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void RedirectsToSignIn_WhenConfirmationTokenIsNotInTempData()
            {
                // arrange
                var controller = CreateController();

                // act
                var result = controller.SignIn();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(3);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", MVC.Identity.OldSignIn.Name), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignIn.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void RedirectsToSignIn_WhenConfirmationTokenIsEmptyGuid()
            {
                // arrange
                var token = Guid.Empty;
                var controller = CreateController();
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.SignIn();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(3);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", MVC.Identity.OldSignIn.Name), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignIn.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void RedirectsToSignIn_WhenConfirmationCannotBeFound()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = "user@valid.edu",
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation,
                            }
                        }
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.SignIn();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<RedirectToRouteResult>();
                var routeResult = (RedirectToRouteResult)result;
                routeResult.RouteValues.ShouldNotBeNull();
                routeResult.RouteValues.Count.ShouldEqual(3);
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "area", Area), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "controller", MVC.Identity.OldSignIn.Name), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.OldSignIn.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.Keys.ShouldNotContain(OldSignUpController.ConfirmationTokenKey);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessView_WhenConfirmationTokenIsValid()
            {
                // arrange
                var tokenConfirmation = new EmailConfirmation
                {
                    Intent = EmailConfirmationIntent.SignUp,
                };
                #region Person Aggregate

                var person = new Person
                {
                    Emails = new List<EmailAddress>
                    {
                        new EmailAddress
                        {
                            Value = "user@valid.edu",
                            Confirmations = new List<EmailConfirmation>
                            {
                                tokenConfirmation
                            }
                        }
                    }
                };
                var emailAddress = person.Emails.Single();
                var confirmation = emailAddress.Confirmations.Single();
                emailAddress.Person = person;
                confirmation.EmailAddress = emailAddress;

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(m => m.People).Returns(new[] { person }.AsQueryable);
                var controller = CreateController(entityQueries.Object);
                controller.TempData[OldSignUpController.ConfirmationTokenKey] = tokenConfirmation.Token;

                // act
                var result = controller.SignIn();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldBeType<OldSignInForm>();
                var form = (OldSignInForm)viewResult.Model;
                form.EmailAddress.ShouldNotBeNull();
                form.EmailAddress.ShouldEqual(emailAddress.Value);
                controller.TempData.Keys.ShouldContain(OldSignUpController.ConfirmationTokenKey);

                entityQueries.Verify(m => m.People, Times.Once());
            }
        }

        private static OldSignUpController CreateController()
        {
            return new OldSignUpController(null, null, null, null, null, null);
        }

        private static OldSignUpController CreateController(IQueryEntities entityQueries)
        {
            return new OldSignUpController(entityQueries, null, null, null, null, null);
        }

        private static OldSignUpController CreateController(IQueryEntities entityQueries, ISignMembers memberSigner)
        {
            return new OldSignUpController(entityQueries, null, null, null, memberSigner, null);
        }

        private static OldSignUpController CreateController(IQueryEntities entityQueries
            , ICommandObjects objectCommander
            , ISignMembers memberSigner
        )
        {
            return new OldSignUpController(entityQueries, objectCommander, null, null, memberSigner, null);
        }

        private static OldSignUpController CreateController(IQueryEntities entityQueries
            , ICommandObjects objectCommander
            , ISendEmails emailSender
            , IManageConfigurations configurationManager
        )
        {
            return new OldSignUpController(entityQueries, objectCommander, emailSender, configurationManager, null, null);
        }

        private static OldSignUpController CreateController(IQueryEntities entityQueries
            , ICommandObjects objectCommander
            , ISendEmails emailSender
            , IManageConfigurations configurationManager
            , IHandleCommands<CreatePersonCommand> createPersonHandler
        )
        {
            return new OldSignUpController(entityQueries, objectCommander, emailSender, configurationManager, null, createPersonHandler);
        }
    }
}
