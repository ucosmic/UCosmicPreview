using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

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
        public virtual JsonResult Put(PreferenceCategory category, PreferenceKey key, string value)
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var command = new UpdateMyPreference(User)
                {
                    Category = category,
                    Key = key,
                    Value = value,
                };
                _preferences.Handle(command);
            }
            return Json(isAuthenticated);
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
