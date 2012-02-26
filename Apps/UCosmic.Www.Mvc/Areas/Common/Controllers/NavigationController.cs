using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Common.Models.Navigation;

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
                DisplayAgreementsTab = User.IsInRole(RoleName.InstitutionalAgreementManager) || User.IsInRole(RoleName.InstitutionalAgreementSupervisor),
                DisplayProfileTab = Request.IsAuthenticated,
            };
            return PartialView(model);
        }
    }
}
