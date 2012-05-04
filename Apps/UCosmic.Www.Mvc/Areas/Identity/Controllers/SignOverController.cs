using System.Web.Mvc;
using FluentValidation.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignOverServices
    {
        public SignOverServices(ISignUsers userSigner)
        {
            UserSigner = userSigner;
        }

        public ISignUsers UserSigner { get; private set; }
    }

    [Authorize]
    [EnforceHttps]
    public partial class SignOverController : BaseController
    {
        private readonly SignOverServices _services;

        public SignOverController(SignOverServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ValidateSigningEmail]
        [ActionName("sign-over")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateSigningReturnUrl]
        public virtual ActionResult Get(string returnUrl)
        {
            var model = new SignOverForm();
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = SignOnForm.EmailAddressPropertyName)] SignOverForm model)
        {
            // form is valid unless email address is eligible
            return ValidateRemote(SignOnForm.EmailAddressPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("sign-over")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateAntiForgeryToken]
        [ValidateSigningEmail(ParamName = "model")]
        public virtual ActionResult Post(SignOverForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return PartialView(model);

            // sign on
            _services.UserSigner.SignOn(model.EmailAddress);

            // set session value
            Session.WasSignedInAs(Session.WasSignedInAs() ?? User.Identity.Name);

            // flash feedback message
            SetFeedbackMessage(string.Format(SuccessMessageFormat, 
                User.Identity.Name, model.EmailAddress));

            // redirect to return url
            model.ReturnUrl = model.ReturnUrl ?? Url.Action(MVC.Identity.MyHome.Get());
            return Redirect(model.ReturnUrl);
        }

        public const string SuccessMessageFormat = "Sign on was changed from {0} to {1}.";

        [HttpGet]
        [ValidateSigningEmail]
        public virtual ActionResult Undo(string returnUrl)
        {
            var wasSignedInAs = Session.WasSignedInAs(false);
            _services.UserSigner.SignOn(wasSignedInAs);
            SetFeedbackMessage(string.Format(SuccessMessageFormat, User.Identity.Name, wasSignedInAs));
            return Redirect(returnUrl);
        }
    }

    public static class SignOverRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOver.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(SignOverRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-over";
            private static readonly string Action = MVC.Identity.SignOver.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Post
        {
            public const string Route = Get.Route;
            private static readonly string Action = MVC.Identity.SignOver.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateEmailAddress
        {
            public const string Route = "sign-over/validate";
            private static readonly string Action = MVC.Identity.SignOver.ActionNames.ValidateEmailAddress;
            public static void MapRoutes(AreaRegistrationContext routes, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                routes.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Undo
        {
            public const string Route = "sign-over/undo";
            private static readonly string Action = MVC.Identity.SignOver.ActionNames.Undo;
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
