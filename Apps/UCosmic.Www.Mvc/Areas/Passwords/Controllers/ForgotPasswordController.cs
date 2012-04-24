using System.Web.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.Passwords.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    public partial class ForgotPasswordController : BaseController
    {
        private readonly ForgotPasswordServices _services;

        public ForgotPasswordController(ForgotPasswordServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("forgot-password")]
        [ReturnUrlReferrer(SignOnRouteMapper.Begin.Route)]
        public virtual PartialViewResult Get()
        {
            var model = new ForgotPasswordForm();
            return PartialView(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("forgot-password")]
        public virtual ActionResult Post(ForgotPasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<SendPasswordResetMessageCommand>(model);
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(string.Format(SuccessMessageFormat, model.EmailAddress));
            return RedirectToAction(MVC.Identity.SignUp.ConfirmEmail(command.ConfirmationToken, null));
        }

        public const string SuccessMessageFormat = "A password reset email has been sent to {0}.";

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = ForgotPasswordForm.EmailAddressPropertyName)] ForgotPasswordForm model)
        {
            return ValidateRemote(ForgotPasswordForm.EmailAddressPropertyName);
        }
    }
}
