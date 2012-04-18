using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Areas.My.Controllers;

namespace UCosmic.Www.Mvc.Areas.My
{
    public class MyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "my"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ChangeEmailSpellingRouter.RegisterRoutes(context);
            ChangeEmailSpellingProfiler.RegisterProfiles();

            ProfileRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "My_default",
            //    "My/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
