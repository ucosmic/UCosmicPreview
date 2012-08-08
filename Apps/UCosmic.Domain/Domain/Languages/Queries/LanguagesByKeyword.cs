using System;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class LanguagesByKeyword : BaseEntitiesQuery<Language>, IDefineQuery<Language[]>
    {
        public LanguagesByKeyword(string keyword)
        {
            Keyword = keyword;
        }

        public string Keyword { get; private set; }
    }

    public class HandleLanguagesByKeywordQuery : IHandleQueries<LanguagesByKeyword, Language[]>
    {
        private readonly IQueryEntities _entities;

        public HandleLanguagesByKeywordQuery(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Language[] Handle(LanguagesByKeyword query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (query.Keyword == null) return new Language[0];

            var result = _entities.Query<Language>()
                .EagerLoad(_entities, query.EagerLoad)
                .OrderBy(query.OrderBy)
            ;

            if (query.Keyword != "")
            {
                result = result.Where(
                    l =>
                        l.TwoLetterIsoCode.Contains(query.Keyword) ||
                        l.ThreeLetterIsoCode.Contains(query.Keyword) ||
                        l.ThreeLetterIsoBibliographicCode.Contains(query.Keyword) ||
                        l.Names.Any(
                            n =>
                                n.Text.Contains(query.Keyword) ||
                                n.AsciiEquivalent.Contains(query.Keyword)
                        )
                );
            }

            return result.ToArray();
        }
    }
}
