namespace UCosmic.Domain.Identity
{
    public class GetUserByEntityIdHandler : IHandleQueries<GetUserByEntityIdQuery, User>
    {
        private readonly IQueryEntities _entities;

        public GetUserByEntityIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByEntityIdQuery query)
        {
            return _entities.Users
                .By(query.EntityId)
            ;
        }
    }
}
