using System;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentByGuid : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment>
    {
        public EstablishmentByGuid(Guid guid)
        {
            if (guid == Guid.Empty) 
                throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class HandleEstablishmentByGuid : IHandleQueries<EstablishmentByGuid, Establishment>
    {
        private readonly IQueryEntities _entities;

        public HandleEstablishmentByGuid(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(EstablishmentByGuid query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<Establishment>()
                .EagerLoad(query.EagerLoad, _entities)
                .By(query.Guid)
            ;
        }
    }
}
