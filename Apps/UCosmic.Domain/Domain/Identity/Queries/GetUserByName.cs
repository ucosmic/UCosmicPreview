namespace UCosmic.Domain.Identity
{
    public class GetUserByNameQuery : BaseEntityQuery<User>, IDefineQuery<User>
    {
        public string Name { get; set; }
    }

    public class GetUserByNameHandler : IHandleQueries<GetUserByNameQuery, User>
    {
        private readonly IQueryEntities _entities;

        public GetUserByNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByNameQuery query)
        {
            return _entities.Read<User>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByName(query.Name)
            ;
        }
    }
}
