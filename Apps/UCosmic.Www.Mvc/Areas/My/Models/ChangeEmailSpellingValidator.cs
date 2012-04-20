using System.Security.Principal;
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
                ChangeMyEmailSpellingValidator.FailedWithPreviousSpellingDoesNotMatchCaseInvariantly;

            RuleFor(p => p.Value)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(changeEmailSpellingErrorMessage)
                .EmailAddress().WithMessage(changeEmailSpellingErrorMessage)
                .Must(MatchPreviousSpellingCaseInvariantly).WithMessage(changeEmailSpellingErrorMessage)
            ;
        }

        private bool MatchPreviousSpellingCaseInvariantly(ChangeEmailSpellingForm form, string value)
        {
            return ChangeMyEmailSpellingValidator
                .NewEmailMatchesPreviousSpellingCaseInvariantly
                    (value, new GenericPrincipal(new GenericIdentity(form.PersonUserName), null), form.Number, _queryProcessor);
        }
    }
}