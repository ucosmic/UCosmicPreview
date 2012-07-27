using System;
using System.Linq;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindEstablishmentsWithInstitutionalAgreementsQuery
        : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment[]>
    {
        public FindEstablishmentsWithInstitutionalAgreementsQuery(Guid? parentEstablishmentGuid = null)
        {
            ParentEstablishmentGuid = parentEstablishmentGuid;
        }

        public Guid? ParentEstablishmentGuid { get; private set; }
    }

    public class FindEstablishmentsWithInstitutionalAgreementsHandler
        : IHandleQueries<FindEstablishmentsWithInstitutionalAgreementsQuery, Establishment[]>
    {
        private readonly IQueryEntities _entities;

        public FindEstablishmentsWithInstitutionalAgreementsHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment[] Handle(FindEstablishmentsWithInstitutionalAgreementsQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var queryable = _entities.Query<InstitutionalAgreementParticipant>()
                .Where(p => p.IsOwner)
                .Select(p => p.Establishment)
                .Distinct()
                .EagerLoad(query.EagerLoad, _entities)
            ;
            if (query.ParentEstablishmentGuid.HasValue)
            {
                queryable = queryable
                    .Where
                    (
                        e =>
                        e.Parent != null &&
                        e.Parent.EntityId == query.ParentEstablishmentGuid
                    )
                ;
            }
            else
            {
                var closedQueryable = queryable;
                queryable = queryable
                    .Where
                    (
                        e =>
                        e.Parent == null ||
                        !closedQueryable.Select(p1 => p1.RevisionId).Contains(e.RevisionId)
                    )
                ;
            }

            queryable = queryable.OrderBy(query.OrderBy);

            return queryable.ToArray();
        }

    }
}
