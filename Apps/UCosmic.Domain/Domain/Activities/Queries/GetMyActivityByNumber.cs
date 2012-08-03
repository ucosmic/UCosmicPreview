using System;
using System.Security.Principal;

namespace UCosmic.Domain.Activities
{
    public class GetMyActivityByNumberQuery : BaseEntityQuery<Activity>, IDefineQuery<Activity>
    {
        public IPrincipal Principal { get; set; }
        public int Number { get; set; }
    }

    public class GetMyActivityByNumberHandler : IHandleQueries<GetMyActivityByNumberQuery, Activity>
    {
        private readonly IQueryEntities _entities;

        public GetMyActivityByNumberHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Activity Handle(GetMyActivityByNumberQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Query<Activity>()
                .EagerLoad(_entities, query.EagerLoad)
                .ByUserNameAndNumber(query.Principal.Identity.Name, query.Number)
            ;

            return result;
        }
    }
}
