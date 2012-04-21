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
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Number)
            ;

            RuleFor(p => p.Value)

                // email address cannot be empty
                .NotEmpty().WithMessage(
                    FailedWithPreviousSpellingDoesNotMatchCaseInsensitively)

                // must be valid against email address regular expression
                .EmailAddress().WithMessage(
                    FailedWithPreviousSpellingDoesNotMatchCaseInsensitively)

                // validate the number within the Value property b/c remote only validates this property
                .Must(ValidateEmailAddressNumberAndPrincipalMatchesEntity).WithMessage(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        p => p.Number)

                // must match previous spelling case insensitively
                .Must(ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively).WithMessage(
                    FailedWithPreviousSpellingDoesNotMatchCaseInsensitively)
            ;
        }

        private EmailAddress _email;

        internal const string FailedWithPreviousSpellingDoesNotMatchCaseInsensitively
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        private bool ValidateEmailAddressNumberAndPrincipalMatchesEntity(ChangeEmailSpellingForm form, string value)
        {
            var principalIdentityName = form.PersonUserName ?? string.Empty;
            var principal = new GenericPrincipal(new GenericIdentity(principalIdentityName), null);
            return ValidateEmailAddress.NumberAndPrincipalMatchesEntity(form.Number, principal, _queryProcessor, out _email);
        }

        private bool ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively(ChangeEmailSpellingForm form, string value)
        {
            return ValidateEmailAddress.NewValueMatchesCurrentValueCaseInsensitively(value, _email);
        }
    }
}