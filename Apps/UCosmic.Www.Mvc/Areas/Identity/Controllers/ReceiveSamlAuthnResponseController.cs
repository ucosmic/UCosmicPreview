using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ReceiveSamlAuthnResponseServices
    {
        public ReceiveSamlAuthnResponseServices(
            IHandleCommands<SignOnSamlUserCommand> commandHandler
        )
        {
            CommandHandler = commandHandler;
        }

        public IHandleCommands<SignOnSamlUserCommand> CommandHandler { get; private set; }

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
        [UnitOfWork]
        public virtual ActionResult Post()
        {
            var command = new SignOnSamlUserCommand
            {
                SsoBinding = Saml2SsoBinding.HttpPost,
                HttpContext = HttpContext,
            };

            _services.CommandHandler.Handle(command);

            return Redirect(command.ReturnUrl);
        }
    }

    public static class ReceiveSamlAuthnResponseRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ReceiveSamlAuthnResponse.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(SignOnRouter), context, Area, Controller);
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
