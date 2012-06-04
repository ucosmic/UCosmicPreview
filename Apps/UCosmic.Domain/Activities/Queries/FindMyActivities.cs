using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Activities
{
    public class FindMyActivitiesQuery : BaseActivitiesQuery, IDefineQuery<Activity[]>
    {
        public IPrincipal Principal { get; set; }
        public int? MaxResults { get; set; }
        public int Total { get; internal set; }
    }

    public class FindMyActivitiesHandler : IHandleQueries<FindMyActivitiesQuery, Activity[]>
    {
        private readonly IQueryEntities _entities;

        public FindMyActivitiesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Activity[] Handle(FindMyActivitiesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Activities
                .EagerLoad(query.EagerLoad, _entities)
                .ByUserName(query.Principal.Identity.Name)
                .OrderBy(query.OrderBy)
            ;

            query.Total = results.Count();

            if (query.MaxResults.HasValue && query.MaxResults > 0)
            {
                results = results.Take(query.MaxResults.Value);
            }

            return results.ToArray();
        }
    }
}
