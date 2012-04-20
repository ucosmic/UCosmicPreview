using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class AffiliationValidator : AbstractValidator<AffiliationForm>
    {
        public const string EmployeeOrStudentRequiredErrorMessage = "Please indicate which statement best describes this affiliation.";

        public AffiliationValidator()
        {
            RuleFor(p => p.EmployeeOrStudent)
                .NotEmpty().WithMessage(EmployeeOrStudentRequiredErrorMessage)
            ;
        }
    }
}