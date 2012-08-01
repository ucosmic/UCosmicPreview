using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class CreatePasswordServices
    {
        public CreatePasswordServices(IProcessQueries queryProcessor
            , IHandleCommands<CreatePasswordCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<CreatePasswordCommand> CommandHandler { get; private set; }
    }

    public partial class CreatePasswordController : BaseController
    {
        private readonly CreatePasswordServices _services;

        public CreatePasswordController(CreatePasswordServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("create-password")]
        [ValidateRedeemTicket("token", EmailConfirmationIntent.CreatePassword)]
        public virtual ActionResult Get(Guid token)
        {
            // skip when there is an empty token
            if (token == Guid.Empty) return HttpNotFound();

            // get the confirmation from the db
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(token)
            );
            if (confirmation == null) return HttpNotFound();

            // convert confirmation to form
            var model = Mapper.Map<CreatePasswordForm>(confirmation);

            // return partial view
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ValidatePasswordConfirmation(
            [CustomizeValidator(Properties = CreatePasswordForm.PasswordConfirmationPropertyName)] CreatePasswordForm model)
        {
            return ValidateRemote(CreatePasswordForm.PasswordConfirmationPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("create-password")]
        [ValidateRedeemTicket("model", EmailConfirmationIntent.CreatePassword)]
        public virtual ActionResult Post(CreatePasswordForm model)
        {
            if (model == null) return HttpNotFound();
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(model.Token)
            );
            if (confirmation == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // execute command
            var command = Mapper.Map<CreatePasswordCommand>(model);
            command.Ticket = TempData.EmailConfirmationTicket();
            _services.CommandHandler.Handle(command);

            // clear the ticket & set the email
            TempData.EmailConfirmationTicket(null);
            TempData.SigningEmailAddress(confirmation.EmailAddress.Value);

            // set feedback message
            SetFeedbackMessage(SuccessMessage);

            // redirect to sign on
            return RedirectToAction(MVC.Identity.SignIn.Get());
        }

        public const string SuccessMessage = "You can now use your password to sign on.";
    }

    public static class CreatePasswordRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.CreatePassword.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "create-password/{token}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.CreatePassword.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    token = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class PostRoute : GetRoute
        {
            public PostRoute()
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.CreatePassword.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    token = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class ValidatePasswordConfirmationRoute : MvcRoute
        {
            public ValidatePasswordConfirmationRoute()
            {
                Url = "create-password/validate";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.CreatePassword.ActionNames.ValidatePasswordConfirmation,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
