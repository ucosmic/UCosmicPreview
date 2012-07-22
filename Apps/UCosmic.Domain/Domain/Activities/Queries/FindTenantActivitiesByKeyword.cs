using System;

namespace UCosmic.Domain.Activities
{
    public class FindTenantActivitiesByKeywordQuery : BaseEntitiesQuery<Activity>, IDefineQuery<PagedResult<Activity>>
    {
        public object Tenant { get; set; }
        public string Keyword { get; set; }
        public PagerOptions PagerOptions { get; set; }
    }

    public class FindTenantActivitiesByKeywordHandler : IHandleQueries<FindTenantActivitiesByKeywordQuery, PagedResult<Activity>>
    {
        private readonly IQueryEntities _entities;

        public FindTenantActivitiesByKeywordHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public PagedResult<Activity> Handle(FindTenantActivitiesByKeywordQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Query<Activity>()
                .EagerLoad(query.EagerLoad, _entities)
                .WithTenant(query.Tenant)
                .WithMode(ActivityMode.Public)
                .WithKeyword(query.Keyword)
                .OrderBy(query.OrderBy)
            ;

            var pagedResults = new PagedResult<Activity>(results, query.PagerOptions);

            return pagedResults;
        }
    }
}
