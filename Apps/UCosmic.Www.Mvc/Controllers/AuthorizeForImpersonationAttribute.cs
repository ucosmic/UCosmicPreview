using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace UCosmic.Www.Mvc.Controllers
{
    public class AuthorizeForImpersonationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.User.Identity.Name)
                && filterContext.HttpContext.Session != null)
            {
                var userName = filterContext.HttpContext.Session.WasSignedInAs()
                               ?? filterContext.HttpContext.User.Identity.Name;
                var member = Membership.GetUser(userName);
                if (member != null && Roles.GetRolesForUser(member.UserName).Contains(RoleName.AuthenticationAgent))
                {
                    return;
                }
            }
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}