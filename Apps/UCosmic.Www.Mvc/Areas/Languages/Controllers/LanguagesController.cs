using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Languages;
using UCosmic.Www.Mvc.Areas.Languages.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Languages.Controllers
{
    public partial class LanguagesController : Controller
    {
        private readonly IProcessQueries _queries;

        public LanguagesController(IProcessQueries queries)
        {
            _queries = queries;
        }

        [HttpGet]
        [ActionName("languages")]
        public virtual ActionResult Get(string keyword = "")
        {
            return View((object)keyword);
        }

        [HttpGet]
        [ActionName("_languages-table")]
        //[OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual ActionResult GetTable(string keyword = "")
        {
            var entities = _queries.Execute(new LanguagesByKeyword(keyword)
            {
                EagerLoad = new Expression<Func<Language, object>>[]
                {
                    l => l.Names.Select(n => n.TranslationToLanguage)
                },
            });
            var models = Mapper.Map<LanguageTable>(entities)
                .OrderByDescending(l => l.IsUserLanguage)
                .ThenByDescending(l => l.NamesCount)
                .ThenBy(l => l.TranslatedNameText)
            ;
            return PartialView(models);
        }
    }

    public static class LanguagesRouter
    {
        private static readonly string Area = MVC.Languages.Name;
        private static readonly string Controller = MVC.Languages.Languages.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "languages";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Languages.Languages.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET")
                });
            }
        }

        public class GetFilteredRoute : GetRoute
        {
            public GetFilteredRoute()
            {
                Url = "languages-by-keyword/{keyword}";
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class GetTableRoute : MvcRoute
        {
            public GetTableRoute()
            {
                Url = "languages-table/by-keyword/{keyword}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Languages.Languages.ActionNames.GetTable,
                    keyword = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET")
                });
            }
        }
    }
}
