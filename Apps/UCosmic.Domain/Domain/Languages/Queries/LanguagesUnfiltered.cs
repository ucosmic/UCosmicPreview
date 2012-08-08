using System;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class LanguagesUnfiltered : BaseEntitiesQuery<Language>, IDefineQuery<Language[]>
    {
    }

    public class HandleLanguagesUnfilteredQuery : IHandleQueries<LanguagesUnfiltered, Language[]>
    {
        private readonly IQueryEntities _entities;

        public HandleLanguagesUnfilteredQuery(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Language[] Handle(LanguagesUnfiltered query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Query<Language>()
                .EagerLoad(_entities, query.EagerLoad)
                .OrderBy(query.OrderBy)
            ;

            return result.ToArray();
        }
    }
}
