using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models.EmailConfirmation;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    public partial class EmailConfirmationController : Controller
    {
        #region Construction & DI

        private readonly PersonFinder _people;
        private readonly ICommandObjects _objectCommander;

        public EmailConfirmationController(IQueryEntities entityQueries, ICommandObjects objectCommander)
        {
            _people = new PersonFinder(entityQueries);
            _objectCommander = objectCommander;
        }

        #endregion
        #region Confirm Email for Password Reset

        [UnitOfWork]
        //[OpenTopTab(TopTabName.SignIn)]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("confirm-password-reset")]
        public virtual ActionResult ConfirmForPasswordReset(Guid? token, string secretCode)
        {
            // do nothing unless there is a confirmation matching the token
            if (token.HasValue && token.Value != Guid.Empty)
            {
                // invoke service
                var confirmation = ConfirmEmailOwnership(token.Value, secretCode);
                if (confirmation != null)
                {
                    // confirmation may have expired
                    if (confirmation.IsExpired)
                        return View(MVC.Identity.Password.Views.reset_password_expired);

                    // try to validate the secret code
                    if (!string.IsNullOrWhiteSpace(secretCode) && confirmation.SecretCode == secretCode
                        && confirmation.EmailAddress.IsConfirmed)
                    {
                        // redirect to reset password
                        if (confirmation.EmailAddress.Person.User == null
                            || confirmation.EmailAddress.Person.User.IsRegistered)
                        {
                            TempData["ConfirmationToken"] = confirmation.Token;
                            return RedirectToAction(MVC.Identity.Password.ResetPassword());
                        }
                        return Redirect(FormsAuthentication.LoginUrl);
                    }

                    // return view with error message
                    if (!string.IsNullOrWhiteSpace(secretCode))
                    {
                        // TODO: Internationalize this error message
                        ModelState.AddModelError("SecretCode", "Invalid confirmation code.");
                    }

                    var model = Mapper.Map<ConfirmEmailForgotPasswordForm>(confirmation);
                    if (!string.IsNullOrWhiteSpace(model.SecretCode)) model.SecretCode = string.Empty;
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [UnitOfWork]
        //[OpenTopTab(TopTabName.SignIn)]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("confirm-password-reset")]
        public virtual ActionResult ConfirmForPasswordReset(ConfirmEmailForgotPasswordForm model)
        {
            // do nothing unless we have email address, token, and secret
            if (model != null)
            {
                return ModelState.IsValid ? ConfirmForPasswordReset(model.Token, model.SecretCode) : View(model);
            }
            return HttpNotFound();
        }

        #endregion
        #region Private methods

        [NonAction]
        private EmailConfirmation ConfirmEmailOwnership(Guid token, string secretCode)
        {
            // search for confirmation
            var person = _people.FindOne(PersonBy.EmailConfirmation(token, EmailConfirmationIntent.PasswordReset));
            var confirmation = (person != null) ? person.Emails.Confirmations().ByToken(token) : null;
            if (confirmation != null && !confirmation.IsExpired
                && confirmation.Token != Guid.Empty)
            {
                // try to validate the secret code
                if (!string.IsNullOrWhiteSpace(secretCode))
                {
                    secretCode = secretCode.Trim();
                    if (secretCode == confirmation.SecretCode)
                    {
                        // confirm the email address
                        confirmation.EmailAddress.IsConfirmed = true;
                        confirmation.ConfirmedOnUtc = DateTime.UtcNow;
                        //_people.Refresh(confirmation.EmailAddress.Person);
                        _objectCommander.Update(confirmation.EmailAddress.Person);
                    }
                }
            }
            return confirmation;
        }

        #endregion
    }
}
