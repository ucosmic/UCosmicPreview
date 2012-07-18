using System;
using System.Security.Principal;

namespace UCosmic.Domain.Activities
{
    public class FindMyActivitiesQuery : BaseEntitiesQuery<Activity>, IDefineQuery<PagedResult<Activity>>
    {
        public IPrincipal Principal { get; set; }
        public PagerOptions PagerOptions { get; set; }
    }

    public class FindMyActivitiesHandler : IHandleQueries<FindMyActivitiesQuery, PagedResult<Activity>>
    {
        private readonly IQueryEntities _entities;

        public FindMyActivitiesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public PagedResult<Activity> Handle(FindMyActivitiesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Activities
                .EagerLoad(query.EagerLoad, _entities)
                .WithUserName(query.Principal.Identity.Name)
                .OrderBy(query.OrderBy)
            ;

            var pagedResults = new PagedResult<Activity>(results, query.PagerOptions);

            return pagedResults;
        }
    }
}
