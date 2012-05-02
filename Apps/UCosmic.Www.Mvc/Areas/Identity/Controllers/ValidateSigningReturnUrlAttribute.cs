using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateSigningReturnUrlAttribute : ActionFilterAttribute
    {
        public const string ReturnUrlParamName = "returnUrl";
        private Uri _requestUrl;
        private string _returnUrl;
        private UrlHelper _urlHelper;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            InitializePrivateFields(filterContext);

            // when signing in from home, redirect to my profile
            if (!ValidateRootReturnUrl(filterContext)) return;

            var invalidReturnUrls = GetInvalidReturnUrls();

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var invalidReturnUrl in invalidReturnUrls)
                if (!ValidateNonRootReturnUrl(filterContext, invalidReturnUrl)) return;
            // ReSharper restore LoopCanBeConvertedToQuery
        }

        private IEnumerable<string> GetInvalidReturnUrls()
        {
            var guid = Guid.NewGuid();
            var slashGuid = string.Format("/{0}", guid);

            // don't go to the following actions after signing in:
            var invalidReturnUrls = new[]
            {
                // don't go to sign on page
                _urlHelper.Action(MVC.Identity.SignOn.Get()),

                // don't go to sign in page
                _urlHelper.Action(MVC.Identity.SignIn.Get()),

                // don't go to sign up page
                _urlHelper.Action(MVC.Identity.SignUp.Get()),

                // don't go to sign out or down pages
                _urlHelper.Action(MVC.Identity.SignOut.Get()),
                _urlHelper.Action(MVC.Identity.SignDown.Get()),

                // don't go to confirm email page
                _urlHelper.Action(MVC.Identity.ConfirmEmail.Get(guid, null))
                    .Replace(slashGuid, string.Empty),

                // don't go to create password page
                _urlHelper.Action(MVC.Passwords.CreatePassword.Get(guid))
                    .Replace(slashGuid, string.Empty),

                // don't go to forgot password page
                _urlHelper.Action(MVC.Passwords.ForgotPassword.Get()),

                // don't go to reset password page
                _urlHelper.Action(MVC.Passwords.ResetPassword.Get(guid))
                    .Replace(slashGuid, string.Empty),
            };
            return invalidReturnUrls;
        }

        private void InitializePrivateFields(ActionExecutingContext filterContext)
        {
            _requestUrl = filterContext.HttpContext.Request.Url;
            if (_requestUrl == null)
                throw new InvalidOperationException(
                    "An unexpected error has occurred (HttpRequestBase.Url was null).");

            if (!filterContext.ActionParameters.ContainsKey(ReturnUrlParamName))
                throw new InvalidOperationException(string.Format(
                    "There is no action parameter named '{0}'.",
                    ReturnUrlParamName));

            var returnUrl = filterContext.ActionParameters[ReturnUrlParamName];
            if (returnUrl != null && !(returnUrl is string))
                throw new InvalidOperationException(string.Format(
                    "The '{0}' action parameter must be a string.",
                    ReturnUrlParamName));

            _returnUrl = returnUrl as string ?? string.Empty;
            _urlHelper = new UrlHelper(filterContext.RequestContext);
        }

        private bool ValidateRootReturnUrl(ActionExecutingContext filterContext)
        {
            return _returnUrl != "/"
                || RedirectWithoutReturnUrl(filterContext);
        }

        private bool ValidateNonRootReturnUrl(ActionExecutingContext filterContext, string invalidReturnUrl)
        {
            return !_returnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase)
                || RedirectWithoutReturnUrl(filterContext);
        }

        private bool RedirectWithoutReturnUrl(ActionExecutingContext filterContext)
        {
            //var myProfileUrl = _urlHelper.Action(MVC.My.Profile.Get()).UrlEncoded();
            //var redirectUrl = string.Format("{0}?{1}={2}",
            //    _requestUrl.AbsolutePath, ReturnUrlParamName, myProfileUrl);
            var redirectUrl = _requestUrl.AbsolutePath;
            filterContext.Result = new RedirectResult(redirectUrl);
            return false;
        }
    }
}