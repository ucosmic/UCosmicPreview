using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class UpdatePasswordServices
    {
        public UpdatePasswordServices(IProcessQueries queryProcessor
            , IStorePasswords passwords
        )
        {
            QueryProcessor = queryProcessor;
            Passwords = passwords;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IStorePasswords Passwords { get; private set; }
    }

    [Authorize]
    public partial class UpdatePasswordController : BaseController
    {
        private readonly UpdatePasswordServices _services;

        public UpdatePasswordController(UpdatePasswordServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-password")]
        [ReturnUrlReferrer(MyHomeRouter.GetRoute.MyHomeUrl)]
        public virtual ActionResult Get()
        {
            // get the user
            var user = _services.QueryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = User.Identity.Name,
                }
            );

            // only local members can change passwords
            if (user.EduPersonTargetedId != null ||
                !_services.Passwords.Exists(User.Identity.Name))
                return RedirectToAction(MVC.Identity.MyHome.Get());

            // create view model
            var model = new UpdatePasswordForm();

            // return partial view
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ValidateCurrentPassword(
            [CustomizeValidator(Properties = UpdatePasswordForm.CurrentPasswordPropertyName)] UpdatePasswordForm model)
        {
            return ValidateRemote(UpdatePasswordForm.CurrentPasswordPropertyName);
        }

        [HttpPost]
        public virtual JsonResult ValidateNewPasswordConfirmation(
            [CustomizeValidator(Properties = UpdatePasswordForm.NewPasswordConfirmationPropertyName)] UpdatePasswordForm model)
        {
            return ValidateRemote(UpdatePasswordForm.NewPasswordConfirmationPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-password")]
        public virtual ActionResult Post(UpdatePasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // get the user
            var user = _services.QueryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = User.Identity.Name,
                }
            );

            // only local members can change passwords
            if (user.EduPersonTargetedId != null ||
                !_services.Passwords.Exists(User.Identity.Name))
                return RedirectToAction(MVC.Identity.MyHome.Get());

            // update the password
            _services.Passwords.Update(User.Identity.Name, model.CurrentPassword, model.NewPassword);

            // reset the invalid password attempt window
            Session.FailedPasswordAttempts(false);

            // set feedback message
            SetFeedbackMessage(SuccessMessage);

            // redirect to return url
            return Redirect(model.ReturnUrl ?? Url.Action(MVC.Identity.MyHome.Get()));
        }

        public const string SuccessMessage = "Your password has been changed. Use your new password to sign on next time.";
    }

    public static class UpdatePasswordRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.UpdatePassword.Name;

        public class GetRoute : Route
        {
            public GetRoute()
                : base("my/password", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdatePassword.ActionNames.Get,
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
                    action = MVC.Identity.UpdatePassword.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ValidateCurrentPasswordRoute : Route
        {
            public ValidateCurrentPasswordRoute()
                : base("my/password/validate", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdatePassword.ActionNames.ValidateCurrentPassword,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ValidateNewPasswordConfirmationRoute : Route
        {
            public ValidateNewPasswordConfirmationRoute()
                : base("my/password/validate/new", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdatePassword.ActionNames.ValidateNewPasswordConfirmation,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
