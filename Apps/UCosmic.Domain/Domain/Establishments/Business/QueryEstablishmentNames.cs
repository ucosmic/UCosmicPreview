using System;
using System.Globalization;
using System.Linq.Expressions;
using LinqKit;

namespace UCosmic.Domain.Establishments
{
    public static class QueryEstablishmentNames
    {
        public static Expression<Func<EstablishmentName, bool>> SearchTermMatches(string term, StringMatchStrategy matchStrategy, StringComparison? stringComparison = null)
        {
            var textOrAsciiMatches =
                TextMatches(term, matchStrategy, stringComparison)
                .Or
                (
                    AsciiEquivalentMatches(term, matchStrategy, stringComparison)
                )
                .Expand()
            ;
            var officialNameMatches =
                IsOfficialName()
                .And
                (
                    textOrAsciiMatches
                )
            ;
            var nonOfficialNameMatches =
                IsNotOfficialName()
                .And
                (
                    TranslationToLanguageMatchesCurrentUiCulture()
                )
                .And
                (
                    textOrAsciiMatches
                )
                .Expand()
            ;
            var nameMatches =
                officialNameMatches
                .Or
                (
                    nonOfficialNameMatches
                )
            ;
            return nameMatches;
        }

        private static Expression<Func<EstablishmentName, bool>> TranslationToLanguageMatchesCurrentUiCulture()
        {
            var currentLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return name =>
                name.TranslationToLanguage != null &&
                name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage;
        }

        private static Expression<Func<EstablishmentName, bool>> IsOfficialName()
        {
            return name => name.IsOfficialName;
        }

        private static Expression<Func<EstablishmentName, bool>> IsNotOfficialName()
        {
            return name => !name.IsOfficialName;
        }

        private static Expression<Func<EstablishmentName, bool>> TextMatches(string term, StringMatchStrategy matchStrategy, StringComparison? stringComparison = null)
        {
            Expression<Func<EstablishmentName, bool>> expression;
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    if (stringComparison.HasValue)
                        expression = name => name.Text.Equals(term, stringComparison.Value);
                    else
                        expression = name => name.Text.Equals(term);
                    break;
                case StringMatchStrategy.StartsWith:
                    if (stringComparison.HasValue)
                        expression = name => name.Text.StartsWith(term, stringComparison.Value);
                    else
                        expression = name => name.Text.StartsWith(term);
                    break;
                case StringMatchStrategy.Contains:
                    if (stringComparison.HasValue)
                        expression = name => name.Text.Contains(term, stringComparison.Value);
                    else
                        expression = name => name.Text.Contains(term);
                    break;
                default:
                    throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
            }
            return expression;
        }

        private static Expression<Func<EstablishmentName, bool>> AsciiEquivalentMatches(string term, StringMatchStrategy matchStrategy, StringComparison? stringComparison = null)
        {
            Expression<Func<EstablishmentName, bool>> expression;
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    if (stringComparison.HasValue)
                        expression = name => name.AsciiEquivalent.Equals(term, stringComparison.Value);
                    else
                        expression = name => name.AsciiEquivalent.Equals(term);
                    break;
                case StringMatchStrategy.StartsWith:
                    if (stringComparison.HasValue)
                        expression = name => name.AsciiEquivalent.StartsWith(term, stringComparison.Value);
                    else
                        expression = name => name.AsciiEquivalent.StartsWith(term);
                    break;
                case StringMatchStrategy.Contains:
                    if (stringComparison.HasValue)
                        expression = name => name.AsciiEquivalent.Contains(term, stringComparison.Value);
                    else
                        expression = name => name.AsciiEquivalent.Contains(term);
                    break;
                default:
                    throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
            }
            return AsciiEquivalentIsNotNull().And(expression).Expand();
        }

        private static Expression<Func<EstablishmentName, bool>> AsciiEquivalentIsNotNull()
        {
            return name => name.AsciiEquivalent != null;
        }
    }
}
