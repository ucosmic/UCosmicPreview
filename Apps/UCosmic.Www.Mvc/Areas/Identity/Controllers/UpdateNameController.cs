using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class UpdateNameServices
    {
        public UpdateNameServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyNameCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyNameCommand> CommandHandler { get; private set; }
    }

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

            var model = Mapper.Map<UpdateNameForm>(user.Person);

            if (ControllerContext.IsChildAction) return PartialView(model);
            return View(model);
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
            if (!ModelState.IsValid) return View(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<UpdateMyNameCommand>(model);
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
    }

    public static class UpdateNameRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.UpdateName.Name;

        public class GetRoute : Route
        {
            public GetRoute()
                : base("my/name/edit", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdateName.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PutRoute : Route
        {
            public PutRoute()
                : base("my/name", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdateName.ActionNames.Put,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                });
            }
        }
    }
}
