using System;
using System.Linq;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindInstitutionalAgreementsByKeywordQuery : BaseEntitiesQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement[]>
    {
        public string Keyword { get; set; }
        public int EstablishmentId { get; set; }
    }

    public class FindInstitutionalAgreementsByKeywordHandler : IHandleQueries<FindInstitutionalAgreementsByKeywordQuery, InstitutionalAgreement[]>
    {
        private readonly IQueryEntities _entities;

        public FindInstitutionalAgreementsByKeywordHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement[] Handle(FindInstitutionalAgreementsByKeywordQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var queryable = _entities.Query<InstitutionalAgreement>()
                .EagerLoad(_entities, query.EagerLoad)
                .OwnedByEstablishment(query.EstablishmentId)
                .MatchingPlaceParticipantOrContact(query.Keyword)
                .OrderBy(query.OrderBy);

            return queryable.ToArray();
        }

    }
}
