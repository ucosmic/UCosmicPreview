using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class SignUpController : BaseController
    {
        public virtual ActionResult Get()
        {
            return new EmptyResult();
        }

        public virtual ActionResult Post()
        {
            return new EmptyResult();
        }
    }

    public static class SignUpRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignUp.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            if (!WebConfig.IsDeployedToCloud)
                RootActionRouter.RegisterRoutes(typeof(SignUpRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-up";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Post
        {
            public const string Route = Get.Route;
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.Post;
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
