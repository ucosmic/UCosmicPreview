using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Preferences
{
    public class PreferencesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "preferences";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Preferences_default",
            //    "Preferences/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
