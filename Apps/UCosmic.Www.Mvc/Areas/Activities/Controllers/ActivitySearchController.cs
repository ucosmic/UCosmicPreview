using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Activities;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivitySearchServices
    {
        public ActivitySearchServices(IProcessQueries queryProcessor)
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class ActivitySearchController : BaseController
    {
        private readonly ActivitySearchServices _services;

        public ActivitySearchController(ActivitySearchServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("activity-results")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Get(string establishment, string keyword = null)
        {
            // return 404 when there is no establishment
            if (string.IsNullOrWhiteSpace(establishment)) return HttpNotFound();

            // redirect to canonical route when keyword is in the query string
            if (Request.QueryString.AllKeys.Contains("keyword", new CaseInsensitiveStringComparer())
                //|| pageNumber < 1
            )
                return RedirectToAction(MVC.Activities.ActivitySearch.Get(establishment, keyword));

            var query = new FindTenantActivitiesByKeywordQuery
            {
                Tenant = establishment,
                Keyword = keyword,
                PagerOptions = new PagerOptions
                {
                    //PageNumber = pageNumber,
                    PageSize = int.MaxValue,
                },
                EagerLoad = new Expression<Func<Activity, object>>[]
                {
                    a => a.Tags,
                    a => a.Person,
                },
                OrderBy = new Dictionary<Expression<Func<Activity, object>>, OrderByDirection>
                {
                    { a => a.Values.Title, OrderByDirection.Ascending },
                },
            };
            var activities = _services.QueryProcessor.Execute(query);
            //if (activities.PageCount > 0 && activities.PageCount < pageNumber)
            //    return RedirectToAction(MVC.Activities.ActivitySearch.Get(establishment, keyword, activities.PageCount));
            var models = Mapper.Map<ActivityResults>(activities);
            //var models = new ActivityResults(Enumerable.Empty<ActivityResults.ActivityResult>(), 0);
            var tenant = _services.QueryProcessor.Execute(
                new GetEstablishmentByUrlQuery(establishment)
            );
            Mapper.Map(tenant, models);
            return View(models);
        }

        [HttpPost]
        public virtual JsonResult AutoCompleteKeyword(string establishment, string term)
        {
            var query = new FindTenantActivitiesByKeywordQuery
            {
                Tenant = establishment,
                Keyword = term,
                PagerOptions = new PagerOptions
                {
                    PageSize = int.MaxValue,
                },
                EagerLoad = new Expression<Func<Activity, object>>[]
                {
                    a => a.Tags,
                    a => a.Person,
                },
            };
            var activities = _services.QueryProcessor.Execute(query);
            var people = activities.Where(a => a.Person.DisplayName.Contains(term, StringComparison.OrdinalIgnoreCase)).Select(a => a.Person.DisplayName).ToArray();
            var titles = activities.Where(a => a.Values.Title.Contains(term, StringComparison.OrdinalIgnoreCase)).Select(a => a.Values.Title).ToArray();
            var tags = activities.SelectMany(a => a.Tags.Where(t => t.Text.Contains(term, StringComparison.OrdinalIgnoreCase))).Select(t => t.Text).ToArray();

            var keywords = new List<string>();
            if (people.Any()) keywords.AddRange(people);
            if (titles.Any()) keywords.AddRange(titles);
            if (tags.Any()) keywords.AddRange(tags);

            keywords = keywords.OrderBy(s => s).ToList();

            return Json(keywords);
        }
    }

    public static class ActivitySearchRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivitySearch.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "{establishment}/activities/search/{keyword}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivitySearch.ActionNames.Get,
                    keyword = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class AutoCompleteKeywordRoute : MvcRoute
        {
            public AutoCompleteKeywordRoute()
            {
                Url = "{establishment}/activities/keywords";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivitySearch.ActionNames.AutoCompleteKeyword,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
