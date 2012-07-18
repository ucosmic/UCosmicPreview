using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class GetMyAffiliationByEstablishmentIdQuery : IDefineQuery<Affiliation>
    {
        public IPrincipal Principal { get; set; }
        public int EstablishmentId { get; set; }
    }

    public class GetMyAffiliationByEstablishmentIdHandler : IHandleQueries<GetMyAffiliationByEstablishmentIdQuery, Affiliation>
    {
        private readonly IProcessQueries _queryProcessor;

        public GetMyAffiliationByEstablishmentIdHandler(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Affiliation Handle(GetMyAffiliationByEstablishmentIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var user = _queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = query.Principal.Identity.Name,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.Person.Affiliations.Select(a => a.Establishment),
                    },
                }
            );

            if (user == null) return null;

            var affiliation = user.Person.GetAffiliation(query.EstablishmentId);

            return affiliation;
        }
    }
}
