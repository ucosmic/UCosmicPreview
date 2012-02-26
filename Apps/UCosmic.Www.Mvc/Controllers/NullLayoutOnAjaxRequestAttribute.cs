using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    public class NullLayoutOnAjaxRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Controller.ViewBag.NullLayout = new object();
        }
    }
}