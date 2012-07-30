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

            ConfigurationFormsRouteMapper.RegisterRoutes(context);

            PublicSearchRouteMapper.RegisterRoutes(context);

            //context.MapRoute(
            //    "InstitutionalAgreements_default",
            //    "InstitutionalAgreements/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
