using System.Security.Principal;
using FluentValidation;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class ChangeMyEmailSpellingValidator : AbstractValidator<ChangeMyEmailSpellingCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public ChangeMyEmailSpellingValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Principal)

                // principal cannot be null
                .NotEmpty().WithMessage(
                    ValidatePrincipal.FailedBecausePrincipalWasNull)

                // principal identity name cannot be null or whitespace
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameWasEmpty)

                // principal identity name must match User.Name entity property
                .Must(ValidatePrincipalIdentityNameMatchesUser).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        p => p.Principal.Identity.Name)
            ;

            RuleFor(p => p.Number)

                // number must match email for user
                .Must(ValidateEmailAddressNumberAndPrincipalMatchesEntity).WithMessage(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        p => p.Number)
            ;

            RuleFor(p => p.NewValue)

                // new email address cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailAddress.FailedBecauseValueWasEmpty)

                // must be valid against email address regular expression
                .EmailAddress().WithMessage(
                    ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress,
                        p => p.NewValue)

                // must match previous spelling case insensitively
                .Must(ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively).WithMessage(
                    ValidateEmailAddress.FailedBecauseNewValueDidNotMatchCurrentValueCaseInvsensitively,
                        p => p.NewValue)
            ;
        }

        private EmailAddress _email;

        private bool ValidatePrincipalIdentityNameMatchesUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }

        private bool ValidateEmailAddressNumberAndPrincipalMatchesEntity(ChangeMyEmailSpellingCommand command, int number)
        {
            return ValidateEmailAddress.NumberAndPrincipalMatchesEntity(number, command.Principal, _queryProcessor, out _email);
        }

        private bool ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively(string newValue)
        {
            return ValidateEmailAddress.NewValueMatchesCurrentValueCaseInsensitively(newValue, _email);
        }
    }
}
