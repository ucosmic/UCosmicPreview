using System.Web.Mvc;

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
            context.MapRoute(
                "Activity_default",
                "Activity/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
