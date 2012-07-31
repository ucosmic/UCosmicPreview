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
            var model = new SignOverForm
            {
                ReturnUrl = returnUrl ??
                           (Request.UrlReferrer != null
                                ? Request.UrlReferrer.PathAndQuery
                                : _services.UserSigner.DefaultSignedOnUrl),
            };
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

            if (!ModelState.IsValid) return View(model);

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

        public class GetRoute : Route
        {
            public GetRoute()
                : base("sign-over", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignOver.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PostRoute : GetRoute
        {
            public PostRoute()
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignOver.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ValidateEmailAddressRoute : Route
        {
            public ValidateEmailAddressRoute()
                : base("sign-over/validate", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignOver.ActionNames.ValidateEmailAddress,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class UndoRoute : Route
        {
            public UndoRoute()
                : base("sign-over/undo", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignOver.ActionNames.Undo,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
