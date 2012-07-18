using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Files;
using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreement : RevisableEntity
    {
        public InstitutionalAgreement()
        {
            _participants = new List<InstitutionalAgreementParticipant>();
            _contacts = new List<InstitutionalAgreementContact>();
            _files = new List<InstitutionalAgreementFile>();
            _children = new List<InstitutionalAgreement>();
            _ancestors = new List<InstitutionalAgreementNode>();
            _offspring = new List<InstitutionalAgreementNode>();
            Visibility = InstitutionalAgreementVisibility.Public;
        }

        public bool IsTitleDerived { get; set; }

        public virtual InstitutionalAgreement Umbrella { get; set; }

        private ICollection<InstitutionalAgreementNode> _ancestors;
        public virtual ICollection<InstitutionalAgreementNode> Ancestors
        {
            get { return _ancestors; }
            set { _ancestors = value; }
        }

        private ICollection<InstitutionalAgreement> _children;
        public virtual ICollection<InstitutionalAgreement> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        private ICollection<InstitutionalAgreementNode> _offspring;
        public virtual ICollection<InstitutionalAgreementNode> Offspring
        {
            get { return _offspring; }
            set { _offspring = value; }
        }

        public string Title { get; set; }

        public string Type { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        [DefaultValue(false)]
        public bool IsExpirationEstimated { get; set; }

        public string Description { get; set; }

        public bool? IsAutoRenew { get; set; }

        public string Status { get; set; }

        private ICollection<InstitutionalAgreementParticipant> _participants;
        public virtual ICollection<InstitutionalAgreementParticipant> Participants
        {
            get { return _participants; }
            set { _participants = value; }
        }
        internal int RemoveParticipant(Guid establishmentEntityId, ICommandObjects commander)
        {
            var participant = Participants.SingleOrDefault(g => g.Establishment.EntityId == establishmentEntityId);
            return (participant != null) ? participant.Remove(commander) : 0;
        }
        internal int AddParticipant(IPrincipal principal, Guid establishmentEntityId, EstablishmentFinder establishmentFinder)
        {
            var participant = Participants.SingleOrDefault(g => g.Establishment.EntityId == establishmentEntityId);
            if (participant != null) return 0;

            var establishment = establishmentFinder.FindOne(By<Establishment>.EntityId(establishmentEntityId)
                .EagerLoad(e => e.Affiliates.Select(a => a.Person.User))
                .EagerLoad(e => e.Names.Select(n => n.TranslationToLanguage))
                .EagerLoad(e => e.Ancestors.Select(h => h.Ancestor.Affiliates.Select(a => a.Person.User)))
                .EagerLoad(e => e.Ancestors.Select(h => h.Ancestor.Names.Select(n => n.TranslationToLanguage)))
                .ForInsertOrUpdate()
            );

            // for establishment to be an owning participant, the principal must be affiliated with
            // the establishment or one of the establishment's ancestors.
            participant = new InstitutionalAgreementParticipant
            {
                Establishment = establishment,
            };
            participant.DeriveIsOwner(principal);
            Participants.Add(participant);
            return 1;
        }

        private ICollection<InstitutionalAgreementContact> _contacts;
        public virtual ICollection<InstitutionalAgreementContact> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }
        internal int RemoveContact(Guid contactEntityId, ICommandObjects commander)
        {
            if (contactEntityId == Guid.Empty) return 0;
            var contact = Contacts.SingleOrDefault(g => g.EntityId == contactEntityId);
            return (contact != null) ? contact.Remove(commander) : 0;
        }
        internal int AddContact(InstitutionalAgreementContact contact, PersonFinder personFinder)
        {
            if (contact.EntityId != Guid.Empty)
            {
                var entity = Contacts.SingleOrDefault(e => e.EntityId == contact.EntityId);
                if (entity != null) return 0;
            }

            if (contact.Person.EntityId != Guid.Empty)
                contact.Person = personFinder.FindOne(By<Person>.EntityId(contact.Person.EntityId)
                    .ForInsertOrUpdate());
            contact.EntityId = Guid.NewGuid();
            Contacts.Add(contact);
            return 1;
        }

        private ICollection<InstitutionalAgreementFile> _files;
        public virtual ICollection<InstitutionalAgreementFile> Files
        {
            get { return _files; }
            set { _files = value; }
        }
        internal int RemoveFile(Guid fileEntityId, ICommandObjects commander)
        {
            if (fileEntityId == Guid.Empty) return 0;
            var file = Files.SingleOrDefault(g => g.EntityId == fileEntityId);
            return (file != null) ? file.Remove(commander) : 0;
        }
        internal int AddFile(Guid fileEntityId, FileFactory fileFactory)
        {
            var file = Files.SingleOrDefault(g => g.EntityId == fileEntityId);
            if (file != null) return 0;

            var looseFile = fileFactory.FindOne(By<LooseFile>.EntityId(fileEntityId));
            if (looseFile == null) return 0;

            // for establishment to be an owning participant, the principal must be affiliated with
            // the establishment or one of the establishment's ancestors.
            file = new InstitutionalAgreementFile
            {
                Content = looseFile.Content,
                Length = looseFile.Length,
                MimeType = looseFile.MimeType,
                Name = looseFile.Name,
            };
            Files.Add(file);
            fileFactory.Purge(looseFile.EntityId);
            return 1;
        }

        public string VisibilityText { get; protected set; }
        public InstitutionalAgreementVisibility Visibility
        {
            get { return VisibilityText.AsEnum<InstitutionalAgreementVisibility>(); }
            protected internal set { VisibilityText = value.AsSentenceFragment(); }
        }

        public string DeriveTitle()
        {
            if (!IsTitleDerived) return Title;
            var title = new StringBuilder();

            title.Append(string.Format("{0} between ", Type ?? "Institutional Agreement"));
            if (Participants != null && Participants.Count > 0)
            {
                var participants = Participants.OrderByDescending(p => p.IsOwner).ToList();
                foreach (var participant in participants)
                {
                    if (participants.Count > 1 && participant == participants.Last())
                    {
                        title.Append(string.Format("and {0} ", participant.Establishment.TranslatedName));
                    }
                    else if (participants.Count == 1)
                    {
                        title.Append(string.Format("{0} and... ", participant.Establishment.TranslatedName));
                    }
                    else if (participants.Count <= 2)
                    {
                        title.Append(string.Format("{0} ", participant.Establishment.TranslatedName));
                    }
                    else
                    {
                        title.Append(string.Format("{0}, ", participant.Establishment.TranslatedName));
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(Status))
            {
                title.Append(string.Format("- Status is {0} ", Status));
            }

            return title.ToString().Trim();
        }

        public override string ToString()
        {
            return string.Format("RevisionId {0}: {1}", RevisionId, Title);
        }
    }

}