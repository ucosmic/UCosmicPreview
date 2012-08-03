using System;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class GetInstitutionalAgreementByGuidQuery : BaseEntityQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement>
    {
        public GetInstitutionalAgreementByGuidQuery(Guid guid)
        {
            if (guid == Guid.Empty) throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class GetInstitutionalAgreementByGuidHandler : IHandleQueries<GetInstitutionalAgreementByGuidQuery, InstitutionalAgreement>
    {
        private readonly IQueryEntities _entities;

        public GetInstitutionalAgreementByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement Handle(GetInstitutionalAgreementByGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<InstitutionalAgreement>()
                .EagerLoad(_entities, query.EagerLoad)
                .By(query.Guid)
            ;
        }
    }
}
