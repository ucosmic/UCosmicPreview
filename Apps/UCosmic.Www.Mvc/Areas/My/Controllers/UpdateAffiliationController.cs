using System.Web.Mvc;
using AutoMapper;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.My.Models;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    [Authorize]
    public partial class UpdateAffiliationController : BaseController
    {
        private readonly UpdateAffiliationServices _services;

        public UpdateAffiliationController(UpdateAffiliationServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-affiliation")]
        [ReturnUrlReferrer(ProfileRouter.Get.Route)]
        public virtual ActionResult Get(int establishmentId)
        {
            // get the affiliation
            var affiliation = _services.QueryProcessor.Execute(
                new GetMyAffiliationByEstablishmentIdQuery
                {
                    Principal = User,
                    EstablishmentId = establishmentId,
                }
            );

            if (affiliation == null) return HttpNotFound();
            return PartialView(Mapper.Map<UpdateAffiliationForm>(affiliation));
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-affiliation")]
        public virtual ActionResult Put(UpdateAffiliationForm model)
        {
            // make sure model is not null
            if (model == null) return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<UpdateMyAffiliationCommand>(model);
            command.Principal = User;
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(command.ChangedState
                ? SuccessMessage
                : NoChangesMessage
            );
            return Redirect(model.ReturnUrl);
        }

        public const string SuccessMessage = "Your affiliation info was successfully updated.";
        public const string NoChangesMessage = "No changes were made.";
    }
}
