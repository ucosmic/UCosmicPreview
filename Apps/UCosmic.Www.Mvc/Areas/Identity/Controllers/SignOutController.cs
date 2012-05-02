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
            , ISignMembers memberSigner
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
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Get(string returnUrl)
        {
            // sign out and redirect back to this action
            if (!string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                TempData.SigningEmailAddress(User.Identity.Name);
                _services.UserSigner.SignOff();
                return RedirectToAction(MVC.Identity.SignOut.Get(returnUrl));
            }

            // determine which form to show
            var emailAddress = TempData.SigningEmailAddress();
            var establishment = _services.QueryProcessor.Execute(
                new GetEstablishmentByEmailQuery
                {
                    Email = emailAddress,
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.SamlSignOn,
                    }
                }
            );

            // return sign in view for local accounts
            if (establishment != null && !establishment.HasSamlSignOn())
                return View(new SignInForm(HttpContext, TempData, returnUrl));

            // otherwise, return sign on view
            TempData.SigningEmailAddress(null);
            return View(new SignOnForm(HttpContext, returnUrl));
        }
    }

    public static class SignOutRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOut.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            if (!WebConfig.IsDeployedToCloud)
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
