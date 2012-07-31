using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Establishments
{
    public class EstablishmentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "establishments"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Establishments_default",
            //    "Establishments/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
