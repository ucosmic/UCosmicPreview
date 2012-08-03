using System;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByIdQuery : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment>
    {
        public GetEstablishmentByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class GetEstablishmentByIdHandler : IHandleQueries<GetEstablishmentByIdQuery, Establishment>
    {
        private readonly IQueryEntities _entities;

        public GetEstablishmentByIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(GetEstablishmentByIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<Establishment>()
                .EagerLoad(_entities, query.EagerLoad)
                .By(query.Id)
            ;
        }
    }
}
