using System;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class GetMyAffiliationByEstablishmentIdQuery : IDefineQuery<Affiliation>
    {
        public IPrincipal Principal { get; set; }
        public int EstablishmentId { get; set; }
    }

    public class GetMyAffiliationByEstablishmentIdHandler : IHandleQueries<GetMyAffiliationByEstablishmentIdQuery, Affiliation>
    {
        private readonly IQueryEntities _entities;

        public GetMyAffiliationByEstablishmentIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Affiliation Handle(GetMyAffiliationByEstablishmentIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<Affiliation>()
                .EagerLoad(new Expression<Func<Affiliation, object>>[]
                {
                    a => a.Person,
                    a => a.Establishment,
                }, _entities)
                .ByUserNameAndEstablishmentId(query.Principal.Identity.Name, query.EstablishmentId);
        }
    }
}
