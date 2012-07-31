using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class IdentityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "identity"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Identity_default",
            //    "Identity/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
