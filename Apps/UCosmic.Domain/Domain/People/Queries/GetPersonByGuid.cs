using System;

namespace UCosmic.Domain.People
{
    public class GetPersonByGuidQuery : BaseEntityQuery<Person>, IDefineQuery<Person>
    {
        public GetPersonByGuidQuery(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class GetPersonByGuidHandler : IHandleQueries<GetPersonByGuidQuery, Person>
    {
        private readonly IQueryEntities _entities;

        public GetPersonByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Person Handle(GetPersonByGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.People
                .EagerLoad(query.EagerLoad, _entities)
                .By(query.Guid)
            ;
        }
    }
}
