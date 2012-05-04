using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;

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
            ConfirmEmailRouter.RegisterRoutes(context);
            ReceiveSamlAuthnResponseRouter.RegisterRoutes(context);
            SignDownRouter.RegisterRoutes(context);
            SignInRouter.RegisterRoutes(context);
            SignOnRouter.RegisterRoutes(context);
            SignOutRouter.RegisterRoutes(context);
            SignOverRouter.RegisterRoutes(context);
            SignUpRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "Identity_default",
            //    "Identity/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
