using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignOutServices
    {
        public SignOutServices(IProcessQueries queryProcessor
            , ISignUsers userSigner
        )
        {
            QueryProcessor = queryProcessor;
            UserSigner = userSigner;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public ISignUsers UserSigner { get; private set; }
    }

    public partial class SignOutController : BaseController
    {
        private readonly SignOutServices _services;

        public SignOutController(SignOutServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("sign-out")]
        [ValidateSigningReturnUrl]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Get(string returnUrl)
        {
            // sign out and redirect back to this action
            if (!string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                // flash the success message
                SetFeedbackMessage(SuccessMessage);

                // remove the signing email from temp data for future reloads
                TempData.SigningEmailAddress(null);

                // delete the authentication cookie and redirect
                _services.UserSigner.SignOff();
                return RedirectToAction(MVC.Identity.SignOut.Get(returnUrl));
            }

            // determine which form to show
            var establishment = _services.QueryProcessor.Execute(
                new GetEstablishmentByEmailQuery
                {
                    Email = HttpContext.SigningEmailAddressCookie(),
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.SamlSignOn,
                    }
                }
            );

            // return sign in view only if there was a non-saml cookie
            if (establishment != null && !establishment.HasSamlSignOn())
                return View(new SignInForm(HttpContext, TempData, returnUrl));

            // otherwise, return sign on view
            return View(new SignOnForm(HttpContext, returnUrl));
        }

        public const string SuccessMessage = "You have successfully been signed out of UCosmic.";
    }

    public static class SignOutRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOut.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(SignOutRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-out";
            private static readonly string Action = MVC.Identity.SignOut.ActionNames.Get;
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
