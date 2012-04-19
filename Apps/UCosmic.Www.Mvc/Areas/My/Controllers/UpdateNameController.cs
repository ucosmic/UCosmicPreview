using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    [Authorize]
    public partial class UpdateNameController : BaseController
    {
        private readonly UpdateNameServices _services;

        public UpdateNameController(UpdateNameServices services)
        {
            _services = services;
        }

        [HttpGet]
        [NullLayoutOnChildAction]
        [ActionName("update-name")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Get()
        {
            var user = _services.QueryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = User.Identity.Name,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.Person.Emails,
                    }
                }
            );

            if (user == null) return HttpNotFound();
            return PartialView(Mapper.Map<UpdateNameForm>(user.Person));
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-name")]
        public virtual ActionResult Put(UpdateNameForm model)
        {
            // make sure model is not null
            if (model == null) return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<UpdateNameCommand>(model);
            command.Principal = User;
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(command.ChangedState
                ? SuccessMessage
                : NoChangesMessage
            );
            return Redirect(UpdateNameForm.ReturnUrl);
        }

        public const string SuccessMessage = "Your info was successfully updated.";
        public const string NoChangesMessage = "No changes were made.";

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult GenerateDisplayName(UpdateNameForm model)
        {
            var query = Mapper.Map<GenerateDisplayNameQuery>(model);
            var displayName = _services.QueryProcessor.Execute(query);
            return Json(displayName);
        }
    }
}
