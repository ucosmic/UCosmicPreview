using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class FormServices
    {
        public FormServices(IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class FormController : BaseController
    {
        private readonly FormServices _services;

        public FormController(FormServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("form")]
        [ReturnUrlReferrer(MyHomeRouter.Get.Route)]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Get(int number)
        {
            var model = new Form
            {
                Number = number,
                Mode = ActivityMode.Draft,
            };
            return View(model);
        }

        [HttpPut]
        [ActionName("form")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Put(Form model)
        {
            if (!ModelState.IsValid)
                return View(model);
            return View(model);
        }
    }

    public static class FormRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.Form.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(FormRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/activities/{number}/edit";
            private static readonly string Action = MVC.Activities.Form.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Put
        {
            public const string Route = "my/activities/{number}";
            private static readonly string Action = MVC.Activities.Form.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
