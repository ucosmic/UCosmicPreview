using System;
using FluentValidation;
using UCosmic.Domain.Identity;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class ChangeMyEmailSpellingValidator : AbstractValidator<ChangeMyEmailSpellingCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public ChangeMyEmailSpellingValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.Principal)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(ValidatePrincipal.FailedWithEmptyIdentityName)
                .Must(MatchIdentityNameWithUser).WithMessage(ValidatePrincipal.FailedWithNoUserMatchesIdentityName, p => p.Principal.Identity.Name)
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
                .Must(MatchPreviousSpellingCaseInvariantly).WithMessage(FailedWithPreviousSpellingDoesNotMatchCaseInvariantly)
            ;
        }

        private bool MatchIdentityNameWithUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }

        public const string FailedWithPreviousSpellingDoesNotMatchCaseInvariantly
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        private bool MatchPreviousSpellingCaseInvariantly(ChangeMyEmailSpellingCommand command, string value)
        {
            return NewEmailMatchesPreviousSpellingCaseInvariantly
                (value, command.Principal, command.Number, _queryProcessor);
        }

        public static bool NewEmailMatchesPreviousSpellingCaseInvariantly(string newValue, IPrincipal principal, int number, IProcessQueries queryProcessor)
        {
            var email = queryProcessor.Execute(
                new GetMyEmailAddressByNumberQuery
                {
                    Principal = principal,
                    Number = number,
                }
            );

            return email != null
                && email.Value.Equals(newValue, StringComparison.OrdinalIgnoreCase);
        }
    }
}
