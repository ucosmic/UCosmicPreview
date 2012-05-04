using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.My.Controllers;
using UCosmic.Www.Mvc.Areas.Passwords.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    public class UpdatePasswordServices
    {
        public UpdatePasswordServices(IProcessQueries queryProcessor
            , ISignMembers memberSigner
        )
        {
            QueryProcessor = queryProcessor;
            MemberSigner = memberSigner;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public ISignMembers MemberSigner { get; private set; }
    }

    [Authorize]
    [EnforceHttps]
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
        [ReturnUrlReferrer(ProfileRouter.Get.Route)]
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
                !_services.MemberSigner.IsSignedUp(User.Identity.Name))
                return RedirectToAction(MVC.My.Profile.Get());

            // create view model
            var model = new UpdatePasswordForm();

            // return partial view
            return PartialView(model);
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

            if (!ModelState.IsValid) return PartialView(model);

            // get the user
            var user = _services.QueryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = User.Identity.Name,
                }
            );

            // only local members can change passwords
            if (user.EduPersonTargetedId != null ||
                !_services.MemberSigner.IsSignedUp(User.Identity.Name))
                return RedirectToAction(MVC.My.Profile.Get());

            // update the password
            _services.MemberSigner.UpdatePassword(User.Identity.Name, model.CurrentPassword, model.NewPassword);

            // reset the invalid password attempt window
            Session.FailedPasswordAttempts(false);

            // set feedback message
            SetFeedbackMessage(SuccessMessage);

            // redirect to return url
            return Redirect(model.ReturnUrl ?? Url.Action(MVC.My.Profile.Get()));
        }

        public const string SuccessMessage = "Your password has been changed. Use your new password to sign on next time.";
    }

    public static class UpdatePasswordRouter
    {
        private static readonly string Area = MVC.Passwords.Name;
        private static readonly string Controller = MVC.Passwords.UpdatePassword.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(UpdatePasswordRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/password";
            private static readonly string Action = MVC.Passwords.UpdatePassword.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Post
        {
            public const string Route = Get.Route;
            private static readonly string Action = MVC.Passwords.UpdatePassword.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateCurrentPassword
        {
            public const string Route = "my/password/validate";
            private static readonly string Action = MVC.Passwords.UpdatePassword.ActionNames.ValidateCurrentPassword;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateNewPasswordConfirmation
        {
            public const string Route = "my/password/validate/new";
            private static readonly string Action = MVC.Passwords.UpdatePassword.ActionNames.ValidateNewPasswordConfirmation;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
