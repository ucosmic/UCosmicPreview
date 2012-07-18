using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class FindRootEstablishmentsWithChildrenQuery : BaseEntitiesQuery<Establishment>, IDefineQuery<ICollection<Establishment>>
    {
    }

    public class FindRootEstablishmentsWithChildrenHandler : IHandleQueries<FindRootEstablishmentsWithChildrenQuery, ICollection<Establishment>>
    {
        private readonly IQueryEntities _entities;

        public FindRootEstablishmentsWithChildrenHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public ICollection<Establishment> Handle(FindRootEstablishmentsWithChildrenQuery query)
        {
            return _entities.Establishments
                .EagerLoad(query.EagerLoad, _entities)
                .IsRoot()
                .WithAnyChildren()
                .OrderBy(query.OrderBy)
                .ToList()
            ;
        }
    }
}
