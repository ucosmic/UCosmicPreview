using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;

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

            RolesRouteMapper.RegisterRoutes(context);
            RolesModelMapper.RegisterProfiles();

            SelfRouteMapper.RegisterRoutes(context);
            SelfModelMapper.RegisterProfiles();

            Saml2MetadataRouteMapper.RegisterRoutes(context);

            SignOnRouteMapper.RegisterRoutes(context);
            SignOnModelMapper.RegisterProfiles();

            //// default route
            //context.MapRoute(
            //    "Identity_default",
            //    "Identity/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
