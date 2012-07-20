namespace UCosmic.Domain.Identity
{
    public class GetUserByEduPersonTargetedIdQuery : BaseEntityQuery<User>, IDefineQuery<User>
    {
        public string EduPersonTargetedId { get; set; }
    }

    public class GetUserByEduPersonTargetedIdHandler : IHandleQueries<GetUserByEduPersonTargetedIdQuery, User>
    {
        private readonly IQueryEntities _entities;

        public GetUserByEduPersonTargetedIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByEduPersonTargetedIdQuery query)
        {
            return _entities.Get<User>()
                .ByEduPersonTargetedId(query.EduPersonTargetedId)
            ;
        }
    }
}
