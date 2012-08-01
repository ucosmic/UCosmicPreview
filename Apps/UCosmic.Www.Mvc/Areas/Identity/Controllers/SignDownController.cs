using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class SignDownController : BaseController
    {
        [HttpGet]
        public virtual ActionResult Get(string returnUrl)
        {
            // remove email from cookie and temp data
            HttpContext.SigningEmailAddressCookie(null);
            TempData.SigningEmailAddress(null);

            if (Request.UrlReferrer != null &&
                Request.UrlReferrer.AbsolutePath == Url.Action(MVC.Identity.SignOut.Get(null)))
                return Redirect(Request.UrlReferrer.PathAndQuery);

            return RedirectToAction(MVC.Identity.SignOn.Get(returnUrl));
        }
    }

    public static class SignDownRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignDown.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "sign-down";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignDown.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
