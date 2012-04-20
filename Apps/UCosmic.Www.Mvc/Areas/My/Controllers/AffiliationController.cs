using System.Web.Mvc;
using AutoMapper;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.My.Models;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public partial class AffiliationController : BaseController
    {
        private readonly AffiliationServices _services;

        public AffiliationController(AffiliationServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("affiliation")]
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
            return PartialView(Mapper.Map<AffiliationForm>(affiliation));
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("affiliation")]
        public virtual ActionResult Put(AffiliationForm model)
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
