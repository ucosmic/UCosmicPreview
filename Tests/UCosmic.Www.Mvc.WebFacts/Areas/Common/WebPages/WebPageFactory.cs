using System;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Identity.WebPages;

namespace UCosmic.Www.Mvc.Areas.Common.WebPages
{
    public static class WebPageFactory
    {
        public static WebPageBase GetPage(IWebDriver browser)
        {
            var absoluteUrl = browser.Url;
            var relativeUrl = absoluteUrl.ToRelativeUrl();

            if (relativeUrl.StartsWith(RelativeUrl.SignUp))
            {
                switch (relativeUrl)
                {
                    case RelativeUrl.SignUp:
                        return new SignUpSendEmailPage(browser);

                    case RelativeUrl.SignUpCreatePassword:
                        return new SignUpCreatePasswordPage(browser);

                    case RelativeUrl.SignUpCompleted:
                        return new SignUpCompletedPage(browser);

                    default:
                        if (relativeUrl.StartsWith(RelativeUrl.SignUpConfirmEmail))
                            return new SignUpConfirmEmailPage(browser);
                        break;
                }
            }

            throw new NotImplementedException(string.Format(
                "The page factory does not know which page to create for url '{0}'.", relativeUrl));
        }
    }
}
