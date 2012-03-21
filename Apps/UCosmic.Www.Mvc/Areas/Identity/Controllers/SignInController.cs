using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignIn;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    public partial class SignInController : BaseController
    {
        #region Construction & DI

        private readonly EstablishmentFinder _establishments;

        public SignInController(IQueryEntities entityQueries)
        {
            _establishments = new EstablishmentFinder(entityQueries);
        }

        #endregion
        #region SignIn

        [HttpGet]
        [ActionName("sign-in")]
        //[OpenTopTab(TopTabName.SignIn)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult SignIn(string returnUrl)
        {
            // TODO: what if user is already signed in?

            if (!IsValidReturnUrl(returnUrl))
            {
                // clear the query string of invalid redirect URL's
                return RedirectToAction(MVC.Identity.SignIn.SignIn());
            }

            // pass the return URL into a new viewmodel
            var model = new SignInForm { ReturnUrl = returnUrl, };

            // return sign in page
            return View(model);
        }

        [HttpPost]
        [ActionName("sign-in")]
        //[OpenTopTab(TopTabName.SignIn)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult SignIn(SignInForm model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    // TODO: allow user to sign in using any of their email addresses?
                    var isAuthentic = Membership.ValidateUser(model.EmailAddress, model.Password);
                    if (isAuthentic)
                    {
                        // set the authentication cookie
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, false);

                        var targetUrl = FormsAuthentication.DefaultUrl;

                        // redirect to appropriate URL
                        if (Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl.Length >= 1 && model.ReturnUrl.StartsWith("/")
                            && !model.ReturnUrl.StartsWith("//") && !model.ReturnUrl.StartsWith("/\\")
                            && IsValidReturnUrl(model.ReturnUrl))
                        {
                            //return Redirect(model.ReturnUrl);
                            targetUrl = model.ReturnUrl;
                        }
                        //return Redirect(FormsAuthentication.DefaultUrl);

                        var establishment = _establishments.FindOne(EstablishmentBy.EmailDomain(model.EmailAddress));
                        var skinsUrl = Url.Action(MVC.Common.Skins.Change(establishment.WebsiteUrl, targetUrl));
                        return Redirect(skinsUrl);
                    }

                    ModelState.AddModelError("Password", "Invalid email address or password.");
                }
                return View(MVC.Identity.SignIn.Views.sign_in, model);
            }
            return HttpNotFound();
        }

        #endregion
        #region SignOut

        [ActionName("sign-out")]
        public virtual ActionResult SignOut(string returnUrl)
        {
            // check thread identity to see if user is still signed in
            var userName = User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // if signed in, delete cookie and redirect to this action
                FormsAuthentication.SignOut();
                return RedirectToAction(MVC.Identity.SignIn.SignOut(returnUrl));
            }

            if (!IsValidReturnUrl(returnUrl))
            {
                // clear the query string of invalid redirect URL's
                return RedirectToAction(MVC.Identity.SignIn.SignOut());
            }

            // pass the return URL into a new viewmodel
            var model = new SignInForm { ReturnUrl = returnUrl, };

            // return sign out page
            return View(model);
        }

        #endregion
        #region ReturnUrl Validation

        [NonAction]
        internal bool IsValidReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                // return URL should not lead to the following pages:
                var invalidReturnUrls = new[]
                {
                    Url.Action(MVC.Identity.SignIn.SignIn()),               // back to sign in
                    Url.Action(MVC.Identity.SignIn.SignOut()),              // over to sign out
                    Url.Action(MVC.Identity.SignUp.SendEmail()),            // over to sign up
                    "/sign-up/confirm-email/",                              // sign up email confirmation
                    "/confirm-password-reset/t-",                           // password reset email confirmation
                    Url.Action(MVC.Identity.Password.ForgotPassword()),     // over to password reset
                };
                //// foreach conversion to linq expression
                //foreach (var invalidReturnUrl in invalidReturnUrls)
                //{
                //    if (returnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase))
                //        return false;
                //}
                return invalidReturnUrls.All(invalidReturnUrl => 
                    !returnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase));
            }

            // sign in from root should go to default url
            if (returnUrl == "/") return false;

            return true;
        }

        #endregion
        #region SignInAs

        [ReturnUrlReferrer("/")]
        [ActionName("sign-in-as")]
        [AuthorizeForImpersonation]
        //[OpenTopTab(TopTabName.User)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult SignInAs()
        {
            var model = new SignInAsForm();
            return View(model);
        }

        [HttpPost]
        [ActionName("sign-in-as")]
        [AuthorizeForImpersonation]
        //[OpenTopTab(TopTabName.User)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult SignInAs(SignInAsForm model)
        {
            if (model != null)
            {
                MembershipUser member = null;
                if (!string.IsNullOrWhiteSpace(model.UserName))
                {
                    model.UserName = model.UserName.Trim();
                    member = Membership.GetUser(model.UserName);
                }

                if (member == null)
                {
                    ModelState.AddModelError("UserName", string.Format("Username '{0}' could not be found.", model.UserName));
                }

                if (ModelState.IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    Session.WasSignedInAs(Session.WasSignedInAs() ?? User.Identity.Name);
                    SetFeedbackMessage(string.Format("Impersonation was successful. You are signed in as {0}.", model.UserName));
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    return RedirectToAction(MVC.Identity.Self.Me());
                }
                return View(model);
            }
            return HttpNotFound();
        }

        [ActionName("undo-sign-in-as")]
        [AuthorizeForImpersonation]
        public virtual ActionResult UndoSignInAs(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(Session.WasSignedInAs()))
            {
                SetFeedbackMessage(string.Format("Undo impersonation was successful. You are now signed in as {0}.", Session.WasSignedInAs()));
                FormsAuthentication.SetAuthCookie(Session.WasSignedInAs(false), false);
            }
            return Redirect("/" + returnUrl);
        }

        #endregion
    }
}
