using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Activity.Models;

namespace UCosmic.Www.Mvc.Areas.Activity
{
    public class ActivityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "activity"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            AutoCompleteTagProfiler.RegisterProfiles();

            context.MapRoute(
                "Activity_default",
                "Activity/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
