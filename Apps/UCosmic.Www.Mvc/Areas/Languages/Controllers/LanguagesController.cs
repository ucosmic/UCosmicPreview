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
        //[OutputCache(VaryByParam = "*", Duration = 15)]
        public virtual ActionResult Get(string keyword = "")
        {
            //Thread.Sleep(800);
            var entities = _queries.Execute(new LanguagesByKeyword(keyword)
            {
                EagerLoad = new Expression<Func<Language, object>>[]
                {
                    l => l.Names.Select(n => n.TranslationToLanguage)
                },
            });
            var results = Mapper.Map<LanguageResult[]>(entities)
                .OrderByDescending(l => l.IsUserLanguage)
                .ThenByDescending(l => l.NamesCount)
                .ThenBy(l => l.TranslatedNameText)
            ;
            if (Request.IsAjaxRequest())
                return Json(results, JsonRequestBehavior.AllowGet);

            var model = new LanguageFinder
            {
                Keyword = keyword,
                Results = results,
            };
            return View(model);
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
    }
}
