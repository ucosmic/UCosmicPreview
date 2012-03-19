namespace UCosmic.Domain.InstitutionalAgreements
{
    public abstract class InstitutionalAgreementFieldValue
    {
        public int Id { get; set; }

        public int ConfigurationId { get; set; }
        public virtual InstitutionalAgreementConfiguration Configuration { get; set; }
        public string Text { get; set; }
    }

    public class InstitutionalAgreementTypeValue : InstitutionalAgreementFieldValue
    {
    }

    public class InstitutionalAgreementStatusValue : InstitutionalAgreementFieldValue
    {
    }

    public class InstitutionalAgreementContactTypeValue : InstitutionalAgreementFieldValue
    {
    }

}