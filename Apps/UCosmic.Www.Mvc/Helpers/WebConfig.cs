using System.Web;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc
{
    public static class WebConfig
    {
        private static readonly IManageConfigurations Config;

        static WebConfig()
        {
            Config = new DotNetConfigurationManager();
        }

        public static bool IsDeployedTo(string deployToTarget)
        {
            return Config.IsDeployedTo(deployToTarget);
        }

        public static bool IsDeployedToCloud { get { return Config.IsDeployedToCloud; } }

        public static bool EnableUserVoiceWidget(HttpRequestBase request)
        {
            return (request != null && request.IsAuthenticated);
        }


        public static string GeoPlanetAppId { get { return Config.GeoPlanetAppId; } }

        public static string GeoNamesUserName { get { return Config.GeoNamesUserName; } }
    }
}
