using FluentValidation;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class AffiliationValidator : AbstractValidator<AffiliationForm>
    {
        public const string FailedBecauseEmployeeOrStudentAffiliationWasEmpty =
            "Please indicate which statement best describes this affiliation.";

        public AffiliationValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EmployeeOrStudentAffiliation)

                // must indicate whether affiliation represents employee, student, neither, or both
                .NotEmpty().WithMessage(
                    FailedBecauseEmployeeOrStudentAffiliationWasEmpty)
            ;
        }
    }
}