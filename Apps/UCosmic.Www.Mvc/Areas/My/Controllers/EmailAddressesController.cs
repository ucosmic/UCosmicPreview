using System.Web.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses;
using UCosmic.Www.Mvc.Areas.My.Services;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public partial class EmailAddressesController : BaseController
    {
        #region Construction & DI

        private readonly EmailAddressesServices _services;

        public EmailAddressesController(EmailAddressesServices services)
        {
            _services = services;
        }

        #endregion
        #region Change Email Spelling

        [HttpGet]
        [Authorize]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-spelling")]
        [ReturnUrlReferrer(SelfRouteMapper.Me.OutboundRoute)]
        public virtual ActionResult ChangeSpelling(int number)
        {
            // get the currently signed-in user / person
            var email = _services.QueryProcessor.Execute(
                new GetEmailAddressByUserNameAndNumberQuery
                {
                    UserName = User.Identity.Name,
                    Number = number,
                }
            );

            if (email != null) return View(Mapper.Map<ChangeSpellingForm>(email));
            return HttpNotFound();
        }

        [HttpPut]
        [Authorize]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-spelling")]
        public virtual ActionResult ChangeSpelling(ChangeSpellingForm model)
        {
            // make sure user owns this email address
            if (model == null || model.PersonUserName != User.Identity.Name) return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return View(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<ChangeEmailAddressSpellingCommand>(model);
            _services.ChangeEmailAddressSpellingHandler.Handle(command);
            SetFeedbackMessage(command.ChangedState
                ? string.Format("Your email address was successfully changed to {0}.", model.Value)
                : "No changes were made."
            );
            return Redirect(model.ReturnUrl);
        }

        [HttpPost]
        [Authorize]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual ActionResult ValidateChangeSpelling(
            [CustomizeValidator(Properties = ChangeSpellingForm.ValuePropertyName)] ChangeSpellingForm model)
        {
            return ValidateRemote(JsonRequestBehavior.DenyGet, ChangeSpellingForm.ValuePropertyName);
        }

        #endregion
    }
}
