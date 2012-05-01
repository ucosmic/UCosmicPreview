using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Saml.Controllers;
using UCosmic.Www.Mvc.Areas.Saml.Models;

namespace UCosmic.Www.Mvc.Areas.Saml
{
    public class SamlAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "saml"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ServiceProviderMetadataRouter.RegisterRoutes(context);

            IdentityProviderListItemModelProfiler.RegisterProfiles();
            ListIdentityProvidersRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "Saml_default",
            //    "Saml/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
