using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreement : RevisableEntity
    {
        public InstitutionalAgreement()
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

        public bool IsTitleDerived { get; set; }

        public virtual InstitutionalAgreement Umbrella { get; set; }

        public virtual ICollection<InstitutionalAgreementNode> Ancestors { get; set; }

        public virtual ICollection<InstitutionalAgreement> Children { get; set; }

        public virtual ICollection<InstitutionalAgreementNode> Offspring { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        [DefaultValue(false)]
        public bool IsExpirationEstimated { get; set; }

        public string Description { get; set; }

        public bool? IsAutoRenew { get; set; }

        public string Status { get; set; }

        public virtual ICollection<InstitutionalAgreementParticipant> Participants { get; set; }

        public virtual ICollection<InstitutionalAgreementContact> Contacts { get; set; }

        public virtual ICollection<InstitutionalAgreementFile> Files { get; set; }

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