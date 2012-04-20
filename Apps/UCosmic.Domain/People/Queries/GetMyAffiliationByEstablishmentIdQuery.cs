using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class GetMyAffiliationByEstablishmentIdQuery : IDefineQuery<Affiliation>
    {
        public IPrincipal Principal { get; set; }
        public int EstablishmentId { get; set; }
    }
}
