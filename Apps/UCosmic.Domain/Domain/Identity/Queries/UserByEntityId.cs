using System;

namespace UCosmic.Domain.Identity
{
    public class UserByEntityId : BaseEntityQuery<User>, IDefineQuery<User>
    {
        public Guid EntityId { get; set; }
    }

    public class HandleUserByEntityId : IHandleQueries<UserByEntityId, User>
    {
        private readonly IQueryEntities _entities;

        public HandleUserByEntityId(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(UserByEntityId query)
        {
            return _entities.Read<User>()
                .By(query.EntityId)
            ;
        }
    }
}
