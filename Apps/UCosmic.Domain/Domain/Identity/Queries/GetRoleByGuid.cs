using System;

namespace UCosmic.Domain.Identity
{
    public class GetRoleByGuidQuery : BaseEntityQuery<Role>, IDefineQuery<Role>
    {
        public GetRoleByGuidQuery(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class GetRoleByGuidHandler : IHandleQueries<GetRoleByGuidQuery, Role>
    {
        private readonly IQueryEntities _entities;

        public GetRoleByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Role Handle(GetRoleByGuidQuery query)
        {
            return _entities.Get<Role>()
                .EagerLoad(query.EagerLoad, _entities)
                .By(query.Guid)
            ;
        }
    }
}
