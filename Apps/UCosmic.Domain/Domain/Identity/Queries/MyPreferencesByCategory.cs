using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public class MyPreferencesByCategory : BaseEntitiesQuery<Preference>, IDefineQuery<Preference[]>
    {
        public MyPreferencesByCategory(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
        public Enum Category { get; set; }
    }

    public class HandleMyPreferenceByCategoryCommand : IHandleQueries<MyPreferencesByCategory, Preference[]>
    {
        private readonly ICommandEntities _entities;

        public HandleMyPreferenceByCategoryCommand(ICommandEntities entities)
        {
            _entities = entities;
        }

        public Preference[] Handle(MyPreferencesByCategory query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Get<Preference>()
                .EagerLoad(_entities, query.EagerLoad)
                .ByPrincipal(query.Principal)
                .ByCategory(query.Category)
                .OrderBy(query.OrderBy)
                .ToArray()
            ;
        }
    }
}
