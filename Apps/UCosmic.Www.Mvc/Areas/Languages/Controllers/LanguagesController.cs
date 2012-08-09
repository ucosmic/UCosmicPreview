using System;
using System.Collections.Generic;
using System.Globalization;
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
        public virtual ActionResult Get(string keyword = "", int size = 10, int number = 1)
        {
            //Thread.Sleep(800);
            Expression<Func<LanguageName, bool>> translatedName = n => n.TranslationToLanguage.TwoLetterIsoCode.Equals(
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase);
            var entities = _queries.Execute(new LanguagesByKeyword(keyword)
            {
                PagerOptions = new PagerOptions
                {
                    PageSize = size,
                    PageNumber = number,
                },
                EagerLoad = new Expression<Func<Language, object>>[]
                {
                    l => l.Names.Select(n => n.TranslationToLanguage)
                },
                OrderBy = new Dictionary<Expression<Func<Language, object>>, OrderByDirection>
                {
                    { l => l.TwoLetterIsoCode.Equals(
                        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase),
                            OrderByDirection.Descending },
                    { l => l.Names.Count,
                            OrderByDirection.Descending },
                    { l => l.Names.AsQueryable().FirstOrDefault(translatedName) != null
                        ? l.Names.AsQueryable().FirstOrDefault(translatedName).Text : null,
                            OrderByDirection.Descending },
                }
            });
            var results = Mapper.Map<LanguageResult[]>(entities)
                //.OrderByDescending(l => l.IsUserLanguage)
                //.ThenByDescending(l => l.NamesCount)
                //.ThenBy(l => l.TranslatedNameText)
                //.Skip(size * (number - 1))
                //.Take(size)
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
