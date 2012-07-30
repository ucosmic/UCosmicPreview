using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Roles.Controllers;

namespace UCosmic.Www.Mvc.Areas.Roles
{
    public class RolesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "roles"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RolesRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "Roles_default",
            //    "Roles/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
