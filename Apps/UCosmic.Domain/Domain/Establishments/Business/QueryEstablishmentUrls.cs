using System;
using System.Linq.Expressions;
using LinqKit;

namespace UCosmic.Domain.Establishments
{
    public static class QueryEstablishmentUrls
    {
        public static Expression<Func<EstablishmentUrl, bool>> SearchTermMatches(string term, StringMatchStrategy matchStrategy, StringComparison? stringComparison = null)
        {
            var textMatches =
                TextMatches(term, matchStrategy, stringComparison).Expand()
            ;
            var officialUrlMatches =
                IsOfficialUrl()
                .And
                (
                    textMatches
                )
            ;
            var nonOfficialNameMatches =
                IsNotOfficialUrl()
                //.And
                //(
                //    TranslationToLanguageMatchesCurrentUiCulture()
                //)
                .And
                (
                    textMatches
                )
                .Expand()
            ;
            var urlMatches =
                officialUrlMatches
                .Or
                (
                    nonOfficialNameMatches
                )
            ;
            return urlMatches;
        }

        //private static Expression<Func<EstablishmentUrl, bool>> TranslationToLanguageMatchesCurrentUiCulture()
        //{
        //    var currentLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        //    return url =>
        //        url.TranslationToLanguage != null &&
        //        url.TranslationToLanguage.TwoLetterIsoCode == currentLanguage;
        //}

        private static Expression<Func<EstablishmentUrl, bool>> IsOfficialUrl()
        {
            return url => url.IsOfficialUrl;
        }

        private static Expression<Func<EstablishmentUrl, bool>> IsNotOfficialUrl()
        {
            return url => !url.IsOfficialUrl;
        }

        private static Expression<Func<EstablishmentUrl, bool>> TextMatches(string term, StringMatchStrategy matchStrategy, StringComparison? stringComparison = null)
        {
            Expression<Func<EstablishmentUrl, bool>> expression;
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    if (stringComparison.HasValue)
                        expression = url => url.Value.Equals(term, stringComparison.Value);
                    else
                        expression = url => url.Value.Equals(term);
                    break;
                case StringMatchStrategy.StartsWith:
                    if (stringComparison.HasValue)
                        expression = url => url.Value.StartsWith(term, stringComparison.Value);
                    else
                        expression = url => url.Value.StartsWith(term);
                    break;
                case StringMatchStrategy.Contains:
                    if (stringComparison.HasValue)
                        expression = url => url.Value.Contains(term, stringComparison.Value);
                    else
                        expression = url => url.Value.Contains(term);
                    break;
                default:
                    throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
            }
            return expression;
        }
    }
}
