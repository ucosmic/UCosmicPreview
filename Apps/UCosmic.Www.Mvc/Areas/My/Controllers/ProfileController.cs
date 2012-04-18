using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public partial class ProfileController : BaseController
    {
        public virtual ActionResult Index()
        {
            return new EmptyResult();
        }

    }
}
