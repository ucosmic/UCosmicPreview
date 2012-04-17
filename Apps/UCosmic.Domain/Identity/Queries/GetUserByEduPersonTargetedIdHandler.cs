namespace UCosmic.Domain.Identity
{
    public class GetUserByEduPersonTargetedIdHandler : IHandleQueries<GetUserByEduPersonTargetedIdQuery, User>
    {
        private readonly IQueryEntities _entities;

        public GetUserByEduPersonTargetedIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User Handle(GetUserByEduPersonTargetedIdQuery query)
        {
            return _entities.Users
                .ByEduPersonTargetedId(query.EduPersonTargetedId)
            ;
        }
    }
}
