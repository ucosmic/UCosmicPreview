
namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindInstitutionalAgreementsByKeywordQuery : IDefineQuery<InstitutionalAgreement[]>
    {
        public string Keyword { get; set; }
        public int EstablishmentId { get; set; }
    }
}
