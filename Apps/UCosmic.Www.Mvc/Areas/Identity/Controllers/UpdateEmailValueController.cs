using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class UpdateEmailValueServices
    {
        public UpdateEmailValueServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyEmailValueCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyEmailValueCommand> CommandHandler { get; private set; }
    }

    [Authenticate]
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
        [ReturnUrlReferrer(MyHomeRouter.GetRoute.MyHomeUrl)]
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
            return View(Mapper.Map<UpdateEmailValueForm>(email));
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
            if (!ModelState.IsValid) return View(model);

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
        [OutputCache(VaryByParam = "*", VaryByCustom = VaryByCustomUser, Duration = 1800)]
        public virtual JsonResult ValidateValue(
            [CustomizeValidator(Properties = UpdateEmailValueForm.ValuePropertyName)] UpdateEmailValueForm model)
        {
            return ValidateRemote(UpdateEmailValueForm.ValuePropertyName);
        }
    }

    public static class UpdateEmailValueRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.UpdateEmailValue.Name;

        public class GetRoute : Route
        {
            public GetRoute()
                : base("my/emails/{number}/change-spelling", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdateEmailValue.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }

        public class PutRoute : Route
        {
            public PutRoute()
                : base("my/emails/{number}", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdateEmailValue.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }

        public class ValidateValueRoute : Route
        {
            public ValidateValueRoute()
                : base("my/emails/{number}/change-spelling/validate", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.UpdateEmailValue.ActionNames.ValidateValue,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }
    }
}
