using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class InstitutionalAgreementsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "InstitutionalAgreements"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ManagementFormsRouteMapper.RegisterRoutes(context);
            ManagementFormsModelMapper.RegisterProfiles();

            ConfigurationFormsRouteMapper.RegisterRoutes(context);
            ConfigurationFormsModelMapper.RegisterProfiles();

            PublicSearchRouteMapper.RegisterRoutes(context);
            PublicSearchModelMapper.RegisterProfiles();

            //context.MapRoute(
            //    "InstitutionalAgreements_default",
            //    "InstitutionalAgreements/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
