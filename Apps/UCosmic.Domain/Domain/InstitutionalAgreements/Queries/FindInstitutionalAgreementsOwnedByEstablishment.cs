using System;
using System.Linq;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindInstitutionalAgreementsOwnedByEstablishmentQuery 
        : BaseEntitiesQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement[]>
    {
        public FindInstitutionalAgreementsOwnedByEstablishmentQuery(int establishmentIdKey)
        {
            EstablishmentKey = establishmentIdKey;
        }

        public FindInstitutionalAgreementsOwnedByEstablishmentQuery(Guid establishmentGuidKey)
        {
            if (establishmentGuidKey == Guid.Empty) 
                throw new ArgumentException("Cannot be empty.", "establishmentGuidKey");
            EstablishmentKey = establishmentGuidKey;
        }

        public FindInstitutionalAgreementsOwnedByEstablishmentQuery(string establishmentUrlKey)
        {
            if (string.IsNullOrWhiteSpace(establishmentUrlKey)) 
                throw new ArgumentException("Cannot be null or white space.", "establishmentUrlKey");
            EstablishmentKey = establishmentUrlKey;
        }

        public object EstablishmentKey { get; private set; }
    }

    public class FindInstitutionalAgreementsOwnedByEstablishmentHandler 
        : IHandleQueries<FindInstitutionalAgreementsOwnedByEstablishmentQuery, InstitutionalAgreement[]>
    {
        private readonly IQueryEntities _entities;

        public FindInstitutionalAgreementsOwnedByEstablishmentHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement[] Handle(FindInstitutionalAgreementsOwnedByEstablishmentQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var queryable = _entities.Get<InstitutionalAgreement>()
                .EagerLoad(query.EagerLoad, _entities)
                .OwnedByEstablishment(query.EstablishmentKey)
                .OrderBy(query.OrderBy);

            return queryable.ToArray();
        }

    }
}
