using System;

namespace UCosmic.Domain.People
{
    public class GetPersonByEmailQuery : BasePersonQuery, IDefineQuery<Person>
    {
        public string Email { get; set; }
    }

    public class GetPersonByEmailHandler : IHandleQueries<GetPersonByEmailQuery, Person>
    {
        private readonly IQueryEntities _entities;

        public GetPersonByEmailHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Person Handle(GetPersonByEmailQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.People
                .EagerLoad(query.EagerLoad, _entities)
                .ByEmail(query.Email)
            ;
        }
    }
}
