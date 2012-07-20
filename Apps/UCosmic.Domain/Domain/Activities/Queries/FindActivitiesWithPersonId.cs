using System;
using System.Linq;

namespace UCosmic.Domain.Activities
{
    public class FindActivitiesWithPersonIdQuery : BaseEntitiesQuery<Activity>, IDefineQuery<Activity[]>
    {
        public int PersonId { get; set; }
    }

    public class FindActivitiesWithPersonIdHandler : IHandleQueries<FindActivitiesWithPersonIdQuery, Activity[]>
    {
        private readonly IQueryEntities _entities;

        public FindActivitiesWithPersonIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Activity[] Handle(FindActivitiesWithPersonIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Get<Activity>()
                .EagerLoad(query.EagerLoad, _entities)
                .WithPersonId(query.PersonId)
                .OrderBy(query.OrderBy)
            ;

            return results.ToArray();
        }
    }
}
