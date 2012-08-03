using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Areas.Common.Models.Navigation;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class NavigationController : Controller
    {
        [ChildActionOnly]
        [ActionName("horizontal-tabs")]
        public virtual PartialViewResult HorizontalTabs()
        {
            var model = new HorizontalTabsInfo
            {
                DisplayAgreementsTab = User.IsInAnyRoles(RoleName.InstitutionalAgreementManagers),
                DisplayProfileTab = Request.IsAuthenticated,
            };
            return PartialView(model);
        }
    }

    public static class NavigationRouter
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Navigation.Name;

        public class HorizontalTabsRoute : MvcRoute
        {
            public HorizontalTabsRoute()
            {
                Url = "navigation/horizontal-tabs";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Navigation.ActionNames.HorizontalTabs,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
