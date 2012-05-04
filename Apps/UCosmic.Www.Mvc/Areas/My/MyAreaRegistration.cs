using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.My.Controllers;

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
            MyHomeRouter.RegisterRoutes(context);
            UpdateAffiliationRouter.RegisterRoutes(context);
            UpdateEmailValueRouter.RegisterRoutes(context);
            UpdateNameRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "My_default",
            //    "My/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
