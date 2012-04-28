using System;
using System.Linq.Expressions;
using System.Security.Principal;
using FluentValidation;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class UpdateMyAffiliationCommand
    {
        public IPrincipal Principal { get; set; }
        public int EstablishmentId { get; set; }
        public string JobTitles { get; set; }
        public bool IsClaimingStudent { get; set; }
        public bool IsClaimingEmployee { get; set; }
        public bool IsClaimingInternationalOffice { get; set; }
        public bool IsClaimingAdministrator { get; set; }
        public bool IsClaimingFaculty { get; set; }
        public bool IsClaimingStaff { get; set; }
        public int ChangeCount { get; internal set; }
        public bool ChangedState { get { return ChangeCount > 0; } }
    }

    public class UpdateMyAffiliationHandler : IHandleCommands<UpdateMyAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public UpdateMyAffiliationHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(UpdateMyAffiliationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the affiliation
            var affiliation = _queryProcessor.Execute(
                new GetMyAffiliationByEstablishmentIdQuery
                {
                    Principal = command.Principal,
                    EstablishmentId = command.EstablishmentId,
                }
            );

            // update fields
            if (!affiliation.IsAcknowledged) command.ChangeCount++;
            affiliation.IsAcknowledged = true;

            if (affiliation.JobTitles != command.JobTitles) command.ChangeCount++;
            affiliation.JobTitles = command.JobTitles;

            if (affiliation.IsClaimingStudent != command.IsClaimingStudent) command.ChangeCount++;
            affiliation.IsClaimingStudent = command.IsClaimingStudent;

            if (affiliation.IsClaimingEmployee != command.IsClaimingEmployee) command.ChangeCount++;
            affiliation.IsClaimingEmployee = command.IsClaimingEmployee;

            if (affiliation.IsClaimingInternationalOffice != command.IsClaimingInternationalOffice) command.ChangeCount++;
            affiliation.IsClaimingInternationalOffice = command.IsClaimingInternationalOffice;

            if (affiliation.IsClaimingAdministrator != command.IsClaimingAdministrator) command.ChangeCount++;
            affiliation.IsClaimingAdministrator = command.IsClaimingAdministrator;

            if (affiliation.IsClaimingFaculty != command.IsClaimingFaculty) command.ChangeCount++;
            affiliation.IsClaimingFaculty = command.IsClaimingFaculty;

            if (affiliation.IsClaimingStaff != command.IsClaimingStaff) command.ChangeCount++;
            affiliation.IsClaimingStaff = command.IsClaimingStaff;

            // store
            if (command.ChangeCount > 0) _entities.Update(affiliation);
        }
    }

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
