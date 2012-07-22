using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindMyInstitutionalAgreementsQuery : BaseEntitiesQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement[]>
    {
        public FindMyInstitutionalAgreementsQuery(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
    }

    public class FindMyInstitutionalAgreementsHandler : IHandleQueries<FindMyInstitutionalAgreementsQuery, InstitutionalAgreement[]>
    {
        private readonly IQueryEntities _entities;

        public FindMyInstitutionalAgreementsHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement[] Handle(FindMyInstitutionalAgreementsQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var queryable = _entities.Query<InstitutionalAgreement>()
                .EagerLoad(query.EagerLoad, _entities)
                .ForTenantUser(query.Principal)
                .OrderBy(query.OrderBy);

            return queryable.ToArray();
        }

    }
}
