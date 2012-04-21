using System;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    [Authorize]
    public partial class UpdateEmailValueController : BaseController
    {
        private readonly UpdateEmailValueServices _services;

        public UpdateEmailValueController(UpdateEmailValueServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-email-value")]
        [ReturnUrlReferrer(ProfileRouter.Get.Route)]
        public virtual ActionResult Get(int number)
        {
            // get the email address
            var email = _services.QueryProcessor.Execute(
                new GetMyEmailAddressByNumberQuery
                {
                    Principal = User,
                    Number = number,
                }
            );

            if (email == null) return HttpNotFound();
            return PartialView(Mapper.Map<UpdateEmailValueForm>(email));
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-email-value")]
        public virtual ActionResult Put(UpdateEmailValueForm model)
        {
            // make sure user owns this email address
            if (model == null || !User.Identity.Name.Equals(model.PersonUserName, StringComparison.OrdinalIgnoreCase))
                return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<UpdateMyEmailValueCommand>(model);
            command.Principal = User;
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(command.ChangedState
                ? string.Format(SuccessMessageFormat, model.Value)
                : NoChangesMessage
            );
            return Redirect(model.ReturnUrl);
        }

        public const string SuccessMessageFormat = "Your email address was successfully changed to {0}.";
        public const string NoChangesMessage = "No changes were made.";

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult ValidateValue(
            [CustomizeValidator(Properties = UpdateEmailValueForm.ValuePropertyName)] UpdateEmailValueForm model)
        {
            return ValidateRemote(UpdateEmailValueForm.ValuePropertyName);
        }
    }
}
