using System;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class ConfirmEmailController : BaseController
    {
        private readonly ConfirmEmailServices _services;

        public  ConfirmEmailController(ConfirmEmailServices services)
        {
            _services = services;
        }

        [HttpGet]
        public virtual ActionResult Get(Guid token, string secretCode)
        {
            return HttpNotFound();
        }
    }
}
