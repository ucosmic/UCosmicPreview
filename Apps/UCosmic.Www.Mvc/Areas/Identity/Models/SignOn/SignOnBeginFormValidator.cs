using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    public class SignOnBeginFormValidator : AbstractValidator<SignOnBeginForm>
    {
        public SignOnBeginFormValidator()
        {
            RuleFor(p => p.EmailAddress).SignOnEmailAddressRules();
        }
    }
}