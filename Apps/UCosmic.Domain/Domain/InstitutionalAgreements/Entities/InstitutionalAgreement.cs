using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreement : RevisableEntity
    {
        protected internal InstitutionalAgreement()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Participants = new List<InstitutionalAgreementParticipant>();
            Contacts = new List<InstitutionalAgreementContact>();
            Files = new List<InstitutionalAgreementFile>();
            Children = new List<InstitutionalAgreement>();
            Ancestors = new List<InstitutionalAgreementNode>();
            Offspring = new List<InstitutionalAgreementNode>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            Visibility = InstitutionalAgreementVisibility.Public;
        }

        public string Title { get; protected internal set; }
        public bool IsTitleDerived { get; protected internal set; }
        public string Description { get; protected internal set; }

        public virtual InstitutionalAgreement Umbrella { get; protected internal set; }
        public virtual ICollection<InstitutionalAgreementNode> Ancestors { get; protected internal set; }
        public virtual ICollection<InstitutionalAgreement> Children { get; protected internal set; }
        public virtual ICollection<InstitutionalAgreementNode> Offspring { get; protected internal set; }

        public string Type { get; protected internal set; }
        public bool? IsAutoRenew { get; protected internal set; }
        public string Status { get; protected internal set; }

        public DateTime StartsOn { get; protected internal set; }
        public DateTime ExpiresOn { get; protected internal set; }
        public bool IsExpirationEstimated { get; protected internal set; }

        public virtual ICollection<InstitutionalAgreementParticipant> Participants { get; protected internal set; }
        public virtual ICollection<InstitutionalAgreementContact> Contacts { get; protected internal set; }
        public virtual ICollection<InstitutionalAgreementFile> Files { get; protected internal set; }

        public string VisibilityText { get; protected set; }
        public InstitutionalAgreementVisibility Visibility
        {
            get { return VisibilityText.AsEnum<InstitutionalAgreementVisibility>(); }
            protected internal set { VisibilityText = value.AsSentenceFragment(); }
        }

        internal void CompleteMoveFiles(IStoreBinaryData binaryData, IUnitOfWork unitOfWork)
        {
            if (Files == null || !Files.Any()) return;

            foreach (var file in Files)
            {
                if (string.IsNullOrWhiteSpace(file.Path))
                    throw new NotSupportedException(string.Format("Institutional agreement file with id '{0}' has no path.", file.RevisionId));
                if (!binaryData.Exists(file.Path))
                    throw new NotSupportedException(string.Format("Institutional agreement file with id '{0}' and path '{1}' is not stored.", file.RevisionId, file.Path));

                if (file.Content == null) continue;

                file.Content = null;
                //var path = string.Format(InstitutionalAgreementFile.PathFormat, RevisionId, Guid.NewGuid());
                //binaryData.Put(path, file.Content);
                //file.Path = path;
                unitOfWork.SaveChanges();
            }
        }

        public override string ToString()
        {
            return string.Format("RevisionId {0}: {1}", RevisionId, Title);
        }
    }

}