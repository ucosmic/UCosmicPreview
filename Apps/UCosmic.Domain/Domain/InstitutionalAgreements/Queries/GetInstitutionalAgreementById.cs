using System;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class GetInstitutionalAgreementByIdQuery : BaseEntityQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement>
    {
        public GetInstitutionalAgreementByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class GetInstitutionalAgreementByIdHandler : IHandleQueries<GetInstitutionalAgreementByIdQuery, InstitutionalAgreement>
    {
        private readonly IQueryEntities _entities;

        public GetInstitutionalAgreementByIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement Handle(GetInstitutionalAgreementByIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.InstitutionalAgreements
                .EagerLoad(query.EagerLoad, _entities)
                .ById(query.Id)
            ;
        }
    }
}
