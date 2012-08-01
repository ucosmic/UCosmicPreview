using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignInServices
    {
        public SignInServices(IProcessQueries queryProcessor
            , ISignUsers userSigner
            , IStorePasswords passwords
        )
        {
            QueryProcessor = queryProcessor;
            UserSigner = userSigner;
            Passwords = passwords;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public ISignUsers UserSigner { get; private set; }
        public IStorePasswords Passwords { get; private set; }
    }

    public partial class SignInController : BaseController
    {
        private readonly SignInServices _services;

        public SignInController(SignInServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("sign-in")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateSigningEmail]
        [ValidateSigningReturnUrl]
        public virtual ActionResult Get(string returnUrl)
        {
            // return a sign-in form
            var model = new SignInForm(HttpContext, TempData, returnUrl);
            return View(model);
        }

        //[HttpGet]
        //public virtual RedirectToRouteResult Test()
        //{
        //    _services.UserSigner.SignOn("ludwigd@uc.edu");
        //    return RedirectToAction(MVC.Identity.MyHome.Get());
        //}

        [HttpPost]
        public virtual JsonResult ValidatePassword(
            [CustomizeValidator(Properties = SignInForm.PasswordPropertyName)] SignInForm model)
        {
            // form is valid unless email address is eligible
            return ValidateRemote(SignInForm.PasswordPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("sign-in")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateAntiForgeryToken]
        [ValidateSigningEmail(ParamName = "model")]
        public virtual ActionResult Post(SignInForm model)
        {
            if (model == null) return HttpNotFound();

            // sync the model email address with cookie or temp data
            model.EmailAddress = HttpContext.SigningEmailAddressCookie(false)
                                 ?? TempData.SigningEmailAddress();
            if (!ModelState.IsValid) return View(model);

            // clear the email from temp data
            TempData.SigningEmailAddress(null);

            // reset the invalid password attempt window
            Session.FailedPasswordAttempts(false);

            // sign on the user
            _services.UserSigner.SignOn(model.EmailAddress, model.RememberMe);

            // flash the success message
            SetFeedbackMessage(string.Format(
                SignOnController.SuccessMessageFormat,
                    model.EmailAddress));

            // redirect to return url
            var establishment = _services.QueryProcessor.Execute(
                new GetEstablishmentByEmailQuery(model.EmailAddress));
            var returnUrl = model.ReturnUrl ??
                            _services.UserSigner.DefaultSignedOnUrl;
            var skinsUrl = Url.Action(MVC.Common.Skins.Change(establishment.WebsiteUrl, returnUrl));
            return Redirect(skinsUrl);
        }
    }

    public static class SignInRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignIn.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "sign-in";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignIn.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }

            public const string SignInUrl = "sign-in";
        }

        public class PostRoute : GetRoute
        {
            public PostRoute()
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignIn.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ValidatePasswordRoute : MvcRoute
        {
            public ValidatePasswordRoute()
            {
                Url = "sign-in/validate";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignIn.ActionNames.ValidatePassword,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
