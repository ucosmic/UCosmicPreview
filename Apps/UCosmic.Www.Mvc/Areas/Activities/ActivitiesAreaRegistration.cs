using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Activities
{
    public class ActivitiesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "activities"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Activities_default",
            //    "Activities/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
