using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class CreateOrUpdateInstitutionalAgreementCommand
    {
        public CreateOrUpdateInstitutionalAgreementCommand(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
        public int RevisionId { get; set; }
        public string Title { get; set; }
        public bool IsTitleDerived { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool? IsAutoRenew { get; set; }
        public DateTime StartsOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpirationEstimated { get; set; }
        public InstitutionalAgreementVisibility Visibility { get; set; }
        public Guid? UmbrellaEntityId { get; set; }
        public IEnumerable<Guid> RemoveParticipantEstablishmentEntityIds { get; set; }
        public IEnumerable<Guid> AddParticipantEstablishmentEntityIds { get; set; }
        public IEnumerable<Guid> RemoveContactEntityIds { get; set; }
        public IEnumerable<InstitutionalAgreementContact> AddContacts { get; set; }
        public IEnumerable<Guid> RemoveFileEntityIds { get; set; }
        public IEnumerable<Guid> AddFileEntityIds { get; set; }
        public int ChangeCount { get; internal set; }
        public Guid EntityId { get; internal set; }
    }

    public class CreateOrUpdateInstitutionalAgreementHandler : IHandleCommands<CreateOrUpdateInstitutionalAgreementCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IHandleCommands<UpdateInstitutionalAgreementHierarchyCommand> _hierarchyHandler;

        public CreateOrUpdateInstitutionalAgreementHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            //, IUnitOfWork unitOfWork
            , IHandleCommands<UpdateInstitutionalAgreementHierarchyCommand> hierarchyHandler
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            //_unitOfWork = unitOfWork;
            _hierarchyHandler = hierarchyHandler;
        }

        public void Handle(CreateOrUpdateInstitutionalAgreementCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // start with an agreement entity
            var entity = _queryProcessor.Execute(
                new GetInstitutionalAgreementByIdQuery(command.RevisionId)
                {
                    EagerLoad = new Expression<Func<InstitutionalAgreement, object>>[]
                    {
                        a => a.Umbrella,
                    }
                }
            );
            if (entity == null && command.RevisionId == 0)
                entity = new InstitutionalAgreement
                {
                    EntityId = Guid.NewGuid(),
                };
            if (entity == null)
                throw new InvalidOperationException(string.Format(
                    "Agreement with id '{0}' does not exist", command.RevisionId));

            // update scalars
            CopyScalars(command, entity);

            // scenario 1: no previous umbrella, no current umbrella.
            // scenario 2: no previous umbrella, with current umbrella.
            // scenario 3: with previous umbrella, same current umbrella.
            // scenario 4: with previous umbrella, different current umbrella.
            // scenario 5: with previous umbrella, no current umbrella.
            var previousUmbrella = entity.Umbrella;
            if (command.UmbrellaEntityId.HasValue &&
                (previousUmbrella == null || previousUmbrella.EntityId != command.UmbrellaEntityId.Value))
            {
                //entity.Umbrella = finder.FindOne(By<InstitutionalAgreement>.EntityId
                //    (command.UmbrellaEntityId.Value).ForInsertOrUpdate());
                entity.Umbrella = _queryProcessor.Execute(
                    new GetInstitutionalAgreementByGuidQuery(command.UmbrellaEntityId.Value));
                ++command.ChangeCount;
            }
            else if (previousUmbrella != null && !command.UmbrellaEntityId.HasValue)
            {
                entity.Umbrella = null;
                ++command.ChangeCount;
            }

            //if (removedParticipantEstablishmentIds != null)
            //    foreach (var removedParticipantEstablishmentId in removedParticipantEstablishmentIds)
            //        changes += entity.RemoveParticipant(removedParticipantEstablishmentId, _commander);
            if (command.RemoveParticipantEstablishmentEntityIds != null)
                foreach (var removedParticipantEstablishmentId in command.RemoveParticipantEstablishmentEntityIds)
                    command.ChangeCount += entity.RemoveParticipant(removedParticipantEstablishmentId, _entities);

            //if (addedParticipantEstablishmentIds != null)
            //    foreach (var addedParticipantEstablishmentId in addedParticipantEstablishmentIds)
            //        changes += entity.AddParticipant(principal, addedParticipantEstablishmentId,
            //            new EstablishmentFinder(_entityQueries));
            if (command.AddParticipantEstablishmentEntityIds != null)
                foreach (var addedParticipantEstablishmentId in command.AddParticipantEstablishmentEntityIds)
                    command.ChangeCount += entity.AddParticipant(command.Principal, addedParticipantEstablishmentId,
                        _queryProcessor);

            //if (removedContactEntityIds != null)
            //    foreach (var removedContactEntityId in removedContactEntityIds.Where(v => v != Guid.Empty))
            //        changes += entity.RemoveContact(removedContactEntityId, _commander);
            if (command.RemoveContactEntityIds != null)
                foreach (var removedContactEntityId in command.RemoveContactEntityIds.Where(v => v != Guid.Empty))
                    command.ChangeCount += entity.RemoveContact(removedContactEntityId, _entities);

            //if (addedContacts != null)
            //    foreach (var addedContact in addedContacts)
            //        changes += entity.AddContact(addedContact,
            //            new PersonFinder(_entityQueries));
            if (command.AddContacts != null)
                foreach (var addedContact in command.AddContacts)
                    command.ChangeCount += entity.AddContact(addedContact, _queryProcessor);

            //if (removedFileEntityIds != null)
            //    foreach (var removedFileEntityId in removedFileEntityIds)
            //        changes += entity.RemoveFile(removedFileEntityId, _commander);
            if (command.RemoveFileEntityIds != null)
                foreach (var removedFileEntityId in command.RemoveFileEntityIds)
                    command.ChangeCount += entity.RemoveFile(removedFileEntityId, _entities);

            //if (addedFileEntityIds != null)
            //    foreach (var addedFileEntityId in addedFileEntityIds)
            //        changes += entity.AddFile(addedFileEntityId,
            //            new FileFactory(_commander, _entityQueries));
            if (command.AddFileEntityIds != null)
                foreach (var addedFileEntityId in command.AddFileEntityIds)
                    command.ChangeCount += entity.AddFile(addedFileEntityId, _queryProcessor, _entities);

            command.EntityId = entity.EntityId;
            if (entity.RevisionId == 0 || command.ChangeCount > 0)
            {
                if (entity.RevisionId == 0) _entities.Create(entity);
                else if (command.ChangeCount > 0) _entities.Update(entity);
                DeriveNodes(entity, previousUmbrella);
                //_unitOfWork.SaveChanges();
                command.RevisionId = entity.RevisionId;
            }
        }

        private void DeriveNodes(InstitutionalAgreement agreement, InstitutionalAgreement previousUmbrella)
        {
            //DeriveNodes(agreement);
            _hierarchyHandler.Handle(new UpdateInstitutionalAgreementHierarchyCommand(agreement));
            if (previousUmbrella != null &&
                (agreement.Umbrella == null || agreement.Umbrella.EntityId != previousUmbrella.EntityId))
                //DeriveNodes(previousUmbrella);
                _hierarchyHandler.Handle(new UpdateInstitutionalAgreementHierarchyCommand(previousUmbrella));
        }

        private static void CopyScalars(CreateOrUpdateInstitutionalAgreementCommand command, InstitutionalAgreement entity)
        {
            if (command.Title != entity.Title) ++command.ChangeCount;
            if (command.IsTitleDerived != entity.IsTitleDerived) ++command.ChangeCount;
            if (command.Type != entity.Type) ++command.ChangeCount;
            if (command.Status != entity.Status) ++command.ChangeCount;
            if (command.Description != entity.Description) ++command.ChangeCount;
            if (command.IsAutoRenew != entity.IsAutoRenew) ++command.ChangeCount;
            if (command.StartsOn != entity.StartsOn) ++command.ChangeCount;
            if (command.ExpiresOn != entity.ExpiresOn) ++command.ChangeCount;
            if (command.IsExpirationEstimated != entity.IsExpirationEstimated) ++command.ChangeCount;
            if (command.Visibility != entity.Visibility) ++command.ChangeCount;

            entity.Title = command.Title;
            entity.IsTitleDerived = command.IsTitleDerived;
            entity.Type = command.Type;
            entity.Status = command.Status;
            entity.Description = command.Description;
            entity.IsAutoRenew = command.IsAutoRenew;
            entity.StartsOn = command.StartsOn;
            entity.ExpiresOn = command.ExpiresOn;
            entity.IsExpirationEstimated = command.IsExpirationEstimated;
            entity.Visibility = command.Visibility;
        }
    }
}
