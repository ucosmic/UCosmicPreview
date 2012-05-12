using System.Web;
using AspNetHaack;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using UCosmic.Www.Mvc;

[assembly: PreApplicationStartMethod(typeof(FormsAuthenticationConfig), "Register")]
namespace UCosmic.Www.Mvc
{
    public static class FormsAuthenticationConfig
    {
        public static void Register()
        {
            DynamicModuleUtility.RegisterModule(typeof(SuppressFormsAuthenticationRedirectModule));
        }
    }
}
