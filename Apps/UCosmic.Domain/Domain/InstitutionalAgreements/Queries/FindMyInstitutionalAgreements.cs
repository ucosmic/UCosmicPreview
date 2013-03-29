using System;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly ICommandEntities _commandEntities;
        private readonly IStoreBinaryData _binaryData;
        private readonly IUnitOfWork _unitOfWork;

        public FindMyInstitutionalAgreementsHandler(IQueryEntities entities
            , ICommandEntities commandEntities
            , IStoreBinaryData binaryData
            , IUnitOfWork unitOfWork
        )
        {
            _entities = entities;
            _commandEntities = commandEntities;
            _binaryData = binaryData;
            _unitOfWork = unitOfWork;
        }

        public InstitutionalAgreement[] Handle(FindMyInstitutionalAgreementsQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // sneak in and copy file data to alternate storage
            var entities = _commandEntities.Get<InstitutionalAgreement>()
                .EagerLoad(_commandEntities, new Expression<Func<InstitutionalAgreement, object>>[]
                {
                    x => x.Files,
                })
                .ForTenantUser(query.Principal)
                .OrderBy(query.OrderBy)
                .ToArray();
            foreach (var entity in entities)
            {
                entity.CompleteMoveFiles(_binaryData, _unitOfWork);
            }

            var queryable = _entities.Query<InstitutionalAgreement>()
                .EagerLoad(_entities, query.EagerLoad)
                .ForTenantUser(query.Principal)
                .OrderBy(query.OrderBy);

            return queryable.ToArray();
        }

    }
}
