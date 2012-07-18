using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class LanguageFinder : RevisableEntityFinder<Language>
    {
        public LanguageFinder(IQueryEntities entityQueries)
            : base(entityQueries)
        {
        }

        public override ICollection<Language> FindMany(RevisableEntityQueryCriteria<Language> criteria)
        {
            var query = InitializeQuery(EntityQueries.Languages, criteria);
            var finder = criteria as LanguageQuery ?? new LanguageQuery();

            // apply iso code
            if (!string.IsNullOrWhiteSpace(finder.IsoCode))
                query = query.Where(language => finder.IsoCode.Equals(language.TwoLetterIsoCode, StringComparison.OrdinalIgnoreCase)
                    || finder.IsoCode.Equals(language.ThreeLetterIsoCode, StringComparison.OrdinalIgnoreCase)
                    || finder.IsoCode.Equals(language.ThreeLetterIsoBibliographicCode, StringComparison.OrdinalIgnoreCase));

            query = FinalizeQuery(query, criteria);

            var results = query.ToList();
            return results;
        }
    }
}