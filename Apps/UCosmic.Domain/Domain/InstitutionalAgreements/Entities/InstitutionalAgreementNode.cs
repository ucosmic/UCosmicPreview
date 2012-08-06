namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementNode : Entity
    {
        protected internal InstitutionalAgreementNode()
        {
        }

        public int AncestorId { get; protected internal set; }
        public virtual InstitutionalAgreement Ancestor { get; protected internal set; }

        public int OffspringId { get; protected internal set; }
        public virtual InstitutionalAgreement Offspring { get; protected internal set; }

        public int Separation { get; protected internal set; }
    }
}