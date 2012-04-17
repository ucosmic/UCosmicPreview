using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses;
using UCosmic.Www.Mvc.Areas.My.Routes;

namespace UCosmic.Www.Mvc.Areas.My
{
    public class MyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "my"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            EmailAddressesRoutes.RegisterRoutes(context);
            ChangeSpellingFormMapper.RegisterProfiles();

            //context.MapRoute(
            //    "My_default",
            //    "My/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
