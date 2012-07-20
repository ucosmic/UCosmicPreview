using System.Collections.Generic;
using System.Collections.ObjectModel;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementConfiguration : RevisableEntity
    {
        public InstitutionalAgreementConfiguration()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            AllowedTypeValues = new Collection<InstitutionalAgreementTypeValue>();
            AllowedStatusValues = new Collection<InstitutionalAgreementStatusValue>();
            AllowedContactTypeValues = new Collection<InstitutionalAgreementContactTypeValue>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int? ForEstablishmentId { get; set; }
        public virtual Establishment ForEstablishment { get; set; }

        public bool IsCustomTypeAllowed { get; set; }
        public bool IsCustomStatusAllowed { get; set; }
        public bool IsCustomContactTypeAllowed { get; set; }

        public virtual ICollection<InstitutionalAgreementTypeValue> AllowedTypeValues { get; set; }
        public virtual ICollection<InstitutionalAgreementStatusValue> AllowedStatusValues { get; set; }
        public virtual ICollection<InstitutionalAgreementContactTypeValue> AllowedContactTypeValues { get; set; }

    }

}