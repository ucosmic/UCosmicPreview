using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Languages
{
    public class LanguagesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "languages";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Languages_default",
            //    "Languages/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
