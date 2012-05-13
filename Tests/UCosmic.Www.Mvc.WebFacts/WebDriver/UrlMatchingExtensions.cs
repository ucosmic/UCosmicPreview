using System;

namespace UCosmic.Www.Mvc
{
    internal static class UrlMatchingExtensions
    {
        internal static bool MatchesUrl(this string actualUrl, string expectedUrl)
        {
            // check for URL path variable token "[PathVar]"
            const string urlPathVariableToken = Page.UrlPathVariableToken;
            var isExact = !expectedUrl.Contains(urlPathVariableToken);

            // do an exact comparison of the expected & actual URL's
            if (isExact)
                return actualUrl.Equals(expectedUrl, StringComparison.OrdinalIgnoreCase)
                    || actualUrl.WithoutQueryString().Equals(expectedUrl);

            return actualUrl.MatchesParameterizedUrl(expectedUrl);
        }

        internal static bool MatchesParameterizedUrl(this string actualUrl, string expectedUrl)
        {
            // check for URL path variable token "[PathVar]"
            const string urlPathVariableToken = Page.UrlPathVariableToken;
            if (!expectedUrl.Contains(urlPathVariableToken)) return false;

            // if there is a path variable token, check that the actual URL starts with the
            // part of the expected URL before the token, and ends with the part of the
            // expected URL after the token
            var urlFront = expectedUrl.Substring(0, expectedUrl.IndexOf(urlPathVariableToken, StringComparison.OrdinalIgnoreCase));
            var urlBack = expectedUrl.Substring(expectedUrl.LastIndexOf(urlPathVariableToken, StringComparison.OrdinalIgnoreCase)
                                                + urlPathVariableToken.Length);
            return actualUrl.StartsWith(urlFront) && actualUrl.EndsWith(urlBack);
        }

        internal static string WithoutQueryString(this string actualUrl)
        {
            var indexOfQueryString = actualUrl.IndexOf('?');
            return indexOfQueryString > 0
                ? actualUrl.Substring(0, indexOfQueryString)
                : actualUrl;
        }
    }
}