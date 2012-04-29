using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class ProfileServices
    {
        public ProfileServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    [Authorize]
    public partial class ProfileController : BaseController
    {
        private readonly ProfileServices _services;

        public ProfileController(ProfileServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("profile")]
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
                        u => u.Person.Affiliations.Select(a => a.Establishment),
                    }
                }
            );

            if (user == null) return HttpNotFound();
            return PartialView(Mapper.Map<ProfileInfo>(user.Person));
        }

    }

    public static class ProfileRouter
    {
        private static readonly string Area = MVC.My.Name;
        private static readonly string Controller = MVC.My.Profile.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ProfileRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/profile";
            private static readonly string Action = MVC.My.Profile.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
