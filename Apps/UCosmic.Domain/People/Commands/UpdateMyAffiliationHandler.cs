using System;

namespace UCosmic.Domain.People
{
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
}
