using System;
using FluentValidation;
using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses
{
    public class ChangeSpellingFormValidator : AbstractValidator<ChangeSpellingForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public ChangeSpellingFormValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.Value)
                .NotEmpty().WithMessage(ChangeEmailSpellingErrorMessage)
                .Must(MatchPreviousSpellingCaseInvariantly).WithMessage(ChangeEmailSpellingErrorMessage)
            ;
        }

        public const string ChangeEmailSpellingErrorMessage
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        private bool MatchPreviousSpellingCaseInvariantly(ChangeSpellingForm form, string value)
        {
            var email = _queryProcessor.Execute(
                new GetEmailAddressByUserNameAndNumberQuery
                {
                    UserName = form.PersonUserName,
                    Number = form.Number,
                }
            );

            return email != null 
                && email.Value.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}