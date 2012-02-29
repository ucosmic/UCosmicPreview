using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    // ReSharper disable UnusedMember.Global
    public class SignOnBeginFormValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsNull()
            {
                var validator = new SignOnBeginFormValidator();
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, null as string);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsEmpty()
            {
                var validator = new SignOnBeginFormValidator();
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, string.Empty);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsWhiteSpace()
            {
                var validator = new SignOnBeginFormValidator();
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, " \t ");
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsMissingTldExtension()
            {
                var validator = new SignOnBeginFormValidator();
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, "email@domain");
            }

            [TestMethod]
            public void HasNoErrorWhen_EmailAddress_IsValidEmailAddress()
            {
                var validator = new SignOnBeginFormValidator();
                validator.ShouldNotHaveValidationErrorFor(model => model.EmailAddress, "email@domain.tld");
            }
        }
    }
}
