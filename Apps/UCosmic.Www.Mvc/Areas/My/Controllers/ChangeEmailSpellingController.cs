using System;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    [Authorize]
    public partial class ChangeEmailSpellingController : BaseController
    {
        private readonly ChangeEmailSpellingServices _services;

        public ChangeEmailSpellingController(ChangeEmailSpellingServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-spelling")]
        [ReturnUrlReferrer(SelfRouteMapper.Me.OutboundRoute)]
        public virtual ActionResult Get(int number)
        {
            // get the currently signed-in user / person
            var email = _services.QueryProcessor.Execute(
                new GetEmailAddressByUserNameAndNumberQuery
                {
                    UserName = User.Identity.Name,
                    Number = number,
                }
            );

            if (email != null) return PartialView(Mapper.Map<ChangeEmailSpellingForm>(email));
            return HttpNotFound();
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-spelling")]
        public virtual ActionResult Put(ChangeEmailSpellingForm model)
        {
            // make sure user owns this email address
            if (model == null || !User.Identity.Name.Equals(model.PersonUserName, StringComparison.OrdinalIgnoreCase))
                return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return View(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<ChangeEmailAddressSpellingCommand>(model);
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
            [CustomizeValidator(Properties = ChangeEmailSpellingForm.ValuePropertyName)] ChangeEmailSpellingForm model)
        {
            return ValidateRemote(JsonRequestBehavior.DenyGet, ChangeEmailSpellingForm.ValuePropertyName);
        }
    }
}
