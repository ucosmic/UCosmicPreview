using System.Security.Principal;
using FluentValidation;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class UpdateEmailValueValidator : AbstractValidator<UpdateEmailValueForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateEmailValueValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Value)

                // email address cannot be empty
                .NotEmpty().WithMessage(
                    FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively)

                // must be valid against email address regular expression
                .EmailAddress().WithMessage(
                    FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively)

                // validate the number within the Value property b/c remote only validates this property
                .Must(ValidateEmailAddressNumberAndPrincipalMatchesEntity).WithMessage(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        p => p.Number)

                // must match previous spelling case insensitively
                .Must(ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively).WithMessage(
                    FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively)
            ;
        }

        private EmailAddress _email;

        internal const string FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        private bool ValidateEmailAddressNumberAndPrincipalMatchesEntity(UpdateEmailValueForm form, string value)
        {
            var principalIdentityName = form.PersonUserName ?? string.Empty;
            var principal = new GenericPrincipal(new GenericIdentity(principalIdentityName), null);
            return ValidateEmailAddress.NumberAndPrincipalMatchesEntity(form.Number, principal, _queryProcessor, out _email);
        }

        private bool ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively(UpdateEmailValueForm form, string value)
        {
            return ValidateEmailAddress.NewValueMatchesCurrentValueCaseInsensitively(value, _email);
        }
    }
}