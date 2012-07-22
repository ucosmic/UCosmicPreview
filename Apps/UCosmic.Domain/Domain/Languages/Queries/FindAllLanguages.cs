using System;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class FindAllLanguagesQuery : BaseEntitiesQuery<Language>, IDefineQuery<Language[]>
    {
    }

    public class FindAllLanguagesHandler : IHandleQueries<FindAllLanguagesQuery, Language[]>
    {
        private readonly IQueryEntities _entities;

        public FindAllLanguagesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Language[] Handle(FindAllLanguagesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Read<Language>()
                .EagerLoad(query.EagerLoad, _entities)
                .OrderBy(query.OrderBy)
            ;

            return result.ToArray();
        }
    }
}
