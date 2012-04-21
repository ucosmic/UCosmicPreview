using System;
using System.Linq.Expressions;
using System.Security.Principal;
using FluentValidation;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class UpdateMyAffiliationValidator : AbstractValidator<UpdateMyAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateMyAffiliationValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Principal)

                // principal cannot be null
                .NotEmpty().WithMessage(
                    ValidatePrincipal.FailedBecausePrincipalWasNull)

                // principal.identity.name cannot be null, empty, or whitespace
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameWasEmpty)

                // principal.identity.name must match User.Name entity property
                .Must(ValidatePrincipalIdentityNameMatchesUser).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        p => p.Principal.Identity.Name)
            ;

            RuleFor(p => p.EstablishmentId)

                // establishment id must exist in database
                .Must(ValidateEstablishmentIdMatchesEntity).WithMessage(
                    ValidateEstablishment.FailedBecauseIdMatchedNoEntity,
                        p => p.EstablishmentId)
            ;
        }

        private bool ValidatePrincipalIdentityNameMatchesUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }

        private bool ValidateEstablishmentIdMatchesEntity(int establishmentId)
        {
            return ValidateEstablishment.IdMatchesEntity(establishmentId, _queryProcessor,
                new Expression<Func<Establishment, object>>[]
                {
                    e => e.Type.Category,
                }
            );
        }
    }
}
