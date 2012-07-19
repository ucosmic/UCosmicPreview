using System;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class GetMyInstitutionalAgreementByGuidQuery : BaseEntityQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement>
    {
        public GetMyInstitutionalAgreementByGuidQuery(IPrincipal principal, Guid guid)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            if (guid == Guid.Empty) throw new ArgumentException("Cannot be empty", "guid");
            Principal = principal;
            Guid = guid;
        }

        public IPrincipal Principal { get; private set; }
        public Guid Guid { get; private set; }
    }

    public class GetMyInstitutionalAgreementByGuidHandler : IHandleQueries<GetMyInstitutionalAgreementByGuidQuery, InstitutionalAgreement>
    {
        private readonly IQueryEntities _entities;

        public GetMyInstitutionalAgreementByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement Handle(GetMyInstitutionalAgreementByGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.InstitutionalAgreements
                .EagerLoad(query.EagerLoad, _entities)
                .ForTenantUser(query.Principal)
                .ById(query.Guid)
            ;
        }
    }
}
