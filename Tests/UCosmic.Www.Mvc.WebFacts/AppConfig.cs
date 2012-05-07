using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;

namespace UCosmic.Www.Mvc
{
    public static class NamedUrl
    {
        public const string Home = "Home";
        public const string SignOn = "Sign On";
        public const string SignDown = "Sign Down";
        public const string EnterPassword = "Enter Password";
        public const string SignOut = "Sign Out";
        public const string PersonalHome = "Personal Home";
        public const string InstitutionalAgreementManagement = "Institutional Agreement Management";
        public const string InstitutionalAgreementAdd = "Institutional Agreement Add";
        public const string InstitutionalAgreementEdit = "Institutional Agreement Edit";
        public const string InstitutionalAgreementDetail = "Public Institutional Agreement Detail";
    }

    public static class RelativeUrl
    {
        public static readonly string Home = string.Empty;
        public const string SignOn = SignOnRouter.Get.Route;
        public const string SignDown = SignDownRouter.Get.Route;
        public const string EnterPassword = SignInRouter.Get.Route;
        public const string SignOut = SignOutRouter.Get.Route;
        public const string PersonalHome = MyHomeRouter.Get.Route;
        public const string InstitutionalAgreementManagement = "my/institutional-agreements/v1";
        public const string InstitutionalAgreementAdd = "my/institutional-agreements/v1/new";
        public const string InstitutionalAgreementEdit = "my/institutional-agreements/v1/[PathVar]/edit";
        public const string InstitutionalAgreementDetail = "institutional-agreements/[PathVar]";

        private static readonly Dictionary<string, string> TitleToUrl = new Dictionary<string, string>
        {
            { NamedUrl.Home, Home },
            { NamedUrl.SignOn, SignOn },
            { NamedUrl.SignDown, SignDown },
            { NamedUrl.EnterPassword, EnterPassword },
            { NamedUrl.SignOut, SignOut },
            { NamedUrl.PersonalHome, PersonalHome },
            { NamedUrl.InstitutionalAgreementManagement, InstitutionalAgreementManagement },
            { NamedUrl.InstitutionalAgreementAdd, InstitutionalAgreementAdd },
            { NamedUrl.InstitutionalAgreementEdit, InstitutionalAgreementEdit },
            { NamedUrl.InstitutionalAgreementDetail, InstitutionalAgreementDetail },
        };

        public static string For(string title)
        {
            if (TitleToUrl.ContainsKey(title))
                return TitleToUrl[title];

            throw new NotSupportedException(string.Format(
                "There is no URL mapped to the page with title '{0}'.", title));
        }
    }

    public static class AbsoluteUrl
    {
        public static readonly string Home = RelativeUrl.Home.ToAbsoluteUrl();
        public static readonly string SignOn = RelativeUrl.SignOn.ToAbsoluteUrl();
        public static readonly string SignDown = RelativeUrl.SignDown.ToAbsoluteUrl();
        public static readonly string EnterPassword = RelativeUrl.EnterPassword.ToAbsoluteUrl();
        public static readonly string SignOut = RelativeUrl.SignOut.ToAbsoluteUrl();
        public static readonly string PersonalHome = RelativeUrl.PersonalHome.ToAbsoluteUrl();
    }

    public static class AppConfig
    {
        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings["BaseUrl"]; }
        }

        public static bool RestartDbServerBeforeTestRun
        {
            get
            {
                var config = ConfigurationManager.AppSettings["RestartDbServerBeforeTestRun"];
                return (config != null && config.Trim().Equals("true", StringComparison.OrdinalIgnoreCase));
            }
        }

        public static bool InitializeAndSeedDbBeforeTestRun
        {
            get
            {
                var config = ConfigurationManager.AppSettings["InitializeAndSeedDbBeforeTestRun"];
                return (config != null && config.Trim().Equals("true", StringComparison.OrdinalIgnoreCase));
            }
        }

        public static bool ClearTransportLevelErrorsAfterDbServerRestart
        {
            get
            {
                var config = ConfigurationManager.AppSettings["ClearTransportLevelErrorsAfterDbServerRestart"];
                return (config != null && config.Trim().Equals("true", StringComparison.OrdinalIgnoreCase));
            }
        }

        public static bool WarmUpBrowsersBeforeTestRun
        {
            get
            {
                var config = ConfigurationManager.AppSettings["WarmUpBrowsersBeforeTestRun"];
                return (config != null && config.Trim().Equals("true", StringComparison.OrdinalIgnoreCase));
            }
        }

        public static int DefaultWebDriverWaitSeconds
        {
            get
            {
                var config = ConfigurationManager.AppSettings["DefaultWebDriverWaitSeconds"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 5;
            }
        }

        public static int BrowserWindowWidth
        {
            get
            {
                var config = ConfigurationManager.AppSettings["BrowserWindowWidth"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 1024;
            }
        }

        public static int BrowserWindowLeft
        {
            get
            {
                var config = ConfigurationManager.AppSettings["BrowserWindowLeft"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static int ChromeWindowLeft
        {
            get
            {
                var config = ConfigurationManager.AppSettings["ChromeWindowLeft"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static int FirefoxWindowLeft
        {
            get
            {
                var config = ConfigurationManager.AppSettings["FirefoxWindowLeft"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static int InternetExplorerWindowLeft
        {
            get
            {
                var config = ConfigurationManager.AppSettings["InternetExplorerWindowLeft"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static int InternetExplorerResetInterval
        {
            get
            {
                var config = ConfigurationManager.AppSettings["InternetExplorerResetInterval"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static int FirefoxResetInterval
        {
            get
            {
                var config = ConfigurationManager.AppSettings["FirefoxResetInterval"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static int ChromeResetInterval
        {
            get
            {
                var config = ConfigurationManager.AppSettings["ChromeResetInterval"];
                int value;
                if (!string.IsNullOrWhiteSpace(config) && int.TryParse(config, out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public static string TestMailDelivery
        {
            get { return ConfigurationManager.AppSettings["TestMailDelivery"]; }
        }

        public static string TestMailInbox
        {
            get { return ConfigurationManager.AppSettings[AppSettingsKey.TestMailInbox.ToString()]; }
        }

        public static string TestMailReset
        {
            get { return ConfigurationManager.AppSettings["TestMailReset"]; }
        }

    }

    public static class ExtensionMethods
    {
        public static string ToAbsoluteUrl(this string relativeUrl)
        {
            return string.Format("{0}{1}", AppConfig.BaseUrl, relativeUrl);
        }

        public static string ToRelativeUrl(this string absoluteUrl)
        {
            return absoluteUrl.Replace(AppConfig.BaseUrl, string.Empty);
        }

        public static void WaitThisManyMilleseconds(this int millesecondsToWait)
        {
            // shortcut for sleeping the thread and waiting a specified time before continuing
            Thread.Sleep(millesecondsToWait);
        }

    }

}
