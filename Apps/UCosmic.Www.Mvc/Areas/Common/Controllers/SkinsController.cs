using System;
using System.Web;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Common.Models.Skins;
using UCosmic.Www.Mvc.Controllers;
using System.IO;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class SkinsController : BaseController
    {
        private readonly EstablishmentFinder _establishments;

        public SkinsController(IQueryEntities entityQueries)
        {
            _establishments = new EstablishmentFinder(entityQueries);
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
                    var establishment = _establishments.FindOne(EstablishmentBy.WebsiteUrl(skinContext));
                    if (establishment == null && !skinContext.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
                        establishment = _establishments.FindOne(EstablishmentBy.WebsiteUrl(string.Format("www.{0}", skinContext)));

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
}
