using FluentValidation;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    public class SignOnBeginFormValidator : AbstractValidator<SignOnBeginForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public SignOnBeginFormValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EmailAddress)

                // validate the email address
                .SignOnEmailAddressRules()

                // establishment must exist
                .Must(ValidateEstablishmentEmailMatchesEntity).WithMessage(
                    FailedBecauseEstablishmentIsNotEligible,
                        p => p.EmailAddress)

                // establishment must be a member
                .Must(ValidateEstablishmentIsMember).WithMessage(
                    FailedBecauseEstablishmentIsNotEligible,
                        p => p.EmailAddress)
            ;
        }

        private Establishment _establishment;

        internal const string FailedBecauseEstablishmentIsNotEligible = "Sorry, but the email address \"{0}\" is not eligible at this time.";

        private bool ValidateEstablishmentEmailMatchesEntity(string emailAddress)
        {
            return ValidateEstablishment.EmailMatchesEntity(emailAddress, _queryProcessor, out _establishment);
        }

        private bool ValidateEstablishmentIsMember(string emailAddress)
        {
            return ValidateEstablishment.IsMember(_establishment);
        }
    }
}