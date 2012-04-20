using FluentValidation;
using UCosmic.Domain.Identity;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class UpdateMyAffiliationValidator : AbstractValidator<UpdateMyAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateMyAffiliationValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.Principal).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameWasEmpty)
                .Must(ValidatePrincipalIdentityNameMatchesUser).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        p => p.Principal.Identity.Name)
            ;
        }

        private bool ValidatePrincipalIdentityNameMatchesUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }
    }
}
