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

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(SignDownRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-down";
            private static readonly string Action = MVC.Identity.SignDown.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
