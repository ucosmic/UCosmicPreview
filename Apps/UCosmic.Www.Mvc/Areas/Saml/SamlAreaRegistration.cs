using System.Web.Mvc;

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
            //context.MapRoute(
            //    "Saml_default",
            //    "Saml/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
