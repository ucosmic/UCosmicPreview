using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class FindEstablishmentsWithGuidsQuery : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment[]>
    {
        public FindEstablishmentsWithGuidsQuery(IEnumerable<Guid> guids)
        {
            if (guids == null) throw new ArgumentNullException("guids");
            Guids = guids;
        }

        public IEnumerable<Guid> Guids { get; private set; }
    }

    public class FindEstablishmentsWithGuidsHandler : IHandleQueries<FindEstablishmentsWithGuidsQuery, Establishment[]>
    {
        private readonly IQueryEntities _entities;

        public FindEstablishmentsWithGuidsHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment[] Handle(FindEstablishmentsWithGuidsQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Query<Establishment>()
                .EagerLoad(_entities, query.EagerLoad)
                .Where(x => query.Guids.Contains(x.EntityId))
                .OrderBy(query.OrderBy);

            return results.ToArray();
        }
    }
}
