using System.Web.Mvc;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Areas.Activity.Models;
using System;

namespace UCosmic.Www.Mvc.Areas.Activity.Controllers
{
    public class TinyMceServices
    {
        public TinyMceServices(IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; set; }
    }

    public partial class TinyMceController : BaseController
    {
        private readonly TinyMceServices _services;

        public TinyMceController(TinyMceServices services)
        {
            _services = services;
        }

        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual PartialViewResult AddTag(string domainType, int revisionId, string taggedText)
        {
            var tags = new[]
            {
                new TinyMceModel.Tag
                {
                    DomainType = Type.GetType(domainType),
                    RevisionId = revisionId,
                    TaggedText = taggedText,
                },
            };
            return PartialView(MVC.Activity.Shared.Views._list_tags, tags);
        }

    }
}
