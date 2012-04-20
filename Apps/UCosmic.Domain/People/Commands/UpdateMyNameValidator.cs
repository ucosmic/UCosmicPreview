using FluentValidation;
using UCosmic.Domain.Identity;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class UpdateMyNameValidator : AbstractValidator<UpdateMyNameCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateMyNameValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.DisplayName)
                .NotEmpty()
            ;

            RuleFor(p => p.Principal).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(ValidatePrincipal.FailedWithEmptyIdentityName)
                .Must(MatchIdentityNameWithUser).WithMessage(ValidatePrincipal.FailedWithNoUserMatchesIdentityName, p => p.Principal.Identity.Name)
            ;
        }

        private bool MatchIdentityNameWithUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }
    }
}
