using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public class MyPreferencesByCategory : BaseEntitiesQuery<Preference>, IDefineQuery<Preference[]>
    {
        public MyPreferencesByCategory(IPrincipal principal)
        {
            Principal = principal;
        }

        public MyPreferencesByCategory(IPrincipal principal, string anonymousId)
            : this(principal)
        {
            AnonymousId = anonymousId;
        }

        public IPrincipal Principal { get; private set; }
        public string AnonymousId { get; private set; }
        public bool HasPrincipal { get { return Principal != null && !string.IsNullOrWhiteSpace(Principal.Identity.Name); } }
        public bool IsAnonymous { get { return !string.IsNullOrWhiteSpace(AnonymousId); } }
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

            if (!query.HasPrincipal && !query.IsAnonymous) return new Preference[0];

            var results = _entities.Get<Preference>()
                .EagerLoad(_entities, query.EagerLoad)
            ;

            if (query.HasPrincipal)
                results = results.ByPrincipal(query.Principal);
            else if (query.IsAnonymous)
                results = results.ByAnonymousId(query.AnonymousId);

            results = results
                .ByCategory(query.Category)
                .OrderBy(query.OrderBy)
            ;

            return results.ToArray();
        }
    }
}
