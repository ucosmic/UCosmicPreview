using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementContact : RevisableEntity
    {
        public string Type { get; set; }

        public virtual InstitutionalAgreement Agreement { get; set; }

        public virtual Person Person { get; set; }

        internal int Remove(ICommandObjects commander)
        {
            commander.Delete(this);
            return 1;
        }
    }
}