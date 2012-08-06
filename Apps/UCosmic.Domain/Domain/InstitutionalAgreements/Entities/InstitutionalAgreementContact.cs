using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementContact : RevisableEntity
    {
        protected internal InstitutionalAgreementContact()
        {
        }

        public string Type { get; protected internal set; }

        public virtual InstitutionalAgreement Agreement { get; protected internal set; }

        public virtual Person Person { get; protected internal set; }
    }
}