using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ReceiveSamlAuthnResponseServices
    {
        public ReceiveSamlAuthnResponseServices(IProvideSaml2Service samlServiceProvider
            , IHandleCommands<ReceiveSamlAuthnResponseCommand> commandHandler
        )
        {
            SamlServiceProvider = samlServiceProvider;
            CommandHandler = commandHandler;
        }

        public IProvideSaml2Service SamlServiceProvider { get; private set; }
        public IHandleCommands<ReceiveSamlAuthnResponseCommand> CommandHandler { get; private set; }
    }

    [EnforceHttps]
    public partial class ReceiveSamlAuthnResponseController : BaseController
    {
        private readonly ReceiveSamlAuthnResponseServices _services;

        public ReceiveSamlAuthnResponseController(ReceiveSamlAuthnResponseServices services)
        {
            _services = services;
        }

        [HttpPost]
        public virtual ActionResult Post()
        {
            // use HttpContext to create a SamlResponse
            var samlResponse = _services.SamlServiceProvider
                .ReceiveSamlResponse(Saml2SsoBinding.HttpPost, HttpContext);

            // execute command on the saml response object
            var command = new ReceiveSamlAuthnResponseCommand
            {
                SamlResponse = samlResponse,
            };
            _services.CommandHandler.Handle(command);

            // flash the success message
            SetFeedbackMessage(string.Format(
                SignOnController.SuccessMessageFormat,
                    samlResponse.EduPersonPrincipalName));

            // redirect after sign on
            return Redirect(command.ReturnUrl ?? Url.Action(MVC.Identity.MyHome.Get()));
        }
    }

    public static class ReceiveSamlAuthnResponseRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ReceiveSamlAuthnResponse.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ReceiveSamlAuthnResponseRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Post
        {
            public const string Route = "sign-on/saml/2/post";
            private static readonly string Action = MVC.Identity.ReceiveSamlAuthnResponse.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
