namespace UCosmic.Domain.Identity
{
    public class GetUserByNameQuery : BaseEntityQuery<User>, IDefineQuery<User>
    {
        public string Name { get; set; }
    }

    public class GetUserByNameHandler : IHandleQueries<GetUserByNameQuery, User>
    {
        private readonly ICommandEntities _entities;

        public GetUserByNameHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByNameQuery query)
        {
            return _entities.Get<User>()
                .EagerLoad(_entities, query.EagerLoad)
                .ByName(query.Name)
            ;
        }
    }
}
