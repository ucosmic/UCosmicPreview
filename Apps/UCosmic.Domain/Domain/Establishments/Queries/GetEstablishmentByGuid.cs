using System;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByGuidQuery : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment>
    {
        public GetEstablishmentByGuidQuery(Guid guid)
        {
            if (guid == Guid.Empty) 
                throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class GetEstablishmentByGuidHandler : IHandleQueries<GetEstablishmentByGuidQuery, Establishment>
    {
        private readonly IQueryEntities _entities;

        public GetEstablishmentByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(GetEstablishmentByGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Establishments
                .EagerLoad(query.EagerLoad, _entities)
                .ById(query.Guid)
            ;
        }
    }
}
