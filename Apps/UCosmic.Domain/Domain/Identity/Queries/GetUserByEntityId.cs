using System;

namespace UCosmic.Domain.Identity
{
    public class GetUserByEntityIdQuery : BaseEntityQuery<User>, IDefineQuery<User>
    {
        public Guid EntityId { get; set; }
    }

    public class GetUserByEntityIdHandler : IHandleQueries<GetUserByEntityIdQuery, User>
    {
        private readonly IQueryEntities _entities;

        public GetUserByEntityIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByEntityIdQuery query)
        {
            return _entities.Get<User>()
                .By(query.EntityId)
            ;
        }
    }
}
