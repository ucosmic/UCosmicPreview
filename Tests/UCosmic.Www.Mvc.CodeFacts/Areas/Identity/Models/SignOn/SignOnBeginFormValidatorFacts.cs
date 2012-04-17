using System.Web;
using FluentValidation;
using FluentValidation.TestHelper;
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
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, null as string);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsEmpty()
            {
                var validator = new SignOnBeginFormValidator(null);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, string.Empty);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsWhiteSpace()
            {
                var validator = new SignOnBeginFormValidator(null);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, " \t ");
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsMissingTldExtension()
            {
                var validator = new SignOnBeginFormValidator(null);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, "email@domain");
            }

            [TestMethod]
            public void HasNoErrorWhen_EmailAddress_IsValidEmailAddress_AndBelongsToMemberEstablishment()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEstablishmentByEmailQuery>()))
                    .Returns(new Establishment { IsMember = true, });
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                validator.ShouldNotHaveValidationErrorFor(model => model.EmailAddress, "email@domain.tld");
            }
        }
    }
}
