﻿using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    // ReSharper disable UnusedMember.Global
    public class SignOnBeginFormValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_SignOnBeginForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<SignOnBeginForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<SignOnBeginFormValidator>();
            }
        }

        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsNull()
            {
                var validator = new SignOnBeginFormValidator(null);
                var model = new SignOnBeginForm { EmailAddress = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().ErrorMessage.ShouldEqual(
                    SignOnEmailAddressValidatorRules.RequiredMessage);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsEmpty()
            {
                var validator = new SignOnBeginFormValidator(null);
                var model = new SignOnBeginForm { EmailAddress = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().ErrorMessage.ShouldEqual(
                    SignOnEmailAddressValidatorRules.RequiredMessage);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsWhiteSpace()
            {
                var validator = new SignOnBeginFormValidator(null);
                var model = new SignOnBeginForm { EmailAddress = " \t " };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().ErrorMessage.ShouldEqual(
                    SignOnEmailAddressValidatorRules.RequiredMessage);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsMissingTldExtension()
            {
                var validator = new SignOnBeginFormValidator(null);
                var model = new SignOnBeginForm { EmailAddress = "email@domain" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().ErrorMessage.ShouldEqual(
                    SignOnEmailAddressValidatorRules.RegexMessage);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_HasNoMatchingEstablishment()
            {
                const string emailAddress = "email@domain.tld";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(
                    It.Is<GetEstablishmentByEmailQuery>(q => q.Email == emailAddress)))
                        .Returns(null as Establishment);
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                var model = new SignOnBeginForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().ErrorMessage.ShouldEqual(string.Format(
                    SignOnBeginFormValidator.IneligibleEmailMessage, emailAddress));
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_MatchingEstablishment_IsNotMember()
            {
                const string emailAddress = "email@domain.tld";
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(
                    It.Is<GetEstablishmentByEmailQuery>(q => q.Email == emailAddress)))
                        .Returns(new Establishment { IsMember = false, });
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                var model = new SignOnBeginForm { EmailAddress = emailAddress };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldEqual(1);
                results.Errors.Single().ErrorMessage.ShouldEqual(string.Format(
                    SignOnBeginFormValidator.IneligibleEmailMessage, emailAddress));
            }

            [TestMethod]
            public void HasNoErrorWhen_EmailAddress_IsValidEmailAddress_AndBelongsToMemberEstablishment()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEstablishmentByEmailQuery>()))
                    .Returns(new Establishment { IsMember = true, });
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                var model = new SignOnBeginForm { EmailAddress = "email@domain.tld" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeTrue();
                results.Errors.Count.ShouldEqual(0);
            }
        }
    }
}
