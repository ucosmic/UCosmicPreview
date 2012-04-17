using System.Web;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain;
using UCosmic.Domain.People;
using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses
{
    // ReSharper disable UnusedMember.Global
    public class ChangeSpellingFormValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_ChangeEmailSpellingForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap();

                var validator = container.GetInstance<IValidator<ChangeSpellingForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<ChangeSpellingFormValidator>();
            }
        }

        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void HasErrorWhen_Value_IsNull()
            {
                var validator = new ChangeSpellingFormValidator(null);
                validator.ShouldHaveValidationErrorFor(model => model.Value, null as string);
            }

            [TestMethod]
            public void HasErrorWhen_Value_IsEmpty()
            {
                var validator = new ChangeSpellingFormValidator(null);
                validator.ShouldHaveValidationErrorFor(model => model.Value, string.Empty);
            }

            [TestMethod]
            public void HasErrorWhen_Value_IsMissingTldExtension()
            {
                var validator = new ChangeSpellingFormValidator(null);
                validator.ShouldHaveValidationErrorFor(model => model.Value, "email@domain");
            }

            [TestMethod]
            public void HasErrorWhen_Value_DoesNotMatchPreviousSpelling_CaseInsensitively()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEmailAddressByUserNameAndNumberQuery>()))
                    .Returns(new EmailAddress{ Value = "user@domain.tld"});
                var validator = new ChangeSpellingFormValidator(queryProcessor.Object);
                validator.ShouldHaveValidationErrorFor(model => model.Value, "user2@domain2.tld");
            }

            [TestMethod]
            public void HasErrorWhen_PreviousSpelling_CannotBeFound()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEmailAddressByUserNameAndNumberQuery>()))
                    .Returns(null as EmailAddress);
                var validator = new ChangeSpellingFormValidator(queryProcessor.Object);
                validator.ShouldHaveValidationErrorFor(model => model.Value, "user@domain.tld");
            }

            [TestMethod]
            public void HasNoErrorWhen_Value_MatchesPreviousSpelling_CaseInsensitively()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<GetEmailAddressByUserNameAndNumberQuery>()))
                    .Returns(new EmailAddress { Value = "user@domain.tld" });
                var validator = new ChangeSpellingFormValidator(queryProcessor.Object);
                validator.ShouldNotHaveValidationErrorFor(model => model.Value, "User@Domain.Tld");
            }
        }
    }
}
