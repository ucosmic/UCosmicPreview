using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class UpdateNameValidator : AbstractValidator<UpdateNameForm>
    {
        public const string DisplayNameRequiredErrorMessage = "Display name is required.";

        public UpdateNameValidator()
        {
            RuleFor(p => p.DisplayName)
                .NotEmpty().WithMessage(DisplayNameRequiredErrorMessage)
            ;
        }
    }
}