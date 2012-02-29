using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    // ReSharper disable UnusedMember.Global
    public class SignOnEmailAddressValidatorRulesFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheSignOnEmailAddressRulesMethod
        {
            [TestMethod]
            public void Returns_IRuleBuilder()
            {
                var validator = new Mock<AbstractValidator<SignOnBeginForm>>();
                var ruleBuilder = validator.Object.RuleFor(p => p.EmailAddress)
                    .SignOnEmailAddressRules();
                ruleBuilder.ShouldNotBeNull();
            }
        }
    }
}
