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
            var setup = _entities.EagerLoad(_entities.InstitutionalAgreements,
                a => a.Participants.Select(p => p.Establishment.Names),
                a => a.Participants.Select(p => p.Establishment.Location));

            var results = setup.OwnedBy(query.EstablishmentId)
                .MatchingPlaceParticipantOrContact(query.Keyword)
                .OrderByDescending(a => a.StartsOn);

            return results.ToArray();
        }
    }
}
