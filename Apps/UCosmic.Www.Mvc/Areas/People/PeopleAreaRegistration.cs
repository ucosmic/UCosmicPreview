using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.People.Controllers;
using UCosmic.Www.Mvc.Areas.People.Models;

namespace UCosmic.Www.Mvc.Areas.People
{
    public class PeopleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get  { return "people"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            PersonNameRouter.RegisterRoutes(context);
            GenerateDisplayNameProfiler.RegisterProfiles();

            //context.MapRoute(
            //    "People_default",
            //    "People/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
