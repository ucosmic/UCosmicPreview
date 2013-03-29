using System;
using System.Collections.Generic;

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

        public override string ToString()
        {
            return string.Format("RevisionId {0}: {1}", RevisionId, Title);
        }
    }

}