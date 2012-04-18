using FluentValidation;
using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class ChangeEmailSpellingValidator : AbstractValidator<ChangeEmailSpellingForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public ChangeEmailSpellingValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            const string changeEmailSpellingErrorMessage =
                ChangeEmailAddressSpellingValidator.ChangeEmailSpellingErrorMessage;

            RuleFor(p => p.Value)
                .NotEmpty().WithMessage(changeEmailSpellingErrorMessage)
                .EmailAddress().WithMessage(changeEmailSpellingErrorMessage)
                .Must(MatchPreviousSpellingCaseInvariantly).WithMessage(changeEmailSpellingErrorMessage)
            ;
        }

        private bool MatchPreviousSpellingCaseInvariantly(ChangeEmailSpellingForm form, string value)
        {
            return ChangeEmailAddressSpellingValidator
                .NewEmailMatchesPreviousSpellingCaseInvariantly
                    (value, form.PersonUserName, form.Number, _queryProcessor);
        }
    }
}