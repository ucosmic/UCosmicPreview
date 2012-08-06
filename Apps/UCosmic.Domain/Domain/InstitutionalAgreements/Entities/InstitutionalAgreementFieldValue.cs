namespace UCosmic.Domain.InstitutionalAgreements
{
    public abstract class InstitutionalAgreementFieldValue : Entity
    {
        public int Id { get; protected internal set; }

        public int ConfigurationId { get; protected internal set; }
        public virtual InstitutionalAgreementConfiguration Configuration { get; protected internal set; }
        public string Text { get; protected internal set; }
    }

    public class InstitutionalAgreementTypeValue : InstitutionalAgreementFieldValue
    {
        protected internal InstitutionalAgreementTypeValue()
        {
        }
    }

    public class InstitutionalAgreementStatusValue : InstitutionalAgreementFieldValue
    {
        protected internal InstitutionalAgreementStatusValue()
        {
        }
    }

    public class InstitutionalAgreementContactTypeValue : InstitutionalAgreementFieldValue
    {
        protected internal InstitutionalAgreementContactTypeValue()
        {
        }
    }
}