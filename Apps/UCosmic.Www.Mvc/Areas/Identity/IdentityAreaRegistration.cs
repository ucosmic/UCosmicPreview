using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.Identity.Models;

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
            //OldSignInRouteMapper.RegisterRoutes(context);
            //OldSignUpRouteMapper.RegisterRoutes(context);

            //EmailConfirmationRouteMapper.RegisterRoutes(context);
            //EmailConfirmationModelMapper.RegisterProfiles();

            //OldPasswordRouteMapper.RegisterRoutes(context);
            //PasswordModelMapper.RegisterProfiles();

            SignOnRouter.RegisterRoutes(context);

            SignOverRouter.RegisterRoutes(context);

            SignInRouter.RegisterRoutes(context);

            SignOutRouter.RegisterRoutes(context);

            SignUpRouter.RegisterRoutes(context);
            SignUpProfiler.RegisterProfiles();

            SignDownRouter.RegisterRoutes(context);

            ReceiveSamlAuthnResponseRouter.RegisterRoutes(context);

            ConfirmEmailRouter.RegisterRoutes(context);
            ConfirmEmailProfiler.RegisterProfiles();

            //context.MapRoute(
            //    "Identity_default",
            //    "Identity/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
