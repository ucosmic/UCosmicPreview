using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
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
            SignInRouteMapper.RegisterRoutes(context);
            SignUpRouteMapper.RegisterRoutes(context);

            EmailConfirmationRouteMapper.RegisterRoutes(context);
            EmailConfirmationModelMapper.RegisterProfiles();

            PasswordRouteMapper.RegisterRoutes(context);
            PasswordModelMapper.RegisterProfiles();

            RolesRouter.RegisterRoutes(context);
            RoleFormProfiler.RegisterProfiles();
            RoleSearchResultProfiler.RegisterProfiles();

            Saml2MetadataRouter.RegisterRoutes(context);

            SignOnRouter.RegisterRoutes(context);
            Saml2IntegrationProfiler.RegisterProfiles();

            ConfirmEmailRouter.RegisterRoutes(context);
            ConfirmEmailProfiler.RegisterProfiles();

            //// default route
            //context.MapRoute(
            //    "Identity_default",
            //    "Identity/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
