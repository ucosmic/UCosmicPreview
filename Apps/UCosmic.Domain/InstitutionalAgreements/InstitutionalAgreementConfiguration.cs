using System.Collections.Generic;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementConfiguration : RevisableEntity
    {
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