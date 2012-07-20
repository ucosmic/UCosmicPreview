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

            var result = _entities.Get<Activity>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByUserNameAndNumber(query.Principal.Identity.Name, query.Number)
            ;

            return result;
        }
    }
}
