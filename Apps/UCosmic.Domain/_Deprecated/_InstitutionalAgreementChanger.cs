//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using UCosmic.Domain.Establishments;
//using UCosmic.Domain.People;
//using UCosmic.Domain.Files;

//namespace UCosmic.Domain.InstitutionalAgreements
//{
//    public class InstitutionalAgreementChanger
//    {
//        private readonly ICommandObjects _commander;
//        private readonly IQueryEntities _entityQueries;

//        public InstitutionalAgreementChanger(ICommandObjects objectCommander, IQueryEntities entityQueries)
//        {
//            if (objectCommander == null)
//                throw new ArgumentNullException("objectCommander");

//            if (entityQueries == null)
//                throw new ArgumentNullException("entityQueries");

//            _commander = objectCommander;
//            _entityQueries = entityQueries;
//        }

//        public int CreateOrModify(IPrincipal principal, InstitutionalAgreement scalars, Guid? umbrellaEntityId,
//            IEnumerable<Guid> removedParticipantEstablishmentIds, IEnumerable<Guid> addedParticipantEstablishmentIds,
//            IEnumerable<Guid> removedContactEntityIds, IEnumerable<InstitutionalAgreementContact>  addedContacts,
//            IEnumerable<Guid> removedFileEntityIds, IEnumerable<Guid> addedFileEntityIds
//        )
//        {
//            if (principal == null) throw new ArgumentNullException("principal");

//            var changes = 1;
//            var entity = scalars;
//            var finder = new InstitutionalAgreementFinder(_entityQueries);
//            if (entity.RevisionId != 0)
//            {
//                entity = finder.FindOne(
//                    By<InstitutionalAgreement>.RevisionId(entity.RevisionId)
//                        .EagerLoad(e => e.Umbrella)
//                        //.EagerLoad(e => e.Participants.Select(p => p.Establishment))
//                        .ForInsertOrUpdate()
//                );
//                changes = CopyScalars(scalars, entity);
//            }
//            else if (entity.EntityId == Guid.Empty) entity.EntityId = Guid.NewGuid();

//            // scenario 1: no previous umbrella, no current umbrella.
//            // scenario 2: no previous umbrella, with current umbrella.
//            // scenario 3: with previous umbrella, same current umbrella.
//            // scenario 4: with previous umbrella, different current umbrella.
//            // scenario 5: with previous umbrella, no current umbrella.
//            var previousUmbrella = entity.Umbrella;
//            if (umbrellaEntityId.HasValue && 
//                (previousUmbrella == null || previousUmbrella.EntityId != umbrellaEntityId.Value))
//            {
//                entity.Umbrella = finder.FindOne(By<InstitutionalAgreement>.EntityId
//                    (umbrellaEntityId.Value).ForInsertOrUpdate());
//                ++changes;
//            }
//            else if (previousUmbrella != null && !umbrellaEntityId.HasValue)
//            {
//                entity.Umbrella = null;
//                ++changes;
//            }

//            // ReSharper disable LoopCanBeConvertedToQuery

//            if (removedParticipantEstablishmentIds != null)
//                foreach (var removedParticipantEstablishmentId in removedParticipantEstablishmentIds)
//                    changes += entity.RemoveParticipant(removedParticipantEstablishmentId, _commander);

//            if (addedParticipantEstablishmentIds != null)
//                foreach (var addedParticipantEstablishmentId in addedParticipantEstablishmentIds)
//                    changes += entity.AddParticipant(principal, addedParticipantEstablishmentId, 
//                        new EstablishmentFinder(_entityQueries));

//            if (removedContactEntityIds != null)
//                foreach (var removedContactEntityId in removedContactEntityIds.Where(v => v != Guid.Empty))
//                    changes += entity.RemoveContact(removedContactEntityId, _commander);

//            if (addedContacts != null)
//                foreach (var addedContact in addedContacts)
//                    changes += entity.AddContact(addedContact,
//                        new PersonFinder(_entityQueries));

//            if (removedFileEntityIds != null)
//                foreach (var removedFileEntityId in removedFileEntityIds)
//                    changes += entity.RemoveFile(removedFileEntityId, _commander);

//            if (addedFileEntityIds != null)
//                foreach (var addedFileEntityId in addedFileEntityIds)
//                    changes += entity.AddFile(addedFileEntityId,
//                        new FileFactory(_commander, _entityQueries));
            
//            // ReSharper restore LoopCanBeConvertedToQuery

//            if (changes > 0)
//            {
//                if (entity.RevisionId == 0) _commander.Insert(entity);
//                else _commander.Update(entity);
//                DeriveNodes(entity, previousUmbrella);
//                _commander.SaveChanges();
//            }

//            return changes;
//        }

