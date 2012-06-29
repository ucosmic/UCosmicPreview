using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Areas.RecruitmentAgencies.Models.Configuration;

namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies.Controllers
{
    public partial class ConfigurationController : Controller
    {
        [ActionName("configure")]
        public virtual ActionResult Configure()
        {
            var model = new ConfigurationForm()
            {
                WelcomeMessage = "Welcome Message",
                Notifications = "Notifications"
            };
            return View(model);
        }

    }
}