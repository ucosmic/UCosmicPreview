using System;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class FindAllRolesQuery : BaseEntitiesQuery<Role>, IDefineQuery<Role[]>
    {
        public string UserName { get; set; }
    }

    public class FindAllRolesHandler : IHandleQueries<FindAllRolesQuery, Role[]>
    {
        private readonly IQueryEntities _entities;

        public FindAllRolesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Role[] Handle(FindAllRolesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<Role>()
                .EagerLoad(_entities, query.EagerLoad)
                .OrderBy(query.OrderBy)
                .ToArray()
            ;
        }
    }
}
