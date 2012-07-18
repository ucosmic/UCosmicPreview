using System;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementQuery : RevisableEntityQueryCriteria<InstitutionalAgreement>
    {
        public string OwnedByEstablishmentUrl { get; set; }
        public bool? HasUmbrella { get; set; }
        public bool? HasChildren { get; set; }
        public IPrincipal PrincipalContext { get; set; }
        public Guid? FileEntityId { get; set; }
    }

    public static class InstitutionalAgreementBy
    {
        public static InstitutionalAgreementQuery FileEntityId(Guid fileEntityId)
        {
            return new InstitutionalAgreementQuery { FileEntityId = fileEntityId };
        }
    }

    public static class InstitutionalAgreementsWith
    {
        public static InstitutionalAgreementQuery OwnedByEstablishmentUrl(string establishmentUrl)
        {
            return new InstitutionalAgreementQuery { OwnedByEstablishmentUrl = establishmentUrl };
        }

        public static InstitutionalAgreementQuery NoUmbrellaButWithChildren()
        {
            return new InstitutionalAgreementQuery { HasUmbrella = false, HasChildren = true };
        }

        public static InstitutionalAgreementQuery PrincipalContext(IPrincipal principal)
        {
            return new InstitutionalAgreementQuery { PrincipalContext = principal };
        }
    }
}