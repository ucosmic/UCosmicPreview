//using System;
//using OpenQA.Selenium;
//using UCosmic.Www.Mvc.Areas.Identity;
//using UCosmic.Www.Mvc.Areas.InstitutionalAgreements;

//namespace UCosmic.Www.Mvc.Areas.Common.WebPages
//{
//    public static class WebPageFactory
//    {
//        public static WebPageBase GetPage(IWebDriver browser)
//        {
//            var absoluteUrl = browser.Url;
//            var relativeUrl = absoluteUrl.ToRelativeUrl();

//            if (relativeUrl.MatchesUrl(RelativeUrl.SignOn))
//                return new SignOnForm(browser);

//            if (relativeUrl.MatchesUrl(RelativeUrl.EnterPassword))
//                return new SignInForm(browser);

//            if (relativeUrl.MatchesUrl(RelativeUrl.InstitutionalAgreementAdd) ||
//                relativeUrl.MatchesUrl(RelativeUrl.InstitutionalAgreementEdit))
//                return new ManageForm(browser);

//            throw new NotSupportedException(string.Format(
//                "The page factory does not know which page to create for url '{0}'.", relativeUrl));
//        }
//    }
//}
