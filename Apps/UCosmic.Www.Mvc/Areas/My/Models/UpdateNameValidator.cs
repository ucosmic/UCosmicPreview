using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class UpdateNameValidator : AbstractValidator<UpdateNameForm>
    {
        public const string DisplayNameRequiredErrorMessage = "Display name is required.";

        public UpdateNameValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.DisplayName)

                // person must have a display name
                .NotEmpty().WithMessage(
                    DisplayNameRequiredErrorMessage)
            ;
        }
    }
}