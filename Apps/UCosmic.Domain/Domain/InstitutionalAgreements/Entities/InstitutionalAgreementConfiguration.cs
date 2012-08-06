using System.Collections.Generic;
using System.Collections.ObjectModel;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementConfiguration : RevisableEntity
    {
        protected internal InstitutionalAgreementConfiguration()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            AllowedTypeValues = new Collection<InstitutionalAgreementTypeValue>();
            AllowedStatusValues = new Collection<InstitutionalAgreementStatusValue>();
            AllowedContactTypeValues = new Collection<InstitutionalAgreementContactTypeValue>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int? ForEstablishmentId { get; protected internal set; }
        public virtual Establishment ForEstablishment { get; protected internal set; }

        public bool IsCustomTypeAllowed { get; protected internal set; }
        public bool IsCustomStatusAllowed { get; protected internal set; }
        public bool IsCustomContactTypeAllowed { get; protected internal set; }

        public virtual ICollection<InstitutionalAgreementTypeValue> AllowedTypeValues { get; protected set; }
        public virtual ICollection<InstitutionalAgreementStatusValue> AllowedStatusValues { get; protected set; }
        public virtual ICollection<InstitutionalAgreementContactTypeValue> AllowedContactTypeValues { get; protected set; }

    }

}