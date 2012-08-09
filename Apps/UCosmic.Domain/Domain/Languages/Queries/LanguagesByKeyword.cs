using System;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class LanguagesByKeyword : BaseEntitiesQuery<Language>, IDefineQuery<PagedResult<Language>>
    {
        public LanguagesByKeyword(string keyword)
        {
            Keyword = keyword;
        }

        public string Keyword { get; private set; }
        public PagerOptions PagerOptions { get; set; }
    }

    public class HandleLanguagesByKeywordQuery : IHandleQueries<LanguagesByKeyword, PagedResult<Language>>
    {
        private readonly IQueryEntities _entities;

        public HandleLanguagesByKeywordQuery(IQueryEntities entities)
        {
            _entities = entities;
        }

        public PagedResult<Language> Handle(LanguagesByKeyword query)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (query.Keyword == null) return null;

            var results = _entities.Query<Language>()
                .EagerLoad(_entities, query.EagerLoad)
            ;

            if (query.Keyword != "")
            {
                results = results.Where(
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

            results = results.OrderBy(query.OrderBy);

            var pagedResults = new PagedResult<Language>(results, query.PagerOptions);

            return pagedResults;
        }
    }
}
