using System;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class FindRolesGrantedToUserNameQuery : BaseEntitiesQuery<Role>, IDefineQuery<Role[]>
    {
        public string UserName { get; set; }
    }

    public class FindRolesGrantedToUserNameHandler : IHandleQueries<FindRolesGrantedToUserNameQuery, Role[]>
    {
        private readonly IQueryEntities _entities;

        public FindRolesGrantedToUserNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Role[] Handle(FindRolesGrantedToUserNameQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Roles
                .EagerLoad(query.EagerLoad, _entities)
                .GrantedTo(query.UserName)
                .ToArray()
            ;
        }
    }
}
