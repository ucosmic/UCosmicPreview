using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies
{
    public class RecruitmentAgenciesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "RecruitmentAgencies"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "RecruitmentAgencies_default",
            //    "RecruitmentAgencies/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
