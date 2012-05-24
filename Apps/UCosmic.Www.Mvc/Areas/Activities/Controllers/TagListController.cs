using System.Web.Mvc;
using System.Web.Routing;
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
        public virtual PartialViewResult Add(TagDomainType domainType, int? revisionId, string taggedText)
        {
            var tags = new[]
            {
                new Form.Tag
                {
                    DomainType = domainType,
                    RevisionId = revisionId,
                    TaggedText = taggedText,
                },
            };
            return PartialView(MVC.Activities.Shared.Views._tag_list, tags);
        }

    }

    public static class TagListRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.TagList.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(TagListRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Add
        {
            public const string Route = "activities/tags/add";
            private static readonly string Action = MVC.Activities.TagList.ActionNames.Add;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
