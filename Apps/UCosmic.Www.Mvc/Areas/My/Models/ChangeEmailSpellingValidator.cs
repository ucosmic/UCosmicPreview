using FluentValidation;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class ChangeEmailSpellingValidator : AbstractValidator<ChangeEmailSpellingForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public ChangeEmailSpellingValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            const string changeEmailSpellingErrorMessage =
                Domain.People.ChangeEmailSpellingValidator.ChangeEmailSpellingErrorMessage;

            RuleFor(p => p.Value)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(changeEmailSpellingErrorMessage)
                .EmailAddress().WithMessage(changeEmailSpellingErrorMessage)
                .Must(MatchPreviousSpellingCaseInvariantly).WithMessage(changeEmailSpellingErrorMessage)
            ;
        }

        private bool MatchPreviousSpellingCaseInvariantly(ChangeEmailSpellingForm form, string value)
        {
            return Domain.People.ChangeEmailSpellingValidator
                .NewEmailMatchesPreviousSpellingCaseInvariantly
                    (value, form.PersonUserName, form.Number, _queryProcessor);
        }
    }
}