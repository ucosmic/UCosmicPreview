using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class AffiliationValidator : AbstractValidator<AffiliationForm>
    {
        public const string EmployeeOrStudentAffiliationRequiredErrorMessage = "Please indicate which statement best describes this affiliation.";

        public AffiliationValidator()
        {
            RuleFor(p => p.EmployeeOrStudentAffiliation)
                .NotEmpty().WithMessage(EmployeeOrStudentAffiliationRequiredErrorMessage)
            ;
        }
    }
}