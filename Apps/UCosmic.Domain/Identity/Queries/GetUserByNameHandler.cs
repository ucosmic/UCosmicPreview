namespace UCosmic.Domain.Identity
{
    public class GetUserByNameHandler : IHandleQueries<GetUserByNameQuery, User>
    {
        private readonly IQueryEntities _entities;

        public GetUserByNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByNameQuery query)
        {
            return _entities.Users
                .EagerLoad(query.EagerLoad, _entities)
                .ByName(query.Name)
            ;
        }
    }
}
