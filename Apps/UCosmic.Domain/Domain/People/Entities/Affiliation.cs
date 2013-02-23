using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    public class Affiliation : Entity
    {
        private bool _isDefault;

        protected internal Affiliation()
        {
        }

        public int PersonId { get; protected internal set; }
        public virtual Person Person { get; protected internal set; }

        public int EstablishmentId { get; protected internal set; }
        public virtual Establishment Establishment { get; protected internal set; }

        public string JobTitles { get; protected internal set; }

        public bool IsDefault
        {
            get { return _isDefault; }
            protected internal set
            {
                _isDefault = value;
                IsPrimary = value;
            }
        }

        public bool IsPrimary { get; private set; }

        public bool IsAcknowledged { get; protected internal set; }
        public bool IsClaimingStudent { get; protected internal set; }
        public bool IsClaimingEmployee { get; protected internal set; }

        public bool IsClaimingInternationalOffice { get; protected internal set; }
        public bool IsClaimingAdministrator { get; protected internal set; }
        public bool IsClaimingFaculty { get; protected internal set; }
        public bool IsClaimingStaff { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Person.DisplayName, Establishment.OfficialName);
        }
    }
}