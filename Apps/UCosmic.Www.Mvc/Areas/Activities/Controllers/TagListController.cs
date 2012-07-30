using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class TagListServices
    {
    }

    public partial class TagListController : BaseController
    {
        private readonly TagListServices _services;

        public TagListController(TagListServices services)
        {
            _services = services;
        }

        [HttpPost]
        public virtual PartialViewResult Add(ActivityTagDomainType domainType, int? domainKey, string text)
        {
            var tags = new[]
            {
                new ActivityForm.Tag
                {
                    DomainType = domainType,
                    DomainKey = domainKey,
                    Text = text,
                },
            };
            return PartialView(MVC.Activities.Shared.Views._tag_list, tags);
        }

    }

    public static class TagListRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.TagList.Name;

        public class AddRoute : Route
        {
            public AddRoute():base("activities/tags/add", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.TagList.ActionNames.Add,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
