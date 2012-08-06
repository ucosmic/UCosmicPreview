using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementParticipant : Entity
    {
        protected internal InstitutionalAgreementParticipant()
        {
        }

        public int Id { get; protected internal set; }
        public bool IsOwner { get; protected internal set; }

        public virtual InstitutionalAgreement Agreement { get; protected internal set; }
        public virtual Establishment Establishment { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}",
                IsOwner ? "Owner: " : "Non-Owner: ",
                Establishment.OfficialName);
        }
    }
}