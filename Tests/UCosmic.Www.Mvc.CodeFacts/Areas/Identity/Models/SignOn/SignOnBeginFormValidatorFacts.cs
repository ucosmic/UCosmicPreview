using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;

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
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindEstablishmentByEmailQuery>())).Returns(new Establishment());
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, null as string);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsEmpty()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindEstablishmentByEmailQuery>())).Returns(new Establishment());
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, string.Empty);
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsWhiteSpace()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindEstablishmentByEmailQuery>())).Returns(new Establishment());
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, " \t ");
            }

            [TestMethod]
            public void HasErrorWhen_EmailAddress_IsMissingTldExtension()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindEstablishmentByEmailQuery>())).Returns(new Establishment());
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                validator.ShouldHaveValidationErrorFor(model => model.EmailAddress, "email@domain");
            }

            [TestMethod]
            public void HasNoErrorWhen_EmailAddress_IsValidEmailAddress()
            {
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.IsAny<FindEstablishmentByEmailQuery>())).Returns(new Establishment());
                var validator = new SignOnBeginFormValidator(queryProcessor.Object);
                validator.ShouldNotHaveValidationErrorFor(model => model.EmailAddress, "email@domain.tld");
            }
        }
    }
}
