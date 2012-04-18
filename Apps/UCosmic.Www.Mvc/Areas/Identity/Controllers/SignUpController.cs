using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.Email;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignIn;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignUp;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    public partial class SignUpController : BaseController
    {
        #region Construction & DI

        private readonly PersonFinder _people;
        private readonly EstablishmentFinder _establishments;
        private readonly EmailTemplateFinder _emailTemplates;
        private readonly ISendEmails _emailSender;
        private readonly IManageConfigurations _configurationManager;
        private readonly ISignMembers _memberSigner;
        private readonly ICommandObjects _objectCommander;
        public const string ConfirmationTokenKey = "ConfirmationToken";

        public SignUpController(IQueryEntities entityQueries
            , ICommandObjects objectCommander
            , ISendEmails emailSender
            , IManageConfigurations configurationManager
            , ISignMembers memberSigner
        )
        {
            _people = new PersonFinder(entityQueries);
            _establishments = new EstablishmentFinder(entityQueries);
            _emailTemplates = new EmailTemplateFinder(entityQueries);
            _emailSender = emailSender;
            _configurationManager = configurationManager;
            _memberSigner = memberSigner;
            _objectCommander = objectCommander;
        }

        #endregion
        #region Send Email

        [HttpPost]
        public virtual JsonResult ValidateSendEmail(string emailAddress)
        {
            var model = new SendEmailForm { EmailAddress = emailAddress, };
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);
            return isValid
                ? Json(true)
                : Json(results[0].ErrorMessage);
        }

        [HttpGet]
        [ActionName("send-email")]
        //[OpenTopTab(TopTabName.SignUp)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ViewResult SendEmail()
        {
            var model = new SendEmailForm();
            return View(model);
        }

        [HttpPost]
        [ActionName("send-email")]
        //[OpenTopTab(TopTabName.SignUp)]
        [OpenTopTab(TopTabName.Home)]
        [ValidateAntiForgeryToken(Salt = SignUpRouteMapper.SendEmail.Route)]
        public virtual ActionResult SendEmail(SendEmailForm model)
        {
            // do nothing without a model
            if (model == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
                return View(model);

            // establishment must exist if model is valid
            var establishment = _establishments
                .FindOne(EstablishmentBy.EmailDomain(model.EmailAddress)
                    .EagerLoad(e => e.Type.Category)
                    .ForInsertOrUpdate()
                );

            // need an email template to send the confirmation message
            var template = _emailTemplates.FindOne(EmailTemplateBy.Name(
                EmailTemplateName.SignUpConfirmation, establishment.RevisionId, true)
                .ForInsertOrUpdate()
            );

            // person may already exist
            var person = _people.FindOne(PersonBy.EmailAddress(model.EmailAddress)  // get the person
                .EagerLoad(p => p.Emails.Select(e => e.Confirmations))              // eager load collections
                .EagerLoad(p => p.Messages)                                         // needed for this process to
                .EagerLoad(p => p.Affiliations.Select(a => a.Establishment))        // avoid extra db calls
                .ForInsertOrUpdate()                                                // track the entity on the unit of work
            )

            // create person if it does not exist
            //?? new Person
            //{
            //    DisplayName = model.EmailAddress,
            //};
            ?? new Person(); // display name no longer set here

            person.AffiliateWith(establishment);
            var emailAddress = person.AddEmail(model.EmailAddress);
            var confirmation = emailAddress.AddConfirmation(EmailConfirmationIntent.SignUp);
            var message = confirmation.ComposeConfirmationMessage(template,
                _configurationManager.SignUpUrl,
                string.Format(_configurationManager.SignUpEmailConfirmationUrlFormat,
                    confirmation.Token, HttpUtility.UrlEncode(confirmation.SecretCode)), _configurationManager);

            // update the db and send email
            if (person.RevisionId == 0)
                _objectCommander.Insert(person, true);
            else
                _objectCommander.Update(person, true);
            //_people.InsertOrUpdate(person);
            //_people.UnitOfWork.SaveChanges();
            _emailSender.Send(message);

            SetFeedbackMessage(string.Format("A confirmation email has been sent to {0}", emailAddress.Value));
            return RedirectToAction(MVC.Identity.SignUp.ConfirmEmail(confirmation.Token, null));
        }

        #endregion
        #region Confirm Email

        [HttpPost]
        public virtual JsonResult ValidateConfirmEmail(Guid token, string secretCode)
        {
            var model = new ConfirmEmailForm { SecretCode = secretCode, Token = token, };
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true);
            return isValid
                ? Json(true)
                : Json(results[0].ErrorMessage);
        }

        [HttpGet]
        [ActionName("confirm-email")]
        //[OpenTopTab(TopTabName.SignUp)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult ConfirmEmail(Guid token, string secretCode)
        {
            if (token != Guid.Empty)
            {
                var confirmation = GetSignUpConfirmation(token);
                if (confirmation != null)
                {
                    var deniedView = GetConfirmEmailDeniedView(confirmation);
                    if (deniedView != null)
                        return deniedView;

                    var model = new ConfirmEmailForm
                    {
                        Token = token,
                        SecretCode = secretCode,
                        IsUrlConfirmation = (!string.IsNullOrWhiteSpace(secretCode)),
                    };

                    return View(model);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("confirm-email")]
        //[OpenTopTab(TopTabName.SignUp)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult ConfirmEmail(ConfirmEmailForm model)
        {
            if (model != null && model.Token != Guid.Empty)
            {
                model.SecretCode = (model.SecretCode != null) ? model.SecretCode.Trim() : null;
                if (!ModelState.IsValid)
                {
                    // chances are this was not a URL confirmation
                    if (!model.IsUrlConfirmation)
                        model.SecretCode = null;
                    return View(model);
                }

                var confirmation = GetSignUpConfirmation(model.Token, true);
                if (confirmation != null)
                {
                    var deniedView = GetConfirmEmailDeniedView(confirmation);
                    if (deniedView != null)
                        return deniedView;

                    confirmation.EmailAddress.Confirm(model.Token,
                        EmailConfirmationIntent.SignUp, model.SecretCode);
                    _objectCommander.Update(confirmation.EmailAddress.Person, true);
                    //_people.InsertOrUpdate(confirmation.EmailAddress.Person);
                    //_people.UnitOfWork.SaveChanges();

                    TempData[ConfirmationTokenKey] = confirmation.Token;
                    SetFeedbackMessage("Your email address was successfully confirmed");
                    return RedirectToAction(MVC.Identity.SignUp.CreatePassword());
                }
            }
            return HttpNotFound();
        }

        [NonAction]
        private EmailConfirmation GetSignUpConfirmation(Guid token, bool forInsertOrUpdate = false)
        {
            // make sure the token exists and has not expired
            var criteria = PersonBy.EmailConfirmation(token, EmailConfirmationIntent.SignUp);
            if (forInsertOrUpdate)
                criteria.ForInsertOrUpdate();
            var person = _people.FindOne(criteria);
            if (person != null)
            {
                var confirmation = person.Emails.SelectManyConfirmations()
                    .SingleOrDefault(c => c.Token == token && c.Intent == EmailConfirmationIntent.SignUp);
                if (confirmation != null)
                    return confirmation;
            }
            return null;
        }

        [NonAction]
        private ViewResult GetConfirmEmailDeniedView(EmailConfirmation confirmation)
        {
            var deniedView = MVC.Identity.SignUp.Views.confirm_denied;

            // confirmations can only be redeemed within 2 hours of being registered.
            if (confirmation.IsExpired)
                return View(deniedView, new ConfirmDeniedPage(ConfirmDeniedPage.DeniedBecause.ConfirmationIsExpired));

            // when person is already registered, a confirmation has already been redeemed.
            if (confirmation.EmailAddress.Person.User != null
                && confirmation.EmailAddress.Person.User.IsRegistered)
                return View(deniedView, new ConfirmDeniedPage(ConfirmDeniedPage.DeniedBecause.UserIsSignedUp));

            if (_memberSigner.IsSignedUp(confirmation.EmailAddress.Value))
                return View(deniedView, new ConfirmDeniedPage(ConfirmDeniedPage.DeniedBecause.MemberIsSignedUp));

            // when person is not registered but confirmation has already been redeemed
            if (confirmation.ConfirmedOnUtc.HasValue)
                return View(deniedView, new ConfirmDeniedPage(ConfirmDeniedPage.DeniedBecause.ConfirmationIsRedeemed));

            return null;
        }

        #endregion
        #region Create Password

        [HttpGet]
        [ActionName("create-password")]
        //[OpenTopTab(TopTabName.SignUp)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult CreatePassword()
        {
            // need to get fresh data each time, to make sure user is not already registered
            var token = TempData[ConfirmationTokenKey] as Guid?;
            if (token.HasValue && token.Value != Guid.Empty)
            {
                TempData.Keep(ConfirmationTokenKey);
                var confirmation = GetSignUpConfirmation(token.Value);
                var deniedView = GetCreatePasswordDeniedView(confirmation);
                if (deniedView != null)
                    return deniedView;

                var model = new CreatePasswordForm();
                return View(model);
            }
            TempData.Remove(ConfirmationTokenKey);
            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("create-password")]
        //[OpenTopTab(TopTabName.SignUp)]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult CreatePassword(CreatePasswordForm model)
        {
            // need to get fresh data each time, to make sure user is not already registered
            var token = TempData[ConfirmationTokenKey] as Guid?;
            if (token.HasValue && token.Value != Guid.Empty && model != null)
            {
                TempData.Keep(ConfirmationTokenKey);
                if (!ModelState.IsValid)
                    return View(model);

                var confirmation = GetSignUpConfirmation(token.Value, true);
                var deniedView = GetCreatePasswordDeniedView(confirmation);
                if (deniedView != null)
                    return deniedView;

                var user = confirmation.EmailAddress.Person.SignUp(confirmation.EmailAddress);
                _objectCommander.Update(confirmation.EmailAddress.Person);
                _memberSigner.SignUp(user.Name, model.Password);
                _objectCommander.SaveChanges();

                SetFeedbackMessage("Your password was created successfully");
                return RedirectToAction(MVC.Identity.SignUp.SignIn());
            }
            TempData.Remove(ConfirmationTokenKey);
            return HttpNotFound();
        }

        [NonAction]
        private ViewResult GetCreatePasswordDeniedView(EmailConfirmation confirmation)
        {
            var deniedView = MVC.Identity.SignUp.Views.create_denied;

            // confirmation should not be null at this point, but protect against it
            if (confirmation == null || !confirmation.EmailAddress.IsConfirmed
                || !confirmation.ConfirmedOnUtc.HasValue)
                return View(deniedView, new CreateDeniedPage(CreateDeniedPage.DeniedBecause.ConfirmationDoesNotExist));

            // passwords can only be created within the expiration window
            if (confirmation.IsExpired)
                return View(deniedView, new CreateDeniedPage(CreateDeniedPage.DeniedBecause.ConfirmationIsExpired));

            // when person is already registered cannot create member & password
            if (confirmation.EmailAddress.Person.User != null
                && confirmation.EmailAddress.Person.User.IsRegistered)
                return View(deniedView, new CreateDeniedPage(CreateDeniedPage.DeniedBecause.UserIsSignedUp));

            if (_memberSigner.IsSignedUp(confirmation.EmailAddress.Value))
                return View(deniedView, new CreateDeniedPage(CreateDeniedPage.DeniedBecause.MemberIsSignedUp));

            return null;
        }

        #endregion
        #region Sign In

        [HttpGet]
        [ActionName("sign-in")]
        public virtual ActionResult SignIn()
        {
            var token = TempData[ConfirmationTokenKey] as Guid?;
            if (token.HasValue && token.Value != Guid.Empty)
            {
                var confirmation = GetSignUpConfirmation(token.Value);
                if (confirmation != null)
                {
                    TempData.Keep(ConfirmationTokenKey);
                    var model = new SignInForm
                    {
                        EmailAddress = confirmation.EmailAddress.Value,
                    };
                    return View(model);
                }
            }
            TempData.Remove(ConfirmationTokenKey);
            return RedirectToAction(MVC.Identity.SignIn.SignIn());
        }

        #endregion
    }
}
