using System;
using System.Linq;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindRootInstitutionalAgreementsWithChildrenQuery : BaseEntitiesQuery<InstitutionalAgreement>, IDefineQuery<InstitutionalAgreement[]>
    {
        public string Keyword { get; set; }
        public int EstablishmentId { get; set; }
    }

    public class FindRootInstitutionalAgreementsWithChildrenHandler : IHandleQueries<FindRootInstitutionalAgreementsWithChildrenQuery, InstitutionalAgreement[]>
    {
        private readonly IQueryEntities _entities;

        public FindRootInstitutionalAgreementsWithChildrenHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public InstitutionalAgreement[] Handle(FindRootInstitutionalAgreementsWithChildrenQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Get<InstitutionalAgreement>()
                .EagerLoad(query.EagerLoad, _entities)
                .IsRoot()
                .WithAnyChildren()
                .OrderBy(query.OrderBy)
                .ToArray()
            ;
        }

    }
}
