using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    public static class SignOnEmailAddressValidatorRules
    {
        public static IRuleBuilder<T, string> SignOnEmailAddressRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotEmpty().WithMessage(SignOnBeginForm.EmailAddressRequiredMessage)
                .EmailAddress().WithMessage(SignOnBeginForm.EmailAddressRegexMessage)
            ;
            return ruleBuilder;
        }
    }
}