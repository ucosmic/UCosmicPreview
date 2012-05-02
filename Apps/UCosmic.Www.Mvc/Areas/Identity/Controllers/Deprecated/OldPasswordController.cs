using System;
using System.Web.Mvc;
using System.Web.Security;
using UCosmic.Domain;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models.Password;
using UCosmic.Www.Mvc.Areas.My.Controllers;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    public partial class OldPasswordController : BaseController
    {
        #region Construction & DI

        private readonly PersonFinder _people;
        private readonly ICommandObjects _objectCommander;
        private readonly EmailComposer _emailsComposer;
        private readonly ISendEmails _emailsSender;
        private readonly ILogExceptions _exceptionLogger;
        private readonly IManageConfigurations _config;

        public OldPasswordController(IQueryEntities entityQueries, ICommandObjects objectCommander, 
            ISendEmails emailsSender, ILogExceptions exceptionLogger, IManageConfigurations config)
        {
            _people = new PersonFinder(entityQueries);
            _objectCommander = objectCommander;
            _emailsComposer = new EmailComposer(entityQueries, config);
            _emailsSender = emailsSender;
            _exceptionLogger = exceptionLogger;
            _config = config;
        }

        #endregion
        #region Change Password

        [HttpGet]
        [Authorize]
        //[OpenTopTab(TopTabName.User)]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-password")]
        [ReturnUrlReferrer(ProfileRouter.Get.Route)]
        public virtual ActionResult ChangePassword()
        {
            var currentUser = Membership.GetUser(User.Identity.Name, true);
            if (currentUser != null)
            {
                var model = new ChangePasswordForm();
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        //[OpenTopTab(TopTabName.User)]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-password")]
        public virtual ActionResult ChangePassword(ChangePasswordForm model)
        {
            var currentUser = Membership.GetUser(User.Identity.Name, true);
            if (currentUser != null)
            {
                if (ModelState.IsValid)
                {
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        SetFeedbackMessage("Your password has been changed successfully.");
                        return RedirectToAction(MVC.My.Profile.Get());
                    }

                    ModelState.AddModelError("OldPassword", "The Old Password is incorrect.");
                }
                return View(model);
            }
            return HttpNotFound();
        }

        #endregion
        #region Forgot Password

        //[HttpGet]
        //[ActionName("forgot-password")]
        ////[OpenTopTab(TopTabName.SignIn)]
        //[OpenTopTab(TopTabName.Home)]
        //[ReturnUrlReferrer(SignInRouteMapper.SignIn.Route)]
        //public virtual ActionResult ForgotPassword()
        //{
        //    var model = new ForgotPasswordForm();
        //    return View(model);
        //}

        //[HttpPost]
        //[UnitOfWork]
        //[ValidateAntiForgeryToken]
        //[ActionName("forgot-password")]
        ////[OpenTopTab(TopTabName.SignIn)]
        //[OpenTopTab(TopTabName.Home)]
        //public virtual ActionResult ForgotPassword(ForgotPasswordForm model)
        //{
        //    // do nothing without a model
        //    if (model != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                // search for email address
        //                var person = _people.FindOne(PersonBy.EmailAddress(model.EmailAddress));
        //                var email = (person != null) ? person.Emails.ByValue(model.EmailAddress) : null;

        //                // create & add new confirmation
        //                var confirmation = new EmailConfirmation
        //                {
        //                    Intent = EmailConfirmationIntent.PasswordReset,
        //                    SecretCode = RandomSecretCreator.CreateSecret(12),
        //                };
        //                email.Confirmations.Add(confirmation);
        //                //_people.Refresh(person);
        //                _objectCommander.Update(person);

        //                // create, add, & send email message
        //                // TODO: Create new way to compose email
        //                var message = _emailsComposer.ComposeEmail(EmailTemplateName.PasswordResetConfirmation,
        //                    null, confirmation.GetMessageVariables(_config)); // TODO: GetMessageVariables into extension method
        //                person.Messages.Add(message);
        //                _objectCommander.SaveChanges();
        //                _emailsSender.Send(message);
        //                return RedirectToAction(MVC.Identity.EmailConfirmation.ConfirmForPasswordReset(confirmation.Token, null));
        //            }
        //            catch (Exception ex)
        //            {
        //                _exceptionLogger.LogException(ex);
        //                return null;
        //            }

        //        }
        //        return View(MVC.Identity.Password.Views.forgot_password, model);
        //    }
        //    return HttpNotFound();
        //}

        #endregion
        #region Reset Password

        //[HttpGet]
        //[ActionName("reset-password")]
        ////[OpenTopTab(TopTabName.SignIn)]
        //[OpenTopTab(TopTabName.Home)]
        //public virtual ActionResult ResetPassword()
        //{
        //    // confirmation token must be passed from email confirmation screen
        //    var confirmationToken = TempData["ConfirmationToken"] as Guid?;
        //    if (confirmationToken.HasValue && confirmationToken.Value != Guid.Empty)
        //    {
        //        TempData.Keep("ConfirmationToken");
        //        var confirmation = _people.FindOne(
        //            PersonBy.EmailConfirmation(confirmationToken.Value, EmailConfirmationIntent.PasswordReset)
        //                .EagerLoad(p => p.Emails.Select(e => e.Confirmations))
        //            )
        //            .Emails.SelectManyConfirmations().ByToken(confirmationToken.Value);
        //        if (confirmation != null && confirmation.RedeemedOnUtc.HasValue 
        //            && confirmation.EmailAddress.IsConfirmed)
        //        {
        //            // return viewmodel for password creation
        //            var model = Mapper.Map<ResetPasswordForm>(confirmation);
        //            return View(model);
        //        }
        //    }
        //    return View(Views.reset_password_expired);
        //}

        //[HttpPost]
        //[UnitOfWork]
        //[ActionName("reset-password")]
        ////[OpenTopTab(TopTabName.SignIn)]
        //[OpenTopTab(TopTabName.Home)]
        //public virtual ActionResult ResetPassword(ResetPasswordForm model)
        //{
        //    var currentUser = Membership.GetUser(model.EmailAddressValue, true);
        //    if (currentUser != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            bool changePasswordSucceeded;
        //            try
        //            {
        //                changePasswordSucceeded = currentUser.ChangePassword(currentUser.ResetPassword(), model.Password);
        //            }
        //            catch (Exception)
        //            {
        //                changePasswordSucceeded = false;
        //            }

        //            if (changePasswordSucceeded)
        //            {
        //                SetFeedbackMessage("Your password has been reset, please sign in.");
        //                return RedirectToAction(MVC.My.Profile.Get());
        //            }
        //        }
        //        return View(model);
        //    }
        //    return HttpNotFound();
        //}

        #endregion
    }
}
