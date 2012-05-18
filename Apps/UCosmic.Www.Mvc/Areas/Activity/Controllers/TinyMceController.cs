using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activity.Controllers
{
    public partial class TinyMceController : BaseController
    {
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
