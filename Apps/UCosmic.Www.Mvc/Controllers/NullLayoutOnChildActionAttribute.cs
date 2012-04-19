using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    public class NullLayoutOnChildActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                filterContext.Controller.ViewBag.NullLayout = new object();
        }
    }
}