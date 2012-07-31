using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignUpServices
    {
        public SignUpServices(IHandleCommands<SendCreatePasswordMessageCommand> commandHandler)
        {
            CommandHandler = commandHandler;
        }

        public IHandleCommands<SendCreatePasswordMessageCommand> CommandHandler { get; private set; }
    }

    public partial class SignUpController : BaseController
    {
        private readonly SignUpServices _services;

        public SignUpController(SignUpServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("sign-up")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateSigningEmail]
        public virtual ActionResult Get(string returnUrl)
        {
            var model = new SignUpForm(HttpContext, TempData, returnUrl);
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("sign-up")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateAntiForgeryToken]
        [ValidateSigningEmail(ParamName = "model")]
        public virtual ActionResult Post(SignUpForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // execute command
            var command = Mapper.Map<SendCreatePasswordMessageCommand>(model);
            command.SendFromUrl = Url.Action(MVC.Identity.SignOn.Get());
            _services.CommandHandler.Handle(command);

            // flash feedback message
            SetFeedbackMessage(string.Format(SuccessMessageFormat, model.EmailAddress));

            // redirect to confirm email
            return RedirectToRoute(new
            {
                area = MVC.Identity.Name,
                controller = MVC.Identity.ConfirmEmail.Name,
                action = MVC.Identity.ConfirmEmail.ActionNames.Get,
                token = command.ConfirmationToken,
            });
        }

        public const string SuccessMessageFormat = "A sign up confirmation email has been sent to {0}.";
    }

    public static class SignUpRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignUp.Name;

        public class GetRoute : Route
        {
            public GetRoute()
                : base("sign-up", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignUp.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
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
                    action = MVC.Identity.SignUp.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
