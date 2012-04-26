using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class SignOnEmailAddressValidatorRules
    {
        public const string RequiredMessage = "Email address is required.";
        public const string RegexMessage = "Please enter a valid email address.";

        public static IRuleBuilder<T, string> SignOnEmailAddressRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotEmpty().WithMessage(RequiredMessage)
                .EmailAddress().WithMessage(RegexMessage)
            ;
            return ruleBuilder;
        }
    }
}