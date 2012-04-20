using System;

namespace UCosmic.Domain.People
{
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
                .ByEmail(query.Email)
            ;
        }
    }
}
