using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Preferences.Models;
using UCosmic.Www.Mvc.Controllers;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Preferences.Controllers
{
    public partial class ChangeController : Controller
    {
        private readonly IHandleCommands<UpdateMyPreference> _preferences;

        public ChangeController(IHandleCommands<UpdateMyPreference> preferences
        )
        {
            _preferences = preferences;
        }

        [HttpPut]
        [UnitOfWork]
        public virtual JsonResult Put(MyPreference model)
        {
            var command = new UpdateMyPreference(User, Request.AnonymousID);
            Mapper.Map(model, command);
            _preferences.Handle(command);
            return Json(null);
        }
    }

    public static class PutRouter
    {
        private static readonly string Area = MVC.Preferences.Name;
        private static readonly string Controller = MVC.Preferences.Change.Name;

        public class PutRoute : MvcRoute
        {
            public PutRoute()
            {
                Url = "my/preferences";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Preferences.Change.ActionNames.Put,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("PUT")
                });
            }
        }
    }
}
