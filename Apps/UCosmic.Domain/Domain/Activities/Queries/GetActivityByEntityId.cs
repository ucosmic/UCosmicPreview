using System;

namespace UCosmic.Domain.Activities
{
    public class GetActivityByEntityIdQuery : BaseEntityQuery<Activity>, IDefineQuery<Activity>
    {
        public Guid EntityId { get; set; }
    }

    public class GetActivityByEntityIdHandler : IHandleQueries<GetActivityByEntityIdQuery, Activity>
    {
        private readonly IQueryEntities _entities;

        public GetActivityByEntityIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Activity Handle(GetActivityByEntityIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<Activity>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByEntityId(query.EntityId)
            ;

            return result;
        }
    }
}
