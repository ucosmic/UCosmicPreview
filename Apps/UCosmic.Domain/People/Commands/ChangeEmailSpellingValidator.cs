using System;
using FluentValidation;

namespace UCosmic.Domain.People
{
    public class ChangeEmailSpellingValidator : AbstractValidator<ChangeEmailSpellingCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public ChangeEmailSpellingValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
            ;

            RuleFor(p => p.Number)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
            ;

            RuleFor(p => p.NewValue)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .Must(MatchPreviousSpellingCaseInvariantly).WithMessage(ChangeEmailSpellingErrorMessage)
            ;
        }

        public const string ChangeEmailSpellingErrorMessage
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        private bool MatchPreviousSpellingCaseInvariantly(ChangeEmailSpellingCommand command, string value)
        {
            return NewEmailMatchesPreviousSpellingCaseInvariantly
                (value, command.UserName, command.Number, _queryProcessor);
        }

        public static bool NewEmailMatchesPreviousSpellingCaseInvariantly(string newValue, string userName, int number, IProcessQueries queryProcessor)
        {
            var email = queryProcessor.Execute(
                new GetEmailAddressByUserNameAndNumberQuery
                {
                    UserName = userName,
                    Number = number,
                }
            );

            return email != null
                && email.Value.Equals(newValue, StringComparison.OrdinalIgnoreCase);
        }
    }
}
