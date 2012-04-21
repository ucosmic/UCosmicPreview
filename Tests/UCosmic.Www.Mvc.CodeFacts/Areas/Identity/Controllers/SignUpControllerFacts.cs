using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Email;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignIn;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignUp;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class SignUpControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignUp.Name;

        [TestClass]
        public class SendEmail_Get
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void HasOneRoute_SignUp()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                    controller => controller.SendEmail();
                var url = SignUpRouteMapper.SendEmail.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WithNullEmailAddress_Always()
            {
                // arrange
                var controller = new SignUpController(null, null, null, null, null);

                // act 
                var result = controller.SendEmail();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var model = result.Model;
                model.ShouldNotBeNull();
                model.ShouldBeType<SendEmailForm>();
                var form = (SendEmailForm)model;
                form.EmailAddress.ShouldBeNull();
            }
        }

        [TestClass]
        public class SendEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void HasOneRoute_SignUp()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                    controller => controller.SendEmail(null);
                var url = SignUpRouteMapper.SendEmail.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenModelIsNull()
            {
                // arrange
                var controller = new SignUpController(null, null, null, null, null);

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
                var model = new SendEmailForm { EmailAddress = "user@invalid1.edu", };
                var controller = new SignUpController(null, null, null, null, null);
                controller.ModelState.AddModelError("error", "error");

                // act 
                var result = controller.SendEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                var viewModel = viewResult.Model;
                viewModel.ShouldNotBeNull();
                viewModel.ShouldBeType<SendEmailForm>();
                var form = (SendEmailForm)viewModel;
                form.EmailAddress.ShouldEqual(model.EmailAddress);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenPersonDoesNotExist()
            {
                // arrange
                var model = new SendEmailForm { EmailAddress = "user@valid1.edu" };
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
                entityQueries.Setup(m => m.Establishments).Returns(new[] { establishment }.AsQueryable());
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable());
                entityQueries.Setup(m => m.EmailTemplates).Returns(new[] { emailTemplate }.AsQueryable);
                configurationManager.Setup(m => m.SignUpUrl).Returns("https://sub.domain.tld/sign-up");
                configurationManager.Setup(m => m.SignUpEmailConfirmationUrlFormat).Returns("https://sub.domain.tld/confirm-email/t-{0}/{1}");
                var controller = new SignUpController(entityQueries.Object, commander.Object,
                    emailSender.Object, configurationManager.Object, null);

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
                    "action", MVC.Identity.SignUp.ActionNames.ConfirmEmail), new CaseInsensitiveKeyComparer());
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
                var model = new SendEmailForm { EmailAddress = "user@valid2.edu" };
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
                var controller = new SignUpController(entityQueries.Object, commander.Object,
                    emailSender.Object, configurationManager.Object, null);

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
                    "action", MVC.Identity.SignUp.ActionNames.ConfirmEmail), new CaseInsensitiveKeyComparer());
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
                var model = new SendEmailForm { EmailAddress = "user@valid3.edu" };
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
                var controller = new SignUpController(entityQueries.Object, commander.Object,
                    emailSender.Object, configurationManager.Object, null);

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
                    "action", MVC.Identity.SignUp.ActionNames.ConfirmEmail), new CaseInsensitiveKeyComparer());
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
            public void HasOneRoute_SignUp_Validate_emailAddress()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                    controller => controller.ValidateSendEmail(null);
                var url = SignUpRouteMapper.ValidateSendEmail.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsErrorMessage_WhenModelIsInvalid()
            {
                // arrange
                var model = new SendEmailForm
                {
                    EmailAddress = null,
                };
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                DependencyInjector.Set(di.Object);
                var controller = new SignUpController(null, null, null, null, null);

                // act 
                var result = controller.ValidateSendEmail(model.EmailAddress);

                // assert
                result.ShouldNotBeNull();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(SendEmailForm.RequiredErrorMessage);

                entityQueries.Verify(m => m.Establishments, Times.Never());
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsTrue_WhenModelIsValid()
            {
                // arrange
                var model = new SendEmailForm
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
                var controller = new SignUpController(null, null, null, null, null);

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
            public void HasOneRoute_SignUp_ConfirmEmail_token_secretCode()
            {
                var token = Guid.NewGuid();
                const string secretCode = "its a secret";
                var routeUrl = SignUpRouteMapper.ConfirmEmail.RouteForGet.ToAppRelativeUrl();
                var urlFormat = routeUrl.Replace("token", "0").Replace("secretCode", "1");

                // ~/sign-up/validate/[Guid]
                Expression<Func<SignUpController, ActionResult>> actionWithoutSecretCode =
                    controller => controller.ConfirmEmail(token, string.Empty);
                var urlWithoutSecretCode = string.Format(urlFormat, token, string.Empty);
                urlWithoutSecretCode = urlWithoutSecretCode.WithoutTrailingSlash();
                OutBoundRoute.Of(actionWithoutSecretCode).InArea(Area).AppRelativeUrl().ShouldEqual(urlWithoutSecretCode);
                urlWithoutSecretCode.WithMethod(HttpVerbs.Get).ShouldMapTo(actionWithoutSecretCode);
                urlWithoutSecretCode.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
                actionWithoutSecretCode.DefaultAreaRoutes(Area).ShouldMapToNothing();

                // ~/sign-up/validate/[Guid]/secret
                Expression<Func<SignUpController, ActionResult>> actionWithSecretCode =
                    controller => controller.ConfirmEmail(token, secretCode);
                var urlWithSecretCode = string.Format(urlFormat, token, secretCode.UrlPathEncoded());
                OutBoundRoute.Of(actionWithSecretCode).InArea(Area).AppRelativeUrl().ShouldEqual(urlWithSecretCode);
                urlWithSecretCode = urlWithSecretCode.UrlDecoded();
                urlWithSecretCode.WithMethod(HttpVerbs.Get).ShouldMapTo(actionWithSecretCode);
                urlWithSecretCode.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
                actionWithSecretCode.DefaultAreaRoutes(Area).ShouldMapToNothing();

                // ~/sign-up/validate/Guid.Empty
                Expression<Func<SignUpController, ActionResult>> actionWithEmptyToken =
                    controller => controller.ConfirmEmail(Guid.Empty, string.Empty);
                var urlWithEmptyToken = string.Format(urlFormat, Guid.Empty, string.Empty);
                urlWithEmptyToken = urlWithEmptyToken.WithoutTrailingSlash();
                OutBoundRoute.Of(actionWithEmptyToken).InArea(Area).AppRelativeUrl().ShouldNotEqual(urlWithEmptyToken);
                urlWithEmptyToken.WithAnyMethod().ShouldMapToNothing();
                actionWithEmptyToken.DefaultAreaRoutes(Area).ShouldMapToNothing();

                // ~/sign-up/validate/not a Guid
                var urlWithInvalidToken = string.Format(urlFormat, "not a Guid", string.Empty);
                urlWithInvalidToken = urlWithInvalidToken.WithoutTrailingSlash();
                urlWithInvalidToken.WithAnyMethod().ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenTokenIsEmpty()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.Empty,
                    SecretCode = null,
                };
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = null,
                };
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People)
                        .Returns(new Person[] {}.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = null,
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
                            new EmailConfirmation { Token = model.Token, },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
                        }
                    },
                }
                };

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People)
                    .Returns(new[] { person }.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmEmailForm>();
                var form = (ConfirmEmailForm)viewResult.Model;
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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmEmailForm>();
                var form = (ConfirmEmailForm)viewResult.Model;
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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndPersonHasRegisteredUser()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndConfirmationIsRedeemed()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp_AndConfirmationIsRedeemed()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsRedeemed()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsRedeemed);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.MemberIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

        }

        [TestClass]
        public class ConfirmEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void HasOneRoute_SignUp_ConfirmEmail()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                    controller => controller.ConfirmEmail(null);
                var url = SignUpRouteMapper.ConfirmEmail.RouteForPost.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethod(HttpVerbs.Post).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenModelIsNull()
            {
                // arrange
                var controller = new SignUpController(null, null, null, null, null);

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
                var controller = new SignUpController(null, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(new ConfirmEmailForm { Token = Guid.Empty });

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenPersonIsNull()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                };
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] {}.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token,
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                            }
                        }
                    }
                }
                };

                #endregion
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new [] { person }.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

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
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                    IsUrlConfirmation = false,
                };
                var controller = new SignUpController(null, null, null, null, null);
                controller.ModelState.AddModelError("error", "error");

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmEmailForm>();
                var form = (ConfirmEmailForm)viewResult.Model;
                form.SecretCode.ShouldBeNull();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenModelStateIsInvalid_AndIsUrlConfirmation()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "its a secret",
                    IsUrlConfirmation = true,
                };
                var controller = new SignUpController(null, null, null, null, null);
                controller.ModelState.AddModelError("error", "error");

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmEmailForm>();
                var form = (ConfirmEmailForm)viewResult.Model;
                form.SecretCode.ShouldEqual(model.SecretCode);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndPersonHasRegisteredUser()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 1)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired_AndConfirmationIsRedeemed()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenUserIsSignedUp_AndConfirmationIsRedeemed()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.UserIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsRedeemed()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.ConfirmationIsRedeemed);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token, 
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                            },
                            new EmailConfirmation { Token = Guid.NewGuid(), },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);

                // act
                var result = controller.ConfirmEmail(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.confirm_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<ConfirmDeniedPage>();
                var page = (ConfirmDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(ConfirmDeniedPage.DeniedBecause.MemberIsSignedUp);

                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenConfirmationIsValid()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = "valid secret",
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
                            new EmailConfirmation
                            {
                                Token = model.Token,
                                Intent = EmailConfirmationIntent.SignUp,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                SecretCode = model.SecretCode,
                                ConfirmedOnUtc = null,
                            }
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
                var controller = new SignUpController(entityQueries.Object, commander.Object, null, null, memberSigner.Object);

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
                    "action", MVC.Identity.SignUp.ActionNames.CreatePassword), new CaseInsensitiveKeyComparer());

                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, confirmation.Token));
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(BaseController.FeedbackMessageKey, "Your email address was successfully confirmed"));

                entityQueries.Verify(m => m.People, Times.Once());
                commander.Verify(m => m.Update(It.Is<Person>(p => p == person), true), Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
                emailAddress.IsConfirmed.ShouldBeTrue();
                confirmation.ConfirmedOnUtc.ShouldNotEqual(null);
            }
        }

        [TestClass]
        public class ValidateConfirmEmail_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void HasOneRoute_SignUp_ConfirmEmail_Validate_token_secretCode()
            {
                var token = Guid.NewGuid();
                const string secretCode = "its a secret";
                var routeUrl = SignUpRouteMapper.ValidateConfirmEmail.Route.ToAppRelativeUrl();
                var urlFormat = routeUrl.Replace("token", "0").Replace("secretCode", "1").ToAppRelativeUrl();

                // ~/sign-up/confirm-email/validate/[Guid]
                Expression<Func<SignUpController, ActionResult>> actionWithoutSecretCode =
                    controller => controller.ValidateConfirmEmail(token, string.Empty);
                var urlWithoutSecretCode = string.Format(urlFormat, token, string.Empty);
                urlWithoutSecretCode = urlWithoutSecretCode.WithoutTrailingSlash();
                OutBoundRoute.Of(actionWithoutSecretCode).InArea(Area).AppRelativeUrl().ShouldEqual(urlWithoutSecretCode);
                urlWithoutSecretCode.WithMethod(HttpVerbs.Post).ShouldMapTo(actionWithoutSecretCode);
                urlWithoutSecretCode.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
                actionWithoutSecretCode.DefaultAreaRoutes(Area).ShouldMapToNothing();

                // ~/sign-up/confirm-email/validate/[Guid]/secret
                Expression<Func<SignUpController, ActionResult>> actionWithSecretCode =
                    controller => controller.ValidateConfirmEmail(token, secretCode);
                var urlWithSecretCode = string.Format(urlFormat, token, secretCode.UrlPathEncoded());
                OutBoundRoute.Of(actionWithSecretCode).InArea(Area).AppRelativeUrl().ShouldEqual(urlWithSecretCode);
                urlWithSecretCode = urlWithSecretCode.UrlDecoded();
                urlWithSecretCode.WithMethod(HttpVerbs.Post).ShouldMapTo(actionWithSecretCode);
                urlWithSecretCode.WithMethodsExcept(HttpVerbs.Post).ShouldMapToNothing();
                actionWithSecretCode.DefaultAreaRoutes(Area).ShouldMapToNothing();

                // ~/sign-up/confirm-email/validate/Guid.Empty
                Expression<Func<SignUpController, ActionResult>> actionWithEmptyToken =
                    controller => controller.ValidateConfirmEmail(Guid.Empty, string.Empty);
                var urlWithEmptyToken = string.Format(urlFormat, Guid.Empty, string.Empty);
                urlWithEmptyToken = urlWithEmptyToken.WithoutTrailingSlash();
                OutBoundRoute.Of(actionWithEmptyToken).InArea(Area).AppRelativeUrl().ShouldNotEqual(urlWithEmptyToken);
                urlWithEmptyToken.WithAnyMethod().ShouldMapToNothing();
                actionWithEmptyToken.DefaultAreaRoutes(Area).ShouldMapToNothing();

                // ~/sign-up/confirm-email/validate/not a Guid
                var urlWithInvalidToken = string.Format(urlFormat, "not a Guid", string.Empty);
                urlWithInvalidToken = urlWithInvalidToken.WithoutTrailingSlash();
                urlWithInvalidToken.WithAnyMethod().ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsErrorMessage_WhenModelIsInvalid()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                    SecretCode = null,
                };
                var di = new Mock<IServiceProvider>(MockBehavior.Strict);
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                di.Setup(m => m.GetService(typeof(IQueryEntities))).Returns(entityQueries.Object);
                entityQueries.Setup(m => m.People)
                        .Returns(new Person[] {}.AsQueryable);
                DependencyInjector.Set(di.Object);
                var controller = new SignUpController(null, null, null, null, null);

                // act 
                var result = controller.ValidateConfirmEmail(model.Token, model.SecretCode);

                // assert
                result.ShouldNotBeNull();
                result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.DenyGet);
                result.Data.ShouldEqual(ConfirmEmailForm.RequiredErrorMessage);
                entityQueries.Verify(m => m.People, Times.Never());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsTrue_WhenModelIsValid()
            {
                // arrange
                var model = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
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
                            new EmailConfirmation
                            {
                                Token = model.Token,
                                SecretCode = model.SecretCode,
                                ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                Intent = EmailConfirmationIntent.SignUp,
                                ConfirmedOnUtc = null,
                            }
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
                var controller = new SignUpController(null, null, null, null, null);

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
            public void HasOneRoute_SignUp_CreatePassword()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                   controller => controller.CreatePassword();
                var url = SignUpRouteMapper.CreatePassword.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethods(HttpVerbs.Get).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenTempDataIsLost()
            {
                // arrange
                var controller = new SignUpController(null, null, null, null, null);

                // act
                var result = controller.CreatePassword();

                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenIsEmptyGuid()
            {
                // arrange
                var token = Guid.Empty;
                var controller = new SignUpController(null, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void KeepsConfirmationTokenInTempData_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var controller = new SignUpController(new Mock<IQueryEntities>(MockBehavior.Default).Object,
                    null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(
                    SignUpController.ConfirmationTokenKey, token));
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void LooksForConfirmation_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] {}.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNull()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenEmailIsNotConfirmed()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                                },
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
                entityQueries.Setup(m => m.People).Returns(new[] {person }.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNotRedeemed()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = null,
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationIsExpired);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenPersonHasSignedUpUser()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.UserIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.MemberIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)), Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessView_WhenConfirmationIsValid()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreatePasswordForm>();
                var form = (CreatePasswordForm)viewResult.Model;
                form.Password.ShouldBeNull();
                form.ConfirmPassword.ShouldBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
            }
        }

        [TestClass]
        public class CreatePassword_Post
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void HasOneRoute_SignUp_CreatePassword()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                   controller => controller.CreatePassword(null);
                var url = SignUpRouteMapper.CreatePassword.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethods(HttpVerbs.Post).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Get, HttpVerbs.Post).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenModelIsNull()
            {
                // arrange
                var controller = new SignUpController(null, null, null, null, null);

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
                var model = new CreatePasswordForm();
                var controller = new SignUpController(null, null, null, null, null);

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void Returns404_WhenConfirmationTokenIsEmptyGuid()
            {
                // arrange
                var token = Guid.Empty;
                var model = new CreatePasswordForm();
                var controller = new SignUpController(null, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<HttpNotFoundResult>();
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsInputForm_WhenModelStateIsInvalid()
            {
                // arrange
                var token = Guid.NewGuid();
                var model = new CreatePasswordForm();
                var controller = new SignUpController(null, null, null, null, null);
                controller.ModelState.AddModelError("error", "error");
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(model);

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreatePasswordForm>();
                var form = (CreatePasswordForm)viewResult.Model;
                form.Password.ShouldEqual(model.Password);
                form.ConfirmPassword.ShouldEqual(model.ConfirmPassword);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void KeepsConfirmationTokenInTempData_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var controller = new SignUpController(new Mock<IQueryEntities>(MockBehavior.Strict).Object,
                    null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;
                controller.ModelState.AddModelError("error", "error");

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(
                    SignUpController.ConfirmationTokenKey, token));
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void LooksForConfirmation_WhenItIsNotNullOrEmpty()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] {}.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNull()
            {
                // arrange
                var token = Guid.NewGuid();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                entityQueries.Setup(m => m.People).Returns(new Person[] { }.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenEmailIsNotConfirmed()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsNotRedeemed()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = null,
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenConfirmationIsExpired()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.ConfirmationIsExpired);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenPersonHasSignedUpUser()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.UserIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsDeniedView_WhenMemberIsSignedUp()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                },
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, memberSigner.Object);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.CreatePassword(new CreatePasswordForm());

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.ViewName.ShouldEqual(MVC.Identity.SignUp.Views.create_denied);
                viewResult.Model.ShouldNotBeNull();
                viewResult.Model.ShouldBeType<CreateDeniedPage>();
                var page = (CreateDeniedPage)viewResult.Model;
                page.Reason.ShouldEqual(CreateDeniedPage.DeniedBecause.MemberIsSignedUp);
                controller.TempData.ShouldContain(new KeyValuePair<string, object>(SignUpController.ConfirmationTokenKey, token));
                entityQueries.Verify(m => m.People, Times.Once());
                memberSigner.Verify(m => m.IsSignedUp(It.Is<string>(s => s == emailAddress.Value)),
                    Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessRedirect_WhenPersonHasNullUser()
            {
                // arrange
                var token = Guid.NewGuid();
                var model = new CreatePasswordForm
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                    ConfirmedOnUtc = DateTime.UtcNow.Subtract(new TimeSpan(0, 1, 0)),
                                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                }
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
                var controller = new SignUpController(entityQueries.Object, commander.Object, null, null, memberSigner.Object);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

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
                    "action", MVC.Identity.SignUp.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
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
            public void HasOneRoute_SignUp_SignIn()
            {
                Expression<Func<SignUpController, ActionResult>> action =
                    controller => controller.SignIn();
                var url = SignUpRouteMapper.SignIn.Route.ToAppRelativeUrl();

                OutBoundRoute.Of(action).InArea(Area).AppRelativeUrl().ShouldEqual(url);
                url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
                url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
                action.DefaultAreaRoutes(Area).ShouldMapToNothing();
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void RedirectsToSignIn_WhenConfirmationTokenIsNotInTempData()
            {
                // arrange
                var controller = new SignUpController(null, null, null, null, null);

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
                    "controller", MVC.Identity.SignIn.Name), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.SignIn.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void RedirectsToSignIn_WhenConfirmationTokenIsEmptyGuid()
            {
                // arrange
                var token = Guid.Empty;
                var controller = new SignUpController(null, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

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
                    "controller", MVC.Identity.SignIn.Name), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.SignIn.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void RedirectsToSignIn_WhenConfirmationCannotBeFound()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = Guid.NewGuid(),
                                    Intent = EmailConfirmationIntent.SignUp,
                                }
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
                entityQueries.Setup(m => m.People).Returns(new[] {person}.AsQueryable);
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

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
                    "controller", MVC.Identity.SignIn.Name), new CaseInsensitiveKeyComparer());
                routeResult.RouteValues.ShouldContain(new KeyValuePair<string, object>(
                    "action", MVC.Identity.SignIn.ActionNames.SignIn), new CaseInsensitiveKeyComparer());
                controller.TempData.Keys.ShouldNotContain(SignUpController.ConfirmationTokenKey);

                entityQueries.Verify(m => m.People, Times.Once());
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void ReturnsSuccessView_WhenConfirmationTokenIsValid()
            {
                // arrange
                var token = Guid.NewGuid();
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
                                new EmailConfirmation
                                {
                                    Token = token,
                                    Intent = EmailConfirmationIntent.SignUp,
                                }
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
                var controller = new SignUpController(entityQueries.Object, null, null, null, null);
                controller.TempData[SignUpController.ConfirmationTokenKey] = token;

                // act
                var result = controller.SignIn();

                // assert
                result.ShouldNotBeNull();
                result.ShouldBeType<ViewResult>();
                var viewResult = (ViewResult)result;
                viewResult.Model.ShouldNotBeNull();
                viewResult.ViewName.ShouldEqual(string.Empty);
                viewResult.Model.ShouldBeType<SignInForm>();
                var form = (SignInForm)viewResult.Model;
                form.EmailAddress.ShouldNotBeNull();
                form.EmailAddress.ShouldEqual(emailAddress.Value);
                controller.TempData.Keys.ShouldContain(SignUpController.ConfirmationTokenKey);

                entityQueries.Verify(m => m.People, Times.Once());
            }
        }
    }
}
