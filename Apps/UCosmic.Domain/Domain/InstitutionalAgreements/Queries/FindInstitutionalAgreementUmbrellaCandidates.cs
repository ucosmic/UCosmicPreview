using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindInstitutionalAgreementUmbrellaCandidatesQuery : BaseEntitiesQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement[]>
    {
        public FindInstitutionalAgreementUmbrellaCandidatesQuery(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
        public int ForInstitutionalAgreementRevisionId { get; set; }
    }

    public class FindInstitutionalAgreementUmbrellaCandidatesHandler : IHandleQueries<FindInstitutionalAgreementUmbrellaCandidatesQuery, InstitutionalAgreement[]>
    {
        private readonly IQueryEntities _entities;

        public FindInstitutionalAgreementUmbrellaCandidatesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement[] Handle(FindInstitutionalAgreementUmbrellaCandidatesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var queryable = _entities.InstitutionalAgreements
                .EagerLoad(query.EagerLoad, _entities)
                .ForTenantUser(query.Principal)
                .UmbrellaCandidatesFor(query.ForInstitutionalAgreementRevisionId)
                .OrderBy(query.OrderBy);

            return queryable.ToArray();
        }
    }
}
