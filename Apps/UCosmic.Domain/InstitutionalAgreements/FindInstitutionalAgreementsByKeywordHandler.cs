using System.Linq;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindInstitutionalAgreementsByKeywordHandler : IHandleQueries<FindInstitutionalAgreementsByKeywordQuery, InstitutionalAgreement[]>
    {
        private readonly IQueryEntities _entities;

        public FindInstitutionalAgreementsByKeywordHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement[] Handle(FindInstitutionalAgreementsByKeywordQuery query)
        {
            var queryable = _entities.InstitutionalAgreements
                .EagerLoad(query.EagerLoad, _entities)
                .OwnedBy(query.EstablishmentId)
                .MatchingPlaceParticipantOrContact(query.Keyword)
                .OrderBy(query.OrderBy);

            return queryable.ToArray();
        }

    }
}
