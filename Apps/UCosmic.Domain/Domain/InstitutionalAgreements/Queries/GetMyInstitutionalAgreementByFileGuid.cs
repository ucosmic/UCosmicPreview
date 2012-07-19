using System;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class GetMyInstitutionalAgreementByFileGuidQuery : BaseEntityQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement>
    {
        public GetMyInstitutionalAgreementByFileGuidQuery(IPrincipal principal, Guid guid)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            if (guid == Guid.Empty) throw new ArgumentException("Cannot be empty", "guid");
            Principal = principal;
            Guid = guid;
        }

        public IPrincipal Principal { get; private set; }
        public Guid Guid { get; private set; }
    }

    public class GetMyInstitutionalAgreementByFileGuidHandler : IHandleQueries<GetMyInstitutionalAgreementByFileGuidQuery, InstitutionalAgreement>
    {
        private readonly IQueryEntities _entities;

        public GetMyInstitutionalAgreementByFileGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement Handle(GetMyInstitutionalAgreementByFileGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.InstitutionalAgreements
                .EagerLoad(query.EagerLoad, _entities)
                .ForTenantUser(query.Principal)
                .ByFileGuid(query.Guid)
            ;
        }
    }
}
