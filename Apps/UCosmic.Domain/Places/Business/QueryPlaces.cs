using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace UCosmic.Domain.Places
{
    public static class QueryPlaces
    {
        internal static IQueryable<Place> WithName(this IQueryable<Place> queryable, string term, StringMatchStrategy matchStrategy)
        {
            var matchesName = PredicateBuilder.False<Place>()
                .Or(OfficialNameMatches(term, matchStrategy))
                .Or(NonOfficialNameMatches(term, matchStrategy));

            return queryable.AsExpandable().Where(matchesName);
        }

        private static Expression<Func<Place, bool>> OfficialNameMatches(string term, StringMatchStrategy matchStrategy)
        {
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return place => place.OfficialName.Equals(term);

                case StringMatchStrategy.StartsWith:
                    return place => place.OfficialName.StartsWith(term);

                case StringMatchStrategy.Contains:
                    return place => place.OfficialName.Contains(term);
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }

        private static Expression<Func<Place, bool>> NonOfficialNameMatches(string term, StringMatchStrategy matchStrategy)
        {
            //// using this predicate causes the 'name' parameter to not be bound
            //var predicate = 
            //    QueryPlaceNames.PlaceNameTranslationToLanguageMatchesCurrentUiCulture()
            //    .And
            //    (
            //        QueryPlaceNames.PlaceNameTextMatches(term, matchStrategy)
            //        .Or
            //        (
            //            QueryPlaceNames.PlaceNameAsciiEquivalentMatches(term, matchStrategy)
            //        )
            //    )
            //;
            //return place => place.Names.AsQueryable().Any(predicate);

            var currentLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return place => place.Names.Any(name =>
                        name.TranslationToLanguage != null &&
                        name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage &&
                        (
                            name.Text.Equals(term, StringComparison.OrdinalIgnoreCase) ||
                            (
                                name.AsciiEquivalent != null &&
                                name.AsciiEquivalent.Equals(term, StringComparison.OrdinalIgnoreCase)
                            )
                        )
                    );
                case StringMatchStrategy.StartsWith:
                    return place => place.Names.Any(name =>
                        name.TranslationToLanguage != null &&
                        name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage &&
                        (
                            name.Text.StartsWith(term) ||
                            (
                                name.AsciiEquivalent != null &&
                                name.AsciiEquivalent.StartsWith(term)
                            )
                        )
                    );
                case StringMatchStrategy.Contains:
                    return place => place.Names.Any(name =>
                        name.TranslationToLanguage != null &&
                        name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage &&
                        (
                            name.Text.Contains(term) ||
                            (
                                name.AsciiEquivalent != null &&
                                name.AsciiEquivalent.Contains(term)
                            )
                        )
                    );
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }
    }
}
