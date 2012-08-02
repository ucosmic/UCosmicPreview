using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class MyHomeServices
    {
        public MyHomeServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    [Authorize]
    public partial class MyHomeController : BaseController
    {
        private readonly MyHomeServices _services;

        public MyHomeController(MyHomeServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("my-home")]
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
            return View(Mapper.Map<MyHomeInfo>(user.Person));
        }
    }

    public static class MyHomeRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.MyHome.Name;

        public class GetRoute : MvcRoute
        {
            public const string UrlConstant = "my/home";

            public GetRoute()
            {
                Url = UrlConstant;
                AlternateUrls = new[] {"my"};
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.MyHome.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
