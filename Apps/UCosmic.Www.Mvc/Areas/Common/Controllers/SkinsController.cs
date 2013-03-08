using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Common.Models.Skins;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class SkinsController : BaseController
    {
        private readonly IProcessQueries _queryProcessor;

        public SkinsController(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        [RedirectToRawUrl]
        public virtual RedirectResult Change(string skinContext, string returnUrl)
        {
            // skin context is expected to be member url
            if (!string.IsNullOrWhiteSpace(skinContext))
            {
                if (!skinContext.Equals("default", StringComparison.OrdinalIgnoreCase) &&
                    !skinContext.Equals("ucosmic", StringComparison.OrdinalIgnoreCase) &&
                    !skinContext.Equals("remove", StringComparison.OrdinalIgnoreCase))
                {
                    //var establishment = _establishments.FindOne(EstablishmentBy.WebsiteUrl(skinContext));
                    var establishment = _queryProcessor.Execute(new GetEstablishmentByUrlQuery(skinContext));
                    if (establishment == null && !skinContext.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
                        //establishment = _establishments.FindOne(EstablishmentBy.WebsiteUrl(string.Format("www.{0}", skinContext)));
                        establishment = _queryProcessor.Execute(new GetEstablishmentByUrlQuery(string.Format("www.{0}", skinContext)));

                    if (establishment != null && !string.IsNullOrWhiteSpace(establishment.WebsiteUrl))
                    {
                        while (establishment != null && !Directory.Exists(Server.MapPath(
                            string.Format("~/content/skins/{0}", establishment.WebsiteUrl))))
                        {
                            establishment = establishment.Parent;
                        }
                    }
                    if (establishment != null && !string.IsNullOrWhiteSpace(establishment.WebsiteUrl))
                    {
                        // give a cookie
                        HttpContext.SkinCookie(establishment.WebsiteUrl);
                    }
                    else
                    {
                        HttpContext.SkinCookie(null);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(HttpContext.SkinCookie()))
                {
                    HttpContext.SkinCookie(null);
                }
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }
            return Redirect(returnUrl);
        }

        [ChildActionOnly]
        [ActionName("apply")]
        public virtual PartialViewResult Apply(string skinFile)
        {
            var model = new ApplySkinInfo { SkinFile = skinFile };
            var skin = HttpContext.SkinCookie();
            if (!string.IsNullOrWhiteSpace(skin))
            {
                model.SkinName = skin;
            }
            return PartialView(model);
        }

        [ChildActionOnly]
        [ActionName("logo")]
        public virtual PartialViewResult Logo()
        {
            var model = new LogoInfo();
            var skin = HttpContext.SkinCookie();
            if (!string.IsNullOrWhiteSpace(skin)
                && Url.IsLocalUrl(string.Format("~/content/skins/{0}/head-logo.png", skin))
                && System.IO.File.Exists(Server.MapPath(string.Format("~/content/skins/{0}/head-logo.png", skin))))
            {
                model.SkinName = skin;
            }
            return PartialView(model);
        }

        [ActionName("sample")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ViewResult Sample(string content = "")
        {
            content = content ?? string.Empty;
            ViewBag.SampleContent = content;

            ViewBag.Title = "Skinning Tests";
            if (content == "map")
                ViewBag.Title = "Skinning Test for map page";
            if (content == "form")
                ViewBag.Title = "Skinning Test for input form";

            return View();
        }
    }

    public static class SkinsRouter
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Skins.Name;

        public class ChangeRoute : MvcRoute
        {
            public ChangeRoute()
            {
                Url = "as/{*skinContext}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Skins.ActionNames.Change,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class ApplyRoute : MvcRoute
        {
            public ApplyRoute()
            {
                Url = "skins/apply/{skinFile}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Skins.ActionNames.Apply,
                    skinFile = string.Empty,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class LogoRoute : MvcRoute
        {
            public LogoRoute()
            {
                Url = "skins/logo";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Skins.ActionNames.Logo,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class SampleRoute : MvcRoute
        {
            public SampleRoute()
            {
                Url = "skins/sample/{content}";
                AlternateUrls = new[] { "skins" };
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Skins.ActionNames.Sample,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    content = new RequiredIfPresentRouteConstraint(),
                });
            }
        }
    }
}
