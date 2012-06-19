using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Activities.Controllers;

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
            ActivityListRouter.RegisterRoutes(context);
            ActivityFormRouter.RegisterRoutes(context);
            ActivitySearchRouter.RegisterRoutes(context);
            TagMenuRouter.RegisterRoutes(context);
            TagListRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "Activities_default",
            //    "Activities/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
