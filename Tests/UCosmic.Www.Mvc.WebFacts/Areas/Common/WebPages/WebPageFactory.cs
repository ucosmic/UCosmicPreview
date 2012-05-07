using System;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.Common.WebPages
{
    public static class WebPageFactory
    {
        public static WebPageBase GetPage(IWebDriver browser)
        {
            var absoluteUrl = browser.Url;
            var relativeUrl = absoluteUrl.ToRelativeUrl();

            if (relativeUrl.MatchesUrl(RelativeUrl.SignOn))
                return new SignOnForm(browser);

            if (relativeUrl.MatchesUrl(RelativeUrl.EnterPassword))
                return new SignInForm(browser);

            if (relativeUrl.MatchesUrl(RelativeUrl.InstitutionalAgreementAdd) ||
                relativeUrl.MatchesUrl(RelativeUrl.InstitutionalAgreementEdit))
                return new InstitutionalAgreementForm(browser);

            throw new NotSupportedException(string.Format(
                "The page factory does not know which page to create for url '{0}'.", relativeUrl));
        }
    }

    internal static class UrlMatchingExtensions
    {
        private const string UrlPathVariableToken = "[PathVar]";

        internal static bool MatchesUrl(this string actualUrl, string expectedUrl)
        {
            // check for URL path variable token "[PathVar]"
            var isExact = !expectedUrl.Contains(UrlPathVariableToken);

            // do an exact comparison of the expected & actual URL's
            if (isExact) return actualUrl.StartsWith(expectedUrl);

            // if there is a path variable token, check that the actual URL starts with the
            // part of the expected URL before the token, and ends with the part of the
            // expected URL after the token
            var urlFront = expectedUrl.Substring(0, expectedUrl.IndexOf(UrlPathVariableToken, StringComparison.OrdinalIgnoreCase));
            var urlBack = expectedUrl.Substring(expectedUrl.LastIndexOf(UrlPathVariableToken, StringComparison.OrdinalIgnoreCase)
                + UrlPathVariableToken.Length);
            return actualUrl.StartsWith(urlFront) && actualUrl.EndsWith(urlBack);
        }
    }
}
