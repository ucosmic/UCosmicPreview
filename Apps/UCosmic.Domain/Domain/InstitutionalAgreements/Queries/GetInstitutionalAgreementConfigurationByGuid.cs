using System;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class GetInstitutionalAgreementConfigurationByGuidQuery :
        BaseEntityQuery<InstitutionalAgreementConfiguration>,
        IDefineQuery<InstitutionalAgreementConfiguration>
    {
        public GetInstitutionalAgreementConfigurationByGuidQuery(Guid guid)
        {
            if (guid == Guid.Empty) throw new ArgumentException("Cannot be empty.", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class GetInstitutionalAgreementConfigurationByGuidHandler :
        IHandleQueries<GetInstitutionalAgreementConfigurationByGuidQuery,
        InstitutionalAgreementConfiguration>
    {
        private readonly IQueryEntities _entities;

        public GetInstitutionalAgreementConfigurationByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreementConfiguration Handle(GetInstitutionalAgreementConfigurationByGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Get<InstitutionalAgreementConfiguration>()
                .EagerLoad(query.EagerLoad, _entities)
                .By(query.Guid)
            ;
        }
    }
}