//        // when creating a new agreement, there will be no children.
//        // however, there may be an umbrella and other ancestors.
//        // each ancestor must have its offspring and children updated.
//        // when changing an existing agreement, there may be ancestors and offspring.
//        // however, they only change when the agreement's umbrella has changed.
//        // each ancestor in the old tree must have its offspring and children updated.
//        // each ancestor and offspring in the new tree must have its offspring and ancestors updated.
//        public void DeriveNodes()
//        {
//            var finder = new InstitutionalAgreementFinder(_entityQueries);
//            var agreements = finder.FindMany(InstitutionalAgreementsWith.NoUmbrellaButWithChildren()
//                .EagerLoad(e => e.Offspring.Select(o => o.Ancestor.Umbrella))
//                .EagerLoad(e => e.Offspring.Select(o => o.Offspring.Umbrella))
//                .EagerLoad(e => e.Offspring.Select(o => o.Ancestor.Children))
//                .EagerLoad(e => e.Offspring.Select(o => o.Offspring.Children))
//                .EagerLoad(e => e.Children.Select(c => c.Children.Select(g => g.Children)))
//                .EagerLoad(e => e.Children.Select(c => c.Ancestors.Select(a => a.Ancestor)))
//            );

//            foreach (var umbrella in agreements)
//                DeriveNodes(umbrella);
//        }

//        private void DeriveNodes(InstitutionalAgreement agreement, InstitutionalAgreement previousUmbrella)
//        {
//            DeriveNodes(agreement);
//            if (previousUmbrella != null && 
//                (agreement.Umbrella == null || agreement.Umbrella.EntityId != previousUmbrella.EntityId))
//                DeriveNodes(previousUmbrella);
//        }

//        private void DeriveNodes(InstitutionalAgreement agreement)
//        {
//            if (agreement == null)
//                throw new ArgumentNullException("agreement");

//            var umbrella = agreement;
//            while (umbrella.Umbrella != null)
//                umbrella = umbrella.Umbrella;

//            ClearNodesRecursive(umbrella);
//            BuildNodesRecursive(umbrella);
//        }

//        private void ClearNodesRecursive(InstitutionalAgreement umbrella)
//        {
//            // ensure that the offspring and children properties are not null
//            umbrella.Offspring = umbrella.Offspring ?? new List<InstitutionalAgreementNode>();
//            umbrella.Children = umbrella.Children ?? new List<InstitutionalAgreement>();

//            // delete all of this umbrella's offspring nodes
//            while (umbrella.Offspring.FirstOrDefault() != null)
//                _commander.Delete(umbrella.Offspring.FirstOrDefault());

//            // operate recursively over children
//            foreach (var child in umbrella.Children.Current())
//            {
//                // ensure that the child's ancestor nodes are not null
//                child.Ancestors = child.Ancestors ?? new List<InstitutionalAgreementNode>();

//                // delete each of the child's ancestor nodes
//                while (child.Ancestors.FirstOrDefault() != null)
//                    _commander.Delete(child.Ancestors.First());

//                // run this method again on the child
//                ClearNodesRecursive(child);
//            }
//        }

//        private static void BuildNodesRecursive(InstitutionalAgreement umbrella)
//        {
//            // operate recursively over children
//            foreach (var child in umbrella.Children.Current())
//            {
//                // create & add ancestor node for this child
//                var node = new InstitutionalAgreementNode
//                {
//                    Ancestor = umbrella,
//                    Offspring = child,
//                    Separation = 1,
//                };
//                child.Ancestors.Add(node);

//                // ensure the umbrella's ancestors nodes are not null
//                umbrella.Ancestors = umbrella.Ancestors ?? new List<InstitutionalAgreementNode>();

//                // loop over the umbrella's ancestors
//                foreach (var ancestor in umbrella.Ancestors)
//                {
//                    // create & add ancestor node for this child
//                    node = new InstitutionalAgreementNode
//                    {
//                        Ancestor = ancestor.Ancestor,
//                        Offspring = child,
//                        Separation = ancestor.Separation + 1,
//                    };
//                    child.Ancestors.Add(node);
//                }

//                // run this method again on the child
//                BuildNodesRecursive(child);
//            }
//        }

//        private static int CopyScalars(InstitutionalAgreement scalars, InstitutionalAgreement entity)
//        {
//            var changes = 0;

//            if (scalars.Title != entity.Title) ++changes;
//            if (scalars.IsTitleDerived != entity.IsTitleDerived) ++changes;
//            if (scalars.Type != entity.Type) ++changes;
//            if (scalars.Status != entity.Status) ++changes;
//            if (scalars.Description != entity.Description) ++changes;
//            if (scalars.IsAutoRenew != entity.IsAutoRenew) ++changes;
//            if (scalars.StartsOn != entity.StartsOn) ++changes;
//            if (scalars.ExpiresOn != entity.ExpiresOn) ++changes;
//            if (scalars.IsExpirationEstimated != entity.IsExpirationEstimated) ++changes;
//            if (scalars.Visibility != entity.Visibility) ++changes;

//            entity.Title = scalars.Title;
//            entity.IsTitleDerived = scalars.IsTitleDerived;
//            entity.Type = scalars.Type;
//            entity.Status = scalars.Status;
//            entity.Description = scalars.Description;
//            entity.IsAutoRenew = scalars.IsAutoRenew;
//            entity.StartsOn = scalars.StartsOn;
//            entity.ExpiresOn = scalars.ExpiresOn;
//            entity.IsExpirationEstimated = scalars.IsExpirationEstimated;
//            entity.Visibility = scalars.Visibility;

//            return changes;
//        }
//    }
//}
