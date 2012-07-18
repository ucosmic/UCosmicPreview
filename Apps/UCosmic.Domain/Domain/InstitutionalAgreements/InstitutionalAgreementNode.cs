namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementNode : Entity
    {
        public int AncestorId { get; set; }
        public virtual InstitutionalAgreement Ancestor { get; set; }

        public int OffspringId { get; set; }
        public virtual InstitutionalAgreement Offspring { get; set; }

        public int Separation { get; set; }
    }
}