using System;
using System.Configuration;
using System.Threading;

namespace UCosmic.Www.Mvc
{
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
            return "{0}{1}".FormatWith(AppConfig.BaseUrl, relativeUrl);
        }

        public static void WaitThisManyMilleseconds(this int millesecondsToWait)
        {
            // shortcut for sleeping the thread and waiting a specified time before continuing
            Thread.Sleep(millesecondsToWait);
        }

    }

}
