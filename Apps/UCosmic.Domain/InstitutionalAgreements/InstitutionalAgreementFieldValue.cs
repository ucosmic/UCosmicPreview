using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public abstract class InstitutionalAgreementFieldValue
    {
        [Key] // value should not be revisable
        public int Id { get; set; }

        public int ConfigurationId { get; set; }
        public virtual InstitutionalAgreementConfiguration Configuration { get; set; }
    }

    public class InstitutionalAgreementTypeValue : InstitutionalAgreementFieldValue
    {
        [Required]
        [StringLength(150)] // must match length for InstitutionalAgreement.Type
        public string Text { get; set; }
    }

    public class InstitutionalAgreementStatusValue : InstitutionalAgreementFieldValue
    {
        [Required]
        [StringLength(50)] // must match length for InstitutionalAgreement.Status
        public string Text { get; set; }
    }

    public class InstitutionalAgreementContactTypeValue : InstitutionalAgreementFieldValue
    {
        [Required]
        [StringLength(150)] // must match length for InstitutionalAgreementContact.Type
        public string Text { get; set; }
    }

}