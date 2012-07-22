using System;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class GetMyPersonQuery : BaseEntityQuery<Person>, IDefineQuery<Person>
    {
        public GetMyPersonQuery(IPrincipal principal)
        {
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
    }

    public class GetMyPersonHandler : IHandleQueries<GetMyPersonQuery, Person>
    {
        private readonly IQueryEntities _entities;

        public GetMyPersonHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Person Handle(GetMyPersonQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Read<Person>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByUserName(query.Principal.Identity.Name)
            ;
        }
    }
}
