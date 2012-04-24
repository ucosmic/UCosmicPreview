using System;

namespace UCosmic.Domain.People
{
    public class GetPersonByIdHandler : IHandleQueries<GetPersonByIdQuery, Person>
    {
        private readonly IQueryEntities _entities;

        public GetPersonByIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Person Handle(GetPersonByIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var queryable = _entities.People;
            if (query.WithoutUnitOfWork)
                queryable = queryable.WithoutUnitOfWork(_entities);

            return queryable
                .EagerLoad(query.EagerLoad, _entities)
                .ByRevisionId(query.Id)
            ;
        }
    }
}
