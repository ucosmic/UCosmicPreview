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
                        var cookie = new HttpCookie("skin", establishment.WebsiteUrl)
                        {
                            Expires = DateTime.UtcNow.AddDays(30),
                            Path = "/"
                        };
                        Response.SetCookie(cookie);
                    }
                    else
                    {
                        var cookie = new HttpCookie("skin", null)
                        {
                            Expires = DateTime.UtcNow.AddDays(-1),
                        };
                        Response.SetCookie(cookie);
                    }
                }
                else if (Request.Cookies["skin"] != null)
                {
                    var cookie = new HttpCookie("skin", null)
                    {
                        Expires = DateTime.UtcNow.AddDays(-1),
                    };
                    Response.SetCookie(cookie);
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
            var cookie = Request.Cookies["skin"];
            if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
            {
                model.SkinName = cookie.Value;
            }
            return PartialView(model);
        }

        [ChildActionOnly]
        [ActionName("logo")]
        public virtual PartialViewResult Logo()
        {
            var model = new LogoInfo();
            var cookie = Request.Cookies["skin"];
            if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value)
                && Url.IsLocalUrl(string.Format("~/content/skins/{0}/head-logo.png", cookie.Value))
                && System.IO.File.Exists(Server.MapPath(string.Format("~/content/skins/{0}/head-logo.png", cookie.Value))))
            {
                model.SkinName = cookie.Value;
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

        public class ChangeRoute : Route
        {
            public ChangeRoute()
                : base("as/{skinContext}", new MvcRouteHandler())
            {
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

        public class ApplyRoute : Route
        {
            public ApplyRoute()
                : base("skins/apply/{skinFile}", new MvcRouteHandler())
            {
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

        public class LogoRoute : Route
        {
            public LogoRoute()
                : base("skins/logo", new MvcRouteHandler())
            {
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

        public class SampleRoute : Route
        {
            public SampleRoute()
                : base("skins/sample/{content}", new MvcRouteHandler())
            {
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

        public class SampleSkinsDefaultRoute : SampleRoute
        {
            public SampleSkinsDefaultRoute()
            {
                Url = "skins";
            }
        }
    }
}
