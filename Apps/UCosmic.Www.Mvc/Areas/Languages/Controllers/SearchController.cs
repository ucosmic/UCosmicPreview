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
using UCosmic.Www.Mvc.Areas.Preferences.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Languages.Controllers
{
    [Authenticate]
    public partial class SearchController : Controller
    {
        private readonly IProcessQueries _queries;

        public SearchController(IProcessQueries queries)
        {
            _queries = queries;
        }

        [HttpGet]
        //[OutputCache(VaryByParam = "*", Duration = 5)]
        public virtual ActionResult Get(SearchRequest request)
        {
            if (!Request.IsAjaxRequest())
            {
                var preferences = _queries.Execute(new MyPreferencesByCategory(User)
                {
                    Category = PreferenceCategory.Languages
                });
                Mapper.Map(preferences, request);
                return View(MVC.Languages.Shared.Views.search, request);
            }

            //Thread.Sleep(800);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
            Expression<Func<LanguageName, bool>> translatedName = n => n.TranslationToLanguage.TwoLetterIsoCode.Equals(
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase);
            var entities = _queries.Execute(new LanguagesByKeyword(request.Keyword)
            {
                PagerOptions = new PagerOptions
                {
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
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
                            OrderByDirection.Ascending },
                },
            });
            var model = Mapper.Map<SearchResults>(entities);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

    public static class SearchRouter
    {
        private static readonly string Area = MVC.Languages.Name;
        private static readonly string Controller = MVC.Languages.Search.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "languages";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Languages.Search.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET")
                });
            }
        }
    }
}
