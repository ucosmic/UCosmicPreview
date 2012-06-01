using System;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class GetMyPersonQuery : BasePersonQuery, IDefineQuery<Person>
    {
        public IPrincipal Principal { get; set; }
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

            return _entities.People
                .EagerLoad(query.EagerLoad, _entities)
                .ByUserName(query.Principal.Identity.Name)
            ;
        }
    }
}
