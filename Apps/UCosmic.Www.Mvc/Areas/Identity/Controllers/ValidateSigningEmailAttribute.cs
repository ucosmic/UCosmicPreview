using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateSigningEmailAttribute : ActionFilterAttribute
    {
        private const string ReturnUrlParamName = ValidateSigningReturnUrlAttribute.ReturnUrlParamName;

        public string ParamName { get; set; }

        public IProcessQueries QueryProcessor { get; set; }

        public IStorePasswords Passwords { get; set; }

        private Uri RequestUrl { get; set; }

        private UrlHelper Url { get; set; }

        private string _emailAddress;
        private string EmailAddress
        {
            get
            {
                return IsModeled ? Model.EmailAddress : _emailAddress;
            }
            set
            {
                if (IsModeled) Model.EmailAddress = value;
                else _emailAddress = value;
            }
        }

        private string _returnUrl;
        private string ReturnUrl
        {
            get
            {
                return IsModeled ? Model.ReturnUrl : _returnUrl;
            }
            set
            {
                if (IsModeled) Model.ReturnUrl = value;
                else _returnUrl = value;
            }
        }

        private IModelSigningEmail Model { get; set; }
        private bool IsModeled { get { return Model != null; } }

        private bool _isEstablishmentSet;
        private Establishment _establishment;
        private Establishment Establishment
        {
            get
            {
                if (_isEstablishmentSet) return _establishment;
                _establishment = QueryProcessor.Execute(
                    new GetEstablishmentByEmailQuery(EmailAddress)
                    {
                        EagerLoad = new Expression<Func<Establishment, object>>[]
                        {
                            e => e.SamlSignOn,
                        }
                    }
                );
                _isEstablishmentSet = true;
                return _establishment;
            }
        }

        private bool _isPersonSet;
        private Person _person;
        private Person Person
        {
            get
            {
                if (_isPersonSet) return _person;
                _person = QueryProcessor.Execute(
                    new GetPersonByEmailQuery
                    {
                        Email = EmailAddress,
                        EagerLoad = new Expression<Func<Person, object>>[]
                        {
                            p => p.User,
                            p => p.Emails,
                        },
                    }
                );

                _isPersonSet = true;
                return _person;
            }
        }

        private void EnsureEmailIsConfirmed()
        {
            // switch from email address to user name if email is not confirmed
            if (Person == null) return;
            var emailAddress = Person.GetEmail(EmailAddress);
            if (!emailAddress.IsConfirmed && Person.User != null)
                EmailAddress = Person.User.Name;
        }

        private bool? _isPersonLocalMember;
        private bool IsPersonLocalMember
        {
            get
            {
                if (!_isPersonLocalMember.HasValue)
                {
                    _isPersonLocalMember =
                        !IsPersonSamlUser && 
                        Person != null && 
                        Person.User != null &&
                        Person.User.IsRegistered && 
                        Passwords.Exists(Person.User.Name);
                }
                return _isPersonLocalMember.Value;
            }
        }

        private bool? _isPersonSamlMember;
        private bool IsPersonSamlUser
        {
            get
            {
                if (!_isPersonSamlMember.HasValue)
                    _isPersonSamlMember = 
                        Establishment != null && 
                        Establishment.HasSamlSignOn();
                return _isPersonSamlMember.Value;
            }
        }

        private bool? _isSignOn;
        private bool IsSignOn
        {
            get
            {
                if (_isSignOn.HasValue) return _isSignOn.Value;

                var signOnGet = Url.Action(MVC.Identity.SignOn.Get());
                var signOnPost = Url.Action(MVC.Identity.SignOn.Post());
                _isSignOn = RequestUrl.AbsolutePath.StartsWith(signOnGet) ||
                               RequestUrl.AbsolutePath.StartsWith(signOnPost);
                return _isSignOn.Value;
            }
        }

        private bool? _isSignIn;
        private bool IsSignIn
        {
            get
            {
                if (_isSignIn.HasValue) return _isSignIn.Value;

                var signInGet = Url.Action(MVC.Identity.SignIn.Get());
                var signInPost = Url.Action(MVC.Identity.SignIn.Post());
                _isSignIn = RequestUrl.AbsolutePath.StartsWith(signInGet) ||
                               RequestUrl.AbsolutePath.StartsWith(signInPost);
                return _isSignIn.Value;
            }
        }

        private bool? _isSignUp;
        private bool IsSignUp
        {
            get
            {
                if (_isSignUp.HasValue) return _isSignUp.Value;

                var signUpGet = Url.Action(MVC.Identity.SignUp.Get());
                var signUpPost = Url.Action(MVC.Identity.SignUp.Post());
                _isSignUp = RequestUrl.AbsolutePath.StartsWith(signUpGet) ||
                               RequestUrl.AbsolutePath.StartsWith(signUpPost);
                return _isSignUp.Value;
            }
        }

        private bool? _isSignOver;
        private bool IsSignOver
        {
            get
            {
                if (_isSignOver.HasValue) return _isSignOver.Value;

                var signOverGet = Url.Action(MVC.Identity.SignOver.Get());
                var signOverPost = Url.Action(MVC.Identity.SignOver.Post());
                _isSignOver = RequestUrl.AbsolutePath.StartsWith(signOverGet) ||
                               RequestUrl.AbsolutePath.StartsWith(signOverPost);
                return _isSignOver.Value;
            }
        }

        private bool? _isSignOverUndo;
        private bool IsSignOverUndo
        {
            get
            {
                if (_isSignOverUndo.HasValue) return _isSignOverUndo.Value;

                var signOverUndo = Url.Action(MVC.Identity.SignOver.Undo());
                _isSignOverUndo = RequestUrl.AbsolutePath.StartsWith(signOverUndo);
                return _isSignOverUndo.Value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Initialize(filterContext);

            if (!ValidateSignOn(filterContext)) return;

            if (!ValidateSignIn(filterContext)) return;

            if (!ValidateSignUp(filterContext)) return;

            ValidateSignOver(filterContext);
        }

        private void Initialize(ActionExecutingContext filterContext)
        {
            Url = new UrlHelper(filterContext.RequestContext);
            RequestUrl = filterContext.HttpContext.Request.Url;
            if (RequestUrl == null)
                throw new InvalidOperationException(
                    "An unexpected error has occurred (HttpRequestBase.Url was null).");

            // get the email address from the action parameter
            if (!string.IsNullOrWhiteSpace(ParamName))
            {
                if (!filterContext.ActionParameters.ContainsKey(ParamName))
                    throw new InvalidOperationException(String.Format(
                        "The action method does not have a '{0}' parameter.",
                        ParamName));

                var paramValue = filterContext.ActionParameters[ParamName] as IModelSigningEmail;
                if (paramValue == null)
                    throw new InvalidOperationException(String.Format(
                        "The action method parameter '{0}' cannot be null and " +
                        "must implement the '{1}' interface.",
                        ParamName, typeof(IModelSigningEmail).FullName));

                Model = paramValue;

                // when posting forms, make sure the EmailAddress property is validated
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    throw new InvalidOperationException(String.Format(
                        "The EmailAddress property of the {1} '{0}' parameter was invalid " +
                        "('{2}' is not a valid email address).",
                        ParamName, typeof(IModelSigningEmail).FullName, EmailAddress));

                // make sure we're working with a confirmed email address
                EnsureEmailIsConfirmed();

                // set or delete the cookie when posting
                filterContext.HttpContext.SigningEmailAddressCookie(EmailAddress, Model.RememberMe);
            }

            // without action parameter, email is in cookie or temp data
            else
            {
                // get return url from parameter
                ReturnUrl = filterContext.ActionParameters[ReturnUrlParamName] as string;

                // try to get email first from cookie, then from temp data
                EmailAddress = filterContext.HttpContext.SigningEmailAddressCookie()
                    ?? filterContext.Controller.TempData.SigningEmailAddress();

                // make sure we're working with a confirmed email address
                EnsureEmailIsConfirmed();
            }
        }

        private bool ValidateSignOn(ActionExecutingContext filterContext)
        {
            // do not validate unless this is the sign-on page
            if (!IsSignOn || IsPersonSamlUser) return true;

            // allow get actions to let user enter email address
            if (!IsModeled && string.IsNullOrWhiteSpace(EmailAddress)) return true;

            // set the email address on temp data for next page to catch
            filterContext.Controller.TempData.SigningEmailAddress(EmailAddress);

            // determine which url to redirect to
            var url = IsPersonLocalMember
                ? Url.Action(MVC.Identity.SignIn.Get(ReturnUrl))
                : Url.Action(MVC.Identity.SignUp.Get(ReturnUrl));

            // set the result and return
            filterContext.Result = new RedirectResult(url);
            return false;
        }

        private bool ValidateSignIn(ActionExecutingContext filterContext)
        {
            // do not validate unless this is the sign-in page
            if (!IsSignIn || IsPersonLocalMember) return true;

            // delete email from cookie and temp data
            filterContext.HttpContext.SigningEmailAddressCookie(null);
            filterContext.Controller.TempData.SigningEmailAddress(null);

            // redirect to the sign-on page
            var signOnGet = Url.Action(MVC.Identity.SignOn.Get(ReturnUrl));
            filterContext.Result = new RedirectResult(signOnGet);
            return false;
        }

        private bool ValidateSignUp(ActionExecutingContext filterContext)
        {
            // do not validate unless this is the sign-up page
            if (!IsSignUp) return true;

            // to sign up, cannot be a saml user or existing member
            if (Establishment != null &&
                Establishment.IsMember &&
                !IsPersonSamlUser && 
                !IsPersonLocalMember) return true;

            // determine which url to redirect to
            var url = IsPersonLocalMember
                ? Url.Action(MVC.Identity.SignIn.Get(ReturnUrl))
                : Url.Action(MVC.Identity.SignOn.Get(ReturnUrl));

            // set the result and return
            filterContext.Result = new RedirectResult(url);
            return false;
        }

        private void ValidateSignOver(ActionExecutingContext filterContext)
        {
            // do not validate unless this is the sign-over page
            if (!IsSignOver) return;

            // must be impersonating to undo
            var wasSignedInAs = filterContext.HttpContext.Session.WasSignedInAs();
            if (IsSignOverUndo && string.IsNullOrWhiteSpace(wasSignedInAs))
            {
                filterContext.Result = new HttpNotFoundResult();
                return;
            }

            // candidate principal may already be impersonating
            var userName = wasSignedInAs ??
                           filterContext.HttpContext.User.Identity.Name;

            // username must have value
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = QueryProcessor.Execute(
                    new GetUserByNameQuery
                    {
                        Name = userName,
                        EagerLoad = new Expression<Func<User, object>>[]
                        {
                            u => u.Grants.Select(g => g.Role)
                        },
                    }
                );

                // valid when user is in the authentication agent role
                if (user != null && user.IsInRole(RoleName.AuthenticationAgent))
                    return;
            }

            // set the result and return
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}