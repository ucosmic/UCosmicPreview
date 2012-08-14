using System;
using System.Collections.Generic;
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
    public partial class ItemController : Controller
    {
        private readonly IProcessQueries _queries;

        public ItemController(IProcessQueries queries)
        {
            _queries = queries;
        }

        [HttpGet]
        //[OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual ActionResult Get(object id)
        {
            if (id != null)
            {
                var idAsString = id.ToString();
                var idAsInt = idAsString.ParseIntoInt();
                var entity = idAsInt != 0 ? ById(idAsInt) : ByCode(idAsString);
                if (entity != null)
                {
                    //Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                    var model = Mapper.Map<Item>(entity);
                    //if (Request.Headers.Get("x-pjax") != null)
                    //{
                    //    return PartialView(model);
                    //}
                    return View(MVC.Languages.Shared.Views.item, model);
                }
            }
            return HttpNotFound();
        }

        [NonAction]
        private Language ById(int id)
        {
            var entity = _queries.Execute(new LanguageById(id) { EagerLoad = EagerLoad, });
            return entity;
        }

        [NonAction]
        private Language ByCode(string code)
        {
            var entity = _queries.Execute(new LanguageByIsoCode(code) { EagerLoad = EagerLoad });
            return entity;
        }

        private static readonly IEnumerable<Expression<Func<Language, object>>> EagerLoad =
            new Expression<Func<Language, object>>[]
            {
                l => l.Names.Select(n => n.TranslationToLanguage.Names),
                l => l.Names.Select(n => n.Owner)
            };
    }

    public static class ItemRouter
    {
        private static readonly string Area = MVC.Languages.Name;
        private static readonly string Controller = MVC.Languages.Item.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "languages/{id}";
                Order = 10;
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Languages.Item.ActionNames.Get,
                    id = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET")
                });
            }
        }
    }
}
